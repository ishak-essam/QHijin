using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BackEndHagan.Services.IService
{
    public interface IInvoiceService
    {
        Task<ResponseDTO> GetInvoices ( );
        Task<ResponseDTO> GetInvoiceById ( int Invoiceid ); 
        Task<ResponseDTO> GetInvoiceByUserId ( int Invoiceid ); 
        Task<ResponseDTO> UpdateInvoice ( [FromBody] InvoiceDto InvoiceDto );
        Task<ResponseDTO> AddInvoice ( [FromBody] InvoiceDto Add );
        Task<ResponseDTO> RemoveInvoice ( int id );
    }
}
