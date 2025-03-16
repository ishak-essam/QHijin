using AutoMapper;
using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using QHijin.Entities;
using QHijin.Models.Dto;
using QHijin.Services.IService;
using System.Linq;

namespace QHijin.Services.Service
{
    public class TrainerService : ITrainerService
    {
        private HaganContext _haganContext;
        private ResponseDTO _responseDTO;
        private IMapper _mapper;
        public TrainerService(HaganContext haganContext, IMapper mapper)
        {
            _haganContext = haganContext;
            _mapper = mapper;
            _responseDTO = new ResponseDTO();
        }
        public async Task<ResponseDTO> AddTrainer(trainerDto Add)
        {
            try {
                Trainers product = _mapper.Map<Trainers>(Add);
               var baseUrl = Add.trnImgLocalPath;
                if (Add.Image != null)
                {
                    var images = Add.Image;
                    var CV = Add.Cv;
                    var ids = await _haganContext.Trainer.ToListAsync();
                    var id = 1;
                    if (ids.Count == 0)
                    {
                        id = 1;
                    }
                    else
                        id = ids.Last().trnId + 1;
                    if (images != null && Add.Cv != null)
                    {
                        string extension = Path.GetExtension(images.FileName);
                        string fileName = id + extension;
                        string filePath = @"wwwroot/Trainer/Images/" + fileName;
                        var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                        using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                        {
                            images.CopyTo(fileStream);
                        }
                        string extensionCV = Path.GetExtension(CV.FileName);
                        string fileNameCV = id + extensionCV;
                        string filePathCV = @"wwwroot/Trainer/Docs/" + fileNameCV;
                        var filePathDirectoryCV = Path.Combine(Directory.GetCurrentDirectory(), filePathCV);
                        using (var fileStreamCV = new FileStream(filePathDirectoryCV, FileMode.Create))
                        {
                            CV.CopyTo(fileStreamCV);
                        }
                        product.trnImgUrl = baseUrl + "/Trainer/Images/" + fileName;
                        product.trnCV = baseUrl + "/Trainer/Docs/" + fileNameCV;
                        product.trnImgLocalPath = filePath;
                        product.itemId = Add.itemId;
                        product.empId = Add.empId;
                        product.rpayment_link = Add.rpayment_link;
                        product.rentmoney = Add.rentmoney;
                        await _haganContext.Trainer.AddAsync(product);
                        await _haganContext.SaveChangesAsync();
                        _responseDTO.Result = "Trainer Created";
                    }
                    else
                    {
                        _responseDTO.IsSuccess=false;
                    }
                }
            }
            catch(Exception ex) { }
            return _responseDTO;
        }

        public async Task<ResponseDTO> GetTrainer()
        {
            try
            {
                //var user = await _haganContext.Trainer.Include(e => e.Item).Include(e=>e.Employee).ToListAsync();
                var user = await _haganContext.Trainer.Include(e => e.Item).ToListAsync();
                _responseDTO.Result = user;
            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message.ToString();
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> GetTrainerById(int trneid)
        {
            try
            {
                //var trainers =await _haganContext.Trainer.Include(e => e.Item).Include(e => e.Employee).FirstOrDefaultAsync(e=>e.trnId== trneid);
                var trainers =await _haganContext.Trainer.Include(e => e.Item).FirstOrDefaultAsync(e=>e.trnId== trneid);
                if (trainers != null)
                    _responseDTO.Result = trainers;
                else
                {
                    _responseDTO.Message = "it isn't exist";
                    _responseDTO.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message.ToString();
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }
        public async Task<ResponseDTO> RemoveTrainer(int id)
        {
            try
            {
                var item =await _haganContext.Trainer.FirstOrDefaultAsync(u => u.trnId == id);
                if (item != null)
                {
                    var oldFileDirectory = Path.Combine(Directory.GetCurrentDirectory(), item.trnImgLocalPath);
                    var oldFileDirectoryCv = Path.Combine(Directory.GetCurrentDirectory(), item.trnCV);
                    FileInfo file = new FileInfo(oldFileDirectory);
                    FileInfo fileCV = new FileInfo(oldFileDirectory);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                    if (fileCV.Exists)
                    {
                        fileCV.Delete();
                    }
                    _haganContext.Trainer.Remove(item);
                    _haganContext.SaveChanges();
                    _responseDTO.Result = "Deleted";
                }
                else
                {
                    _responseDTO.Message = "it isn't exist";
                    _responseDTO.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message.ToString();
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> UpdateTrainer(trainerDto Add)
        {
            var item = await _haganContext.Trainer.FirstOrDefaultAsync(u => u.trnId == Add.trnId);
            var baseUrl = Add.trnImgLocalPath;
            if (Add.Image != null)
            {
                var oldFileDirectory = Path.Combine(Directory.GetCurrentDirectory(), item.trnImgLocalPath);
                FileInfo file = new FileInfo(oldFileDirectory);
                if (file.Exists)
                {
                    file.Delete();
                }
                string extension = Path.GetExtension(Add.Image.FileName);
                string fileName = item.trnId + extension;
                string filePath = @"wwwroot/Trainer/Images/" + fileName;
                var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                {
                    Add.Image.CopyTo(fileStream);
                }
                item.trnImgUrl = baseUrl + "/Trainer/Images/" + fileName;
                item.trnImgLocalPath = filePath;
            }
            if (Add.Cv != null)
            {
                var oldFileDirectoryCv = Path.Combine(Directory.GetCurrentDirectory(), item.trnCV);
                FileInfo fileCV = new FileInfo(oldFileDirectoryCv);

                if (fileCV.Exists)
                {
                    fileCV.Delete();
                }
                string extensionCV = Path.GetExtension(Add.Cv.FileName);
                string fileNameCV = item.trnId + extensionCV;
                string filePathCV = @"wwwroot/Trainer/Docs/" + fileNameCV;
                var filePathDirectoryCV = Path.Combine(Directory.GetCurrentDirectory(), filePathCV);
                using (var fileStreamCV = new FileStream(filePathDirectoryCV, FileMode.Create))
                {
                    Add.Cv.CopyTo(fileStreamCV);
                }
                item.trnCV = baseUrl + "/Trainer/Docs/" + fileNameCV;
            }
            item.itemId = Add.itemId;
            item.empId = Add.empId;
            item.rpayment_link = Add.rpayment_link;
            item.FullName = Add.FullName;
            item.Phone = Add.Phone;
            item.Email = Add.Email;
            item.rentmoney = Add.rentmoney;
            _haganContext.Trainer.Update(item);
            await _haganContext.SaveChangesAsync();
            _responseDTO.Result = "Updated";
            return _responseDTO;
        }
    }
}
