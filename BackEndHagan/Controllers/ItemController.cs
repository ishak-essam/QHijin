using AutoMapper;
using BackEndHagan.Entities;
using BackEndHagan.Extensions;
using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndHagan.Controllers
{
    [ApiController]
    [Route("api/item")]
    public class ItemController : ControllerBase
    {
        private HaganContext _haganContext;
        private ResponseDTO _responseDTO;
        private IItemService _itemService;
        private IMapper _mapper;
        public ItemController(HaganContext haganContext, IMapper mapper, IItemService itemService)
        {
            _itemService = itemService;
            _haganContext = haganContext;
            _mapper = mapper;
            _responseDTO = new ResponseDTO();
        }
        [HttpPost]
        [Authorize]

        public async Task<ActionResult<ResponseDTO>> AddItem(ItemDto Add)
        {

            try
            {
                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                Add.BaseUrl = baseUrl;
                _responseDTO = await _itemService.AddItem(Add);
            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message.ToString();
                _responseDTO.IsSuccess = false;
            }

            if (_responseDTO.IsSuccess == false)
                return BadRequest(_responseDTO);
            return Ok(_responseDTO);

        }

        [HttpPut("photo")]
        [Authorize]

        public async Task<ActionResult<ResponseDTO>> UpdateItemImageById(PhotoDto photoDto)
        {
            try
            {
                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                photoDto.ImgLocalPath = baseUrl;
                _responseDTO = await _itemService.UpdateItemImageById(photoDto);
            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message.ToString();
                _responseDTO.IsSuccess = false;
            }

            if (_responseDTO.IsSuccess == false)
                return BadRequest(_responseDTO);
            return Ok(_responseDTO);
        }
        [HttpDelete("photo/{id:int}")]
        public async Task<ActionResult<ResponseDTO>> DeleteItemImageById(int id)
        {
            try
            {
                Photo product = await _haganContext.Photos.FirstAsync(ele => ele.PhId == id);
                if (!string.IsNullOrEmpty(product.ImgLocalPath))
                {
                    var oldFileDirectory = Path.Combine(Directory.GetCurrentDirectory(), product.ImgLocalPath);
                    FileInfo file = new FileInfo(oldFileDirectory);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }
                _haganContext.Remove(product);
                await _haganContext.SaveChangesAsync();
                _responseDTO.Result = "deleted image";
            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message;
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }

        [HttpGet("photo/{id:int}")]
        [Authorize]

        public async Task<ActionResult<ResponseDTO>> GetItemImageById(int id)
        {
            ResponseDTO emp = await _itemService.ItemsImageByPhId(id);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }
        [HttpGet("photo/item/{id:int}")]
        [Authorize]

        public async Task<ActionResult<ResponseDTO>> GetItemImageByUsId(int id)
        {
            ResponseDTO emp = await _itemService.ItemsImageByItemId(id);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }
        [HttpGet("photos")]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> Photos(int id)
        {
            ResponseDTO emp = await _itemService.ItemsImage();
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> UpdateItem(ItemDto2 UpdateAdd)
        {
            ResponseDTO emp = await _itemService.UpdateItem(UpdateAdd);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }
        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetItems()
        {
            ResponseDTO emp = await _itemService.GetItems();
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            if (User.GetAdminRole() == "True")
            {
                if (User.GetRoles().Contains(4))
                {
                    var item = _haganContext.Items.Select(item => new
                    {
                        item.Id,
                        item.UserId,
                        item.BiddingNo,
                        item.Name,
                        item.History,
                        item.Trainers,
                        item.IsActive,
                        item.saleStatus,
                        item.Health,
                        item.VideoUrl,
                        item.Services,
                        item.VAT,
                        item.VideoLocalPath,
                        item.CheckedDate,
                        item.Currency,
                        item.Type,
                        item.BiddingNoNavigation,
                        item.PaymentRequest,
                        item.ItemPhysical,
                        item.EmployeeItems,
                        item.Invoices
                    });

                    return Ok(item);
                }
                else if (User.GetRoles().Contains(1))
                    return Ok(emp);
                else
                    return Unauthorized();
            }
            if (User.GetAdminRole() == "null") return Ok(emp);

            return Ok(emp);

        }
        [HttpGet("active")]
        public async Task<ActionResult<ResponseDTO>> GetItemsActive()
        {
            ResponseDTO emp = await _itemService.GetItemsActive();
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO>> GetItemById(int id)
        {
            ResponseDTO emp = await _itemService.GetItemById(id);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }
        [HttpGet("filterByName")]
        [Authorize]

        public async Task<ActionResult<ResponseDTO>> GetItemByName(string name = "")
        {
            ResponseDTO emp = await _itemService.GetItemByName(name);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }
        [HttpGet("filterByUser/{UserId}")]
        [Authorize]

        public async Task<ActionResult<ResponseDTO>> GetItemByUserCreated(int UserId)
        {
            ResponseDTO emp = await _itemService.GetItemByUserCreated(UserId);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }
        [HttpDelete("DeleteItem/{Id}")]
        [Authorize]

        public async Task<ActionResult<ResponseDTO>> RemoveItem(int Id)
        {
            ResponseDTO emp = await _itemService.RemoveItem(Id);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }



        [HttpPut("RenewItem/{Id}")]
        [Authorize]

        public async Task<ActionResult<ResponseDTO>> RenewItem(int Id)
        {
            ResponseDTO emp = await _itemService.RenewItem(Id);
            if (emp.IsSuccess == false)
                return BadRequest(emp);
            return Ok(emp);
        }
    }
}
