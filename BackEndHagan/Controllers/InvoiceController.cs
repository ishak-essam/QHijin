using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using BackEndHagan.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndHagan.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/invoice")]
    public class InvoiceController : ControllerBase
    {
        private IInvoiceService _invoiceService ;
        private HaganContext _haganContext ;

        public InvoiceController (HaganContext haganContext, IInvoiceService invoiceService )
        {
            _invoiceService = invoiceService;
            _haganContext = haganContext;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetInvoices ( )
        {
            ResponseDTO  emp= await _invoiceService.GetInvoices ();
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<ResponseDTO>> GetInvoiceById ( int id )
        {
            ResponseDTO  emp= await _invoiceService.GetInvoiceById (id);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }

        [HttpGet ("filterByUser/{id}")]
       public async Task<ActionResult<ResponseDTO>>  GetInvoiceByUserId ( int id )
        {
            ResponseDTO  emp= await _invoiceService.GetInvoiceByUserId (id);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }
         [HttpPost]
       public async Task<ActionResult<ResponseDTO>>  AddInvoice ( [FromBody] InvoiceDto InvoiceDto )
        {
            ResponseDTO  emp= await _invoiceService.AddInvoice (InvoiceDto);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }

        [HttpPut]
       public async Task<ActionResult<ResponseDTO>>  UpdateInvoice ( [FromBody] InvoiceDto InvoiceDto )
        {
            ResponseDTO  emp= await _invoiceService.UpdateInvoice (InvoiceDto);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }
        [HttpDelete]
       public async Task<ActionResult<ResponseDTO>>  RemoveInvoice ( int id )
        {
            ResponseDTO  emp= await  _invoiceService.RemoveInvoice (id);
            if ( emp.IsSuccess == false )
                return BadRequest (emp);
            return Ok (emp);
        }
    }
}
