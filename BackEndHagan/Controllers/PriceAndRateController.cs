using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEndHagan.Controllers
{
    [ApiController]
    [Route ("api/price_rate")]
    public class PriceAndRateController : ControllerBase
    {
        private IPriceAndRateService _PriceAndRateService ;

        public PriceAndRateController ( IPriceAndRateService PriceAndRateService )
        {
            _PriceAndRateService = PriceAndRateService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetPriceAndRates ( )
        {
            ResponseDTO  emp= await _PriceAndRateService.GetPriceAndRate ();
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }

        [HttpGet ("{id}")]
        [Authorize]

         public async Task<ActionResult<ResponseDTO>> GetPriceAndRateById ( int id )
        {
            ResponseDTO  emp= await _PriceAndRateService.GetPriceAndRateById (id);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }

        //[HttpPost]
        //[Authorize]
        // public async Task<ActionResult<ResponseDTO>> AddPriceAndRate (  PriceAndRateDto PriceAndRate )
        //{
        //    return _PriceAndRateService.AddPriceAndRate (PriceAndRate);
        //}

        [HttpPost]
        [Authorize]
         public async Task<ActionResult<ResponseDTO>> UpdatePriceAndRate ( [FromBody] PriceAndRateDto PriceAndRate )
        {
            ResponseDTO  emp= await _PriceAndRateService.UpdatePriceAndRate (PriceAndRate);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }
        [HttpDelete]
        [Authorize]
         public async Task<ActionResult<ResponseDTO>> RemovePriceAndRate ( int id )
        {
            ResponseDTO  emp= await _PriceAndRateService.RemovePriceAndRate (id);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }
    }
}