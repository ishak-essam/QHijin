using AutoMapper;
using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace BackEndHagan.Services.Service
{
    public class InvoiceService : IInvoiceService
    {
        static Random random = new Random();
        private  IMapper _mapper;
        private  HaganContext _haganContext;
        private  ResponseDTO _responseDTO;
        public InvoiceService ( HaganContext haganContext, IMapper mapper )
        {
            _haganContext = haganContext;
            _mapper = mapper;
            _responseDTO = new ResponseDTO ();
        }
        public async Task<ResponseDTO> AddInvoice ( [FromBody] InvoiceDto InvoiceDto )
        {
            try
            {
                InvoiceDto.InvoiceNo = GenerateUniqueRandomNumber(_haganContext);
                await _haganContext.Invoices.AddAsync (_mapper.Map<Invoice> (InvoiceDto));
                await _haganContext.SaveChangesAsync ();
                _responseDTO.Result = "Invoice Created";
            }
            catch ( Exception ex )
            {
                _responseDTO.Message = ex.Message;
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> GetInvoiceById ( int Invoiceid )
        {
            try
            {
                var Invoice=await _haganContext.Invoices.FirstOrDefaultAsync(x=>x.InvoiceNo==Invoiceid);
                if ( Invoice != null )
                    _responseDTO.Result = Invoice;
                else
                {
                    _responseDTO.IsSuccess = false;
                    _responseDTO.Message = "It isn't exist";
                }
            }
            catch ( Exception ex )
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString ();
            }
            return _responseDTO;
        }
        public async Task<ResponseDTO> GetInvoiceByUserId ( int id )
        {
            try
            {
                var Invoice=await _haganContext.Invoices.FirstOrDefaultAsync(x=>x.UserId==id);
                if ( Invoice != null )
                    _responseDTO.Result = Invoice;
                else
                {
                    _responseDTO.IsSuccess = false;
                    _responseDTO.Message = "It isn't exist";
                }
            }
            catch ( Exception ex )
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString ();
            }
            return _responseDTO;
        }
        public async Task<ResponseDTO> GetInvoices ( )
        {
            try
            {
            _responseDTO.Result = await _haganContext.Invoices.ToListAsync ();
            }
            catch ( Exception ex )
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString ();
            }
            return _responseDTO;
        }
        public async Task<ResponseDTO> RemoveInvoice ( int id )
        {
            try
            {
                var invoice=await _haganContext.Invoices.FirstOrDefaultAsync(u=>u.InvoiceId==id);

                if ( invoice != null )
                {
                    _haganContext.Invoices.Remove (invoice);
                    _haganContext.SaveChanges ();
                    _responseDTO.Result = "Deleted";
                }
                else
                {
                    _responseDTO.Message = "it isn't exist";
                    _responseDTO.IsSuccess = false;
                }
            }
            catch ( Exception ex )
            {
                _responseDTO.Message = ex.Message.ToString ();
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }
        public async Task<ResponseDTO> UpdateInvoice ( [FromBody] InvoiceDto InvoiceDto )
        {
            try
            {
                Invoice item =_mapper.Map<Invoice>(InvoiceDto);
                var Invoice = await _haganContext.Invoices.FirstOrDefaultAsync(u => u.InvoiceNo == item.InvoiceNo);
                if ( Invoice != null )
                {
                    Invoice.Total = InvoiceDto.Total;
                    Invoice.InvoiceNo = InvoiceDto.InvoiceNo;
                    Invoice.ItemId = InvoiceDto.ItemId;
                    Invoice.UserId = InvoiceDto.UserId;
                    _haganContext.Invoices.Update (Invoice);
                    // Save changes
                    await _haganContext.SaveChangesAsync ();
                    _responseDTO.Result = Invoice;
                }
                else
                {
                    _responseDTO.Message = "Invoice not found";
                    _responseDTO.IsSuccess = false;
                }
            }
            catch ( Exception ex )
            {
                _responseDTO.Message = ex.Message;
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }

        static int GenerateUniqueRandomNumber ( HaganContext dbContext )
        {
            int randomNumber;
            do
            {
                randomNumber = random.Next (10000, 100000);
            } while ( dbContext.Invoices.Any (entity => entity.InvoiceNo == randomNumber) );

            return randomNumber;
        }
    }
}
