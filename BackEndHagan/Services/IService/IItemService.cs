using BackEndHagan.Models.Dto;

namespace BackEndHagan.Services.IService
{
    public interface IItemService
    {
        Task<ResponseDTO> GetItems();
        Task<ResponseDTO> GetTrainers();
        Task<ResponseDTO> GetItemsActive();
        Task<ResponseDTO> GetItemById(int Itemid);
        Task<ResponseDTO> AddItem(ItemDto ItemDto);
        Task<ResponseDTO> UpdateItem(ItemDto2 ItemDto);
        Task<ResponseDTO> RemoveItem(int id);
        Task<ResponseDTO> RenewItem(int id);
        Task<ResponseDTO> GetItemByName(string name = "");
        Task<ResponseDTO> GetItemByUserCreated(int id = 0);
        Task<ResponseDTO> UpdateItemImageById(PhotoDto photoDto);
        Task<ResponseDTO> ItemsImage();
        Task<ResponseDTO> ItemsImageByPhId(int id);
        Task<ResponseDTO> ItemsImageByItemId(int id);
    }
}
