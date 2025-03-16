using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BackEndHagan.Services.IService
{
    public interface IBiddingService
    {
        Task<ResponseDTO> GetBiddings ( );
        Task<ResponseDTO> GetBiddingsActive ( );
        Task UpdateBidState ( );
        Task<ResponseDTO> GetBiddingById ( int Biddingid );
        Task<ResponseDTO> GetBiddingByUserId ( int Biddingid );
        Task<ResponseDTO> GetBiddingByName ( string name = "" );
        Task<ResponseDTO> UpdateBidding (  BiddingDto BiddingDto );
        Task<ResponseDTO> UpdateBidder (  BiddingDto BiddingDto );
        Task<ResponseDTO> AddBidding (  BiddingDto Add );
        Task<ResponseDTO> RemoveBidding ( int id );
        Task<ResponseDTO> ReNewBidding ( int id );
    }
}
