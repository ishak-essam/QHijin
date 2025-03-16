using AutoMapper;
using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QHijin.Entities;
using QHijin.Models;
using QHijin.Models.Dto;
using QHijin.Services.IService;
using System.Net;
using System.Text;

namespace QHijin.Services.Service
{
    public class PaymentRequestService : IPaymentRequestService
    {
        private HaganContext _haganContext;
        private ResponseDTO _responseDTO;
        private IMapper _mapper;
        public PaymentRequestService(HaganContext haganContext, IMapper mapper)
        {
            _mapper = mapper;
            _haganContext = haganContext;
            _responseDTO = new ResponseDTO();
        }

        public async Task<ResponseDTO> GetPayments()
        {
            try
            {
                _responseDTO.Result = await _haganContext.PaymentRequest.Include(e => e.User).Include(e => e.Item).ToListAsync();
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString();
            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> GetPaymentById(int Paymentid)
        {

            try
            {
                var item = await _haganContext.PaymentRequest.Include(e => e.User).Include(e => e.Item).FirstOrDefaultAsync(e => e.Id == Paymentid);

                if (item != null)
                    _responseDTO.Result = item;
                else
                {
                    _responseDTO.IsSuccess = false;
                    _responseDTO.Message = "payment is not exist";
                }
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString();
            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> RemovePayment(int id)
        {
            try
            {
                var item = await _haganContext.PaymentRequest.FirstOrDefaultAsync(e => e.Id == id);

                if (item != null)
                {
                    _haganContext.Remove(item);
                    _haganContext.SaveChanges();
                    _responseDTO.Result = item;
                }
                else
                {
                    _responseDTO.IsSuccess = false;
                    _responseDTO.Message = "payment is not exist";
                }
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString();
            }
            return _responseDTO;
        }

        public Task<ResponseDTO> UpdatePayment(PaymentRequestDto PaymentDto)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDTO> AddPaymentSend(PaymentRequestDto PaymentDto)
        {
            try
            {
                PaymentRequest pay = _mapper.Map<PaymentRequest>(PaymentDto);
                var user = await _haganContext.Users.FirstOrDefaultAsync(e => e.Id == pay.UserId);
                var trainer = await _haganContext.Trainer.FirstOrDefaultAsync(e => e.trnId == pay.TrainerId);
                var item = await _haganContext.Items.FirstOrDefaultAsync(e => e.Id == pay.ItemId);
                if ((trainer == null || user == null) && item == null)
                {
                    _responseDTO.IsSuccess = false;
                    _responseDTO.Message = "(User or Trainer) or Item isn't exists";
                    return _responseDTO;
                }
                double amout = 0;
                if (user != null)
                {
                    amout = item.Price + (item.Price * (item.VAT / 100) + item.Price * (item.Services / 100));
                    pay.UserId = PaymentDto.UserId;
                    pay.TrainerId = null;
                }
                else
                {
                    amout = trainer!.rentmoney + (trainer.rentmoney * (trainer.VAT / 100)) + (trainer.rentmoney * (trainer.Services / 100));
                    pay.UserId = null;
                    pay.TrainerId = PaymentDto.TrainerId;
                }
                pay.SendDate = DateTime.Now;
                pay.OrderDetails = trainer != null ? "User get trainer for item" : "User pay item";
                pay.PaymentCurrency = "EGP";
                pay.CountryName = "EGP";
                pay.PaymentAmount = amout;
                pay.PaymentDone = false;
                pay.RequestRef = CreateReceiptRef();
                await _haganContext.PaymentRequest.AddAsync(pay);
                await _haganContext.SaveChangesAsync();
                Transaction Transaction = new Transaction
                {
                    profile_id = 103633,
                    tran_type = "sale",
                    tran_class = "ecom",
                    cart_description = pay.FirstName + " " + pay.LastName,
                    cart_id = pay.OrderDetails,
                    cart_currency = pay.PaymentCurrency,
                    cart_amount = amout,
                    callback = "https://www.q-musicteaching.com/PaymentCallBack.aspx?Ref=" + pay.RequestRef,
                    returnURL = "https://www.q-musicteaching.com/PaymentCallBack.aspx?Ref=" + pay.RequestRef
                };
                string PayUrl = "https://secure-egypt.paytabs.com/payment/request";
                string authorization = "SLJNDWHWDB-JJB2JRDR2N-ZMRKTZMNBM";
                var responsePay = new
                {
                    Transaction = Transaction,
                    PayUrl = PayUrl,
                    Authorization = authorization
                };
                _responseDTO.Result = responsePay;
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString();
            }
            return _responseDTO;
        }

        private static Random random = new Random((int)DateTime.Now.Ticks);

        public string CreateReceiptRef()
        {
            string randomw = string.Empty;
            string small_alphabets = "abcdefghijklmnopqrstuvwxyz1234567890";
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < 40; i++)
            {
                //ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                ch = small_alphabets[random.Next(0, 35)];
                builder.Append(ch);
            }

            randomw = builder.ToString();
            int dtcount = _haganContext.PaymentRequest.Count(pr => pr.RequestRef == randomw);
            //  string dtcount = ProjectMapper.ExecuteScalar("Select Count(ID) From PaymentRequests where RequestRef=@RequestRef", CommandType.Text, "@RequestRef", randomw);
            if (Convert.ToInt16(dtcount) > 0)
            {
                CreateReceiptRef();
            }
            else
            {
                return randomw;
            }
            return "";
        }

        public async Task<ResponseDTO> PaymentRecive(string RequestRef)
        {
            try
            {
                var Payment = await _haganContext.PaymentRequest.FirstOrDefaultAsync(e => e.RequestRef == RequestRef);
                if (Payment != null)
                {
                    var item = await _haganContext.Items.FirstOrDefaultAsync(e => e.Id == Payment.UserId);
                    if (item != null)
                    {
                        item.IsActive = false;
                        item.saleStatus = true;
                    }
                    Payment.PaymentDate = DateTime.Now;
                    Payment.PaymentDone = true;
                    _haganContext.PaymentRequest.Update(Payment);
                    _haganContext.PaymentRequest.Update(Payment);
                    _haganContext.SaveChanges();
                    _responseDTO.Result = "Updated";
                }
                else
                {
                    _responseDTO.IsSuccess = false;
                    _responseDTO.Message = "payment is not exist";
                }
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString();
            }
            return _responseDTO;
        }


        public async Task<object> PaymentRequestAsync(Transaction transaction)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string apiUrl = "https://secure-egypt.paytabs.com/payment/request";
            string inputJson = JsonConvert.SerializeObject(transaction);
            inputJson = inputJson.Substring(0, inputJson.IndexOf("}")) + ",\"return\":\"" + transaction.returnURL + "\"}";

            using (var client = new HttpClient())
            {
                var content = new StringContent(inputJson, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Add("authorization", "SLJNDWHWDB-JJB2JRDR2N-ZMRKTZMNBM"); //test
                var response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonConvert.DeserializeObject<object>(jsonResponse);
                    return responseObject!;
                }
                else
                {
                    return null!;
                }
            }
        }
    }
}
