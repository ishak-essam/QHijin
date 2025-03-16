

using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using BackEndHagan.Models;
using BackEndHagan.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace BackEndHagan.Services.Service
{
    public class ContactService: IContactService
    {
        private  HaganContext _haganContext;
        private  IMapper _mapper;
        private  ResponseDTO _responseDTO;
        public ContactService ( HaganContext haganContext , IMapper mapper

            )
        {
            _haganContext = haganContext;
            _responseDTO = new ResponseDTO ();
            _mapper = mapper;
        }
        public async Task<ResponseDTO> GetContactById ( int Contactid )
        {
            try
            {
                var Contact=await _haganContext.Contacts.FirstOrDefaultAsync(x=>x.ContactNo==Contactid);
                if ( Contact != null )
                {
                    _responseDTO.Result = Contact;
                }
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

        public async Task<ResponseDTO> GetContactByUser ( int id  )
        {
            try
            {
                IQueryable<Contact> query =_haganContext.Contacts;
                if ( query != null )
                {
                    query = query.Where (x => x.UserId==id);
                    _responseDTO.Result =await query.ToListAsync();
                }
                return _responseDTO;
            }
            catch ( Exception ex )
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString ();
            }
            return _responseDTO;
        }
        public async Task<ResponseDTO> GetContacts ( )
        {
            try
            {
                var items = await _haganContext.Contacts.ToListAsync();
                _responseDTO.Result = items;
            }
            catch(Exception ex) { 
                    _responseDTO.Message = ex.Message.ToString ();
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }
        public async Task<ResponseDTO> RemoveContact (int EmailNo )
        {
            try
            {
                var Contact=await _haganContext.Contacts.FirstOrDefaultAsync(u=>u.ContactNo==EmailNo);
               
                if ( Contact != null )
                {
                    _haganContext.Contacts.Remove (Contact);
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

        public async Task<ResponseDTO> UpdateContact ( [FromBody] ContactDto ContactDto )
        {
            try
            {
                Contact item =_mapper.Map<Contact>(ContactDto);
                var Contact = await _haganContext.Contacts.FirstOrDefaultAsync(u => u.ContactNo == item.ContactNo);
                if ( Contact != null )
                {
                    Contact.ContactMsg = item.ContactMsg;
                    Contact.ReadTime = item.ReadTime;
                    Contact.UserId = item.UserId;
                    _haganContext.Contacts.Update (Contact);
                    await _haganContext.SaveChangesAsync ();
                    _responseDTO.Result = Contact;
                }
                else
                {
                    _responseDTO.Message = "Contact not found";
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
        public async Task<ResponseDTO> AddContact ( [FromBody] ContactDto ContactDto )
        {
            try
            {
                await _haganContext.Contacts.AddAsync (_mapper.Map<Contact> (ContactDto));
                await _haganContext.SaveChangesAsync ();
                _responseDTO.Result = "Contact Created";
            }
            catch ( Exception ex )
            {
                _responseDTO.Message = ex.Message;
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }

    }
}
