using BackEndHagan.Entities;
using BackEndHagan.Extensions;
using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using BackEndHagan.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace BackEndHagan.Controllers
{
    [ApiController]
    [Route ("api/bid")]
    public class BiddingController : ControllerBase
    {
        private HaganContext _HaganContext ;
        private IBiddingService _BiddingService ;
        public BiddingController ( IBiddingService BiddingService, HaganContext HaganContext )
        {
            _BiddingService = BiddingService;
            _HaganContext = HaganContext;
        }
        
        [HttpGet]
        public async Task<ActionResult< ResponseDTO>> GetBiddings ( )
        {
            ResponseDTO  emp=   await  _BiddingService.GetBiddings ();
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }
        [HttpGet("active")]
        public async Task<ActionResult<ResponseDTO>> GetBiddingsActive ( )
        {
            ResponseDTO  emp=   await  _BiddingService.GetBiddingsActive ();
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }
        


        [ HttpGet ("{id}")]
        [Authorize]

        public async Task<ActionResult<ResponseDTO>> GetBiddingById ( int id )
        {
            ResponseDTO  emp=   await    _BiddingService.GetBiddingById (id);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }
        [HttpGet ("user/{id}")]
        [Authorize]

        public async Task<ActionResult<ResponseDTO>> GetBiddingByUserId ( int id )
        {
            ResponseDTO  emp=   await  _BiddingService.GetBiddingByUserId (id);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }
        
        
        [HttpPost ]
        [Authorize]

        public async Task<ActionResult<ResponseDTO>> AddBidding (  BiddingDto BiddingDto )
        {
            var item=_HaganContext.Items.FirstOrDefault(e=>e.Id==BiddingDto.ItemNo);
            var bid=_HaganContext.Biddings.FirstOrDefault(e=>e.ItemNo==BiddingDto.ItemNo);
            if ( item?.IsActive == false )
                return BadRequest (" item isn't exists");
            if ( bid != null )
            { 
            if( bid?.IsActive == true )
                return BadRequest ("Bid for item already exists");
            }
                if ( BiddingDto.StartDate < DateTime.Now)
                return BadRequest ("Start date cannot be in the past");
            if ( BiddingDto.EndDate < DateTime.Today.AddDays (1) )
                return BadRequest ("End date cannot be in the past or today");

            ResponseDTO  emp=   await _BiddingService.AddBidding (BiddingDto);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }

        [HttpPut]
        [Authorize]

        public async Task<ActionResult<ResponseDTO>> UpdateBidding (  BiddingDto BiddingDto )
        {
            var item=_HaganContext.Items.FirstOrDefault(e=>e.Id==BiddingDto.ItemNo);
            if ( item!.UserId == BiddingDto.UserId )
                return BadRequest ("User can't bid for his item");
            if ( BiddingDto.StartDate < DateTime.Now )
                return BadRequest ("Start date cannot be in the past");
            if ( BiddingDto.EndDate < DateTime.Today.AddDays (1) )
                return BadRequest ("End date cannot be in the past or today");
            ResponseDTO  emp=   await _BiddingService.UpdateBidding (BiddingDto);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }
        [HttpPut ("bidder")]
        [Authorize]

        public async Task<ActionResult<ResponseDTO>> UpdateBidder ( BiddingDto BiddingDto )
        {
            var bid=_HaganContext.Biddings.FirstOrDefault(e=>e.BiddingNo==BiddingDto.BiddingNo);
            var item=_HaganContext.Items.FirstOrDefault(e=>e.Id==bid!.ItemNo);
            if ( bid != null )
            {
                if ( item!.UserId == BiddingDto.UserId )
                    return BadRequest ("User can't bid for his item");
            }
            else
                return BadRequest ("bid isn't exists");

            ResponseDTO  emp=   await _BiddingService.UpdateBidder (BiddingDto);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }
        [HttpDelete]
        [Authorize]

        public async Task<ActionResult<ResponseDTO>> RemoveBidding ( int id )
        {
            ResponseDTO  emp=   await _BiddingService.RemoveBidding (id);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }
        [HttpPost("renew")]
        [Authorize]

        public async Task<ActionResult<ResponseDTO>> ReNewBidding ( int id )
        {
            ResponseDTO  emp=   await _BiddingService.ReNewBidding (id);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }
    
    //    [HttpPost ("updateBidState")]
    //[Authorize]
    //    public async  Task<IActionResult> UpdateBidState ( )
    //    {
    //        var  emp=   _BiddingService.UpdateBidState ();
    //        return Ok ( emp);
    //    }
    }
}
