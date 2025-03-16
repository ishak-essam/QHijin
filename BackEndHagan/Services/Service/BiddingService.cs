using AutoMapper;
using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BackEndHagan.Services.Service
{
    public class BiddingService : IBiddingService
    {
        private  HaganContext _haganContext;
        private  ResponseDTO _responseDTO;
        private  IMapper _mapper;

        public BiddingService ( HaganContext haganContext, IMapper mapper )
        {
            _mapper = mapper;
            _haganContext = haganContext;
            _responseDTO = new ResponseDTO ();
        }
        public async Task<ResponseDTO> GetBiddingById ( int Biddingid )
        {
            try
            {
                var Bidding=await _haganContext.Biddings.Include(e=>e.Items).Include(e=>e.Items.Photo).FirstOrDefaultAsync(x=>x.BiddingNo==Biddingid);

                if ( Bidding != null )
                {
                    _responseDTO.Result = Bidding;
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
        public async Task<ResponseDTO> GetBiddingByUserId ( int userid )
        {
            try
            {
                var items = await _haganContext.Biddings.Include(e=>e.Items).Include(e=>e.Items.Photo).ToListAsync();
                var user= items.Where  (e => e?.Items.UserId == userid);

                if ( items != null )
                {
                    _responseDTO.Result = user;
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
        public async Task<ResponseDTO> GetBiddingByName ( string name = "" )
        {
            try
            {
                IQueryable<Bidding> query =_haganContext.Biddings.Include(e=>e.Items).Include(e=>e.User);
                if ( query != null )
                {
                    query = query.Where (x => x!.Title!.Contains (name));
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
        public async Task<ResponseDTO> GetBiddings ( )
        {
            try
            {
                var items =await  _haganContext.Biddings.Include(b => b.Items).Include(b => b.Items.Photo).ToListAsync();
                _responseDTO.Result = items;
            }
            catch ( Exception ex )
            {
                _responseDTO.Result = ex.Message.ToString ();
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }
        public async Task<ResponseDTO> GetBiddingsActive ( )
        {
            try
            {
                var items =  _haganContext.Biddings.Include(e=>e.Items).Include(e=>e.Items.Photo).Where(e=>e.IsActive==true);
                items= items.Where (e => e.Items.IsActive==true);
                _responseDTO.Result = await items.ToListAsync();
            }
            catch ( Exception ex )
            {
                _responseDTO.Result = ex.Message.ToString ();
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }
        public async Task<ResponseDTO> RemoveBidding ( int BiddingNo )
        {
            try
            {
                var Bidding=await _haganContext.Biddings.FirstOrDefaultAsync(u=>u.BiddingNo==BiddingNo);

                if ( Bidding != null )
                {
                    Bidding.IsActive = false;
                    _haganContext.Biddings.Update (Bidding);
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
        public async Task<ResponseDTO> ReNewBidding ( int BiddingNo )
        {
            try
            {
                var Bidding=await _haganContext.Biddings.FirstOrDefaultAsync(u=>u.BiddingNo==BiddingNo);

                if ( Bidding != null )
                {
                    Bidding.IsActive = true;
                    _haganContext.Biddings.Update (Bidding);
                    _haganContext.SaveChanges ();
                    _responseDTO.Result = "Renew";
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
        public async Task<ResponseDTO> UpdateBidding (  BiddingDto BiddingDto )
        {
            try
            {
                var bid=_mapper.Map<Bidding>(BiddingDto);
                var item =  _haganContext.Items.FirstOrDefault(u => u.Id == bid.ItemNo);
                var user =  _haganContext.Users.FirstOrDefault(u => u.Id == bid.UserId);
                if ( user != null && item != null )
                {
                    var Bidding =  _haganContext.Biddings.FirstOrDefault(u => u.BiddingNo == BiddingDto.BiddingNo);
                    if ( Bidding != null )
                    {
                        Bidding.Title = bid.Title;
                        Bidding.EndDate = BiddingDto.EndDate;
                        Bidding.IsActive = bid.IsActive;
                        // Update only the Bidding entity
                        _haganContext.Biddings.Update (Bidding);
                        // Save changes
                        await _haganContext.SaveChangesAsync ();
                        _responseDTO.Result = "Updated";
                    }
                    else
                    {
                        _responseDTO.Message = "Bid not found";
                        _responseDTO.IsSuccess = false;
                    }
                }
                else if ( user == null )
                {
                    _responseDTO.Message = "user isn't exists ";
                    _responseDTO.IsSuccess = false;
                }
                else
                {
                    _responseDTO.Message = "item isn't exists ";
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
        public async Task<ResponseDTO> UpdateBidder ( BiddingDto BiddingDto )
        {
            try
            {
                var bid=_mapper.Map<Bidding>(BiddingDto);
                var Bidding =await  _haganContext.Biddings.FirstOrDefaultAsync(u => u.BiddingNo == BiddingDto.BiddingNo);
                Bidding!.UserId = bid.UserId;
                Bidding.Price = BiddingDto.Price;
                Bidding.IsActive = true;
                _haganContext.Biddings.Update (Bidding);
               await _haganContext.SaveChangesAsync ();
                _responseDTO.Result = "Updated";
            }
            catch ( Exception ex )
            {
                _responseDTO.Message = ex.Message;
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }
        public async Task<ResponseDTO> AddBidding ( BiddingDto BiddingDto )
        {
            try
            {
                var item = await _haganContext.Items.FirstOrDefaultAsync(u => u.Id == BiddingDto.ItemNo);
                var user = await _haganContext.Users.Include(e=>e.Actions).ThenInclude(p=>p.Type).FirstOrDefaultAsync(u => u.Id == BiddingDto.UserId);
                if ( item == null ) {
                    _responseDTO.IsSuccess = false;
                    _responseDTO.Result = "Item isn't exist";
                    return _responseDTO;
                }
                if ( user == null )
                {
                    _responseDTO.IsSuccess = false;
                    _responseDTO.Result = "User isn't exist";
                    return _responseDTO;
                }
                if(user.Actions.Any(a => a.TypeId == 1)==false)
                  await  _haganContext.Actions.AddAsync(new QHijin.Entities.Action { 
                    Id=0,
                    TypeId=1,
                    UserId=user.Id});

              await  _haganContext.Biddings.AddAsync(_mapper.Map<Bidding> (BiddingDto));
                    await    _haganContext.SaveChangesAsync();
                       _responseDTO.Result = "Bid Created";
            }
            catch ( Exception ex )
            {
                _responseDTO.Message = ex.Message;
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }
        public async Task UpdateBidState ( )
        {
            await _haganContext.Database.ExecuteSqlRawAsync ("EXEC UpdateBidState");
        }
    }
}