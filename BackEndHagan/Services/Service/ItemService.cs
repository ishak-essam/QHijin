using AutoMapper;
using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using Microsoft.EntityFrameworkCore;
using QHijin.Entities;
namespace BackEndHagan.Services.Service
{
    public class ItemService : IItemService
    {
        private HaganContext _haganContext;
        private ResponseDTO _responseDTO;
        private IMapper _mapper;
        public ItemService(HaganContext haganContext, IMapper mapper)
        {
            _haganContext = haganContext;
            _mapper = mapper;
            _responseDTO = new ResponseDTO();
        }

        public async Task<ResponseDTO> GetItemById(int Itemid)
        {
            try
            {
                var item = await _haganContext.Items.Include(e => e.Photo).Include(e => e.BiddingNoNavigation).Include(e => e.ItemPhysical).Include(e => e.Trainers).FirstOrDefaultAsync(ele => ele.Id == Itemid);
                if (item != null)
                {
                    _responseDTO.Result = item;
                }
                else
                {
                    _responseDTO.Message = "It isn't exists";
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

        public async Task<ResponseDTO> GetItems()
        {
            try
            {
                var items2 = await _haganContext.Items.Include(e => e.Photo).Include(e => e.Trainers).Include(e => e.BiddingNoNavigation).Include(e => e.ItemPhysical).ToListAsync();
                _responseDTO.Result = items2;
            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message.ToString();
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }
        public async Task<ResponseDTO> GetItemsActive()
        {
            try
            {
                var items2 = _haganContext.Items.Include(e => e.Photo).Include(e => e.Trainers).Include(e => e.BiddingNoNavigation).Include(e => e.ItemPhysical).Where(e => e.IsActive == true);
                _responseDTO.Result = await items2.ToListAsync();
            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message.ToString();
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> RemoveItem(int id)
        {
            try
            {
                var item = await _haganContext.Items.FirstOrDefaultAsync(u => u.Id == id);

                if (item != null)
                {
                    item.IsActive = false;
                    _haganContext.Items.Update(item);
                    await _haganContext.SaveChangesAsync();
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
        public async Task<ResponseDTO> RenewItem(int id)
        {
            try
            {
                var item = await _haganContext.Items.FirstOrDefaultAsync(u => u.Id == id);

                if (item != null)
                {
                    item.IsActive = true;
                    _haganContext.Items.Update(item);
                    await _haganContext.SaveChangesAsync();
                    _responseDTO.Result = "Renew";
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

        public async Task<ResponseDTO> UpdateItem(ItemDto2 itemIn)
        {
            try
            {
                var item = await _haganContext.Items.Include(e => e.ItemPhysical).FirstOrDefaultAsync(ele => ele.Id == itemIn.Id);
                if (item != null)
                {
                    var itemPh = await _haganContext.ItemPhysical.FirstOrDefaultAsync(ele => ele.Id == item.ItemPhysical.Id);
                    if (itemPh == null)
                    {
                        _responseDTO.Message = "Item Physical isn't exists";
                        _responseDTO.IsSuccess = false;
                        return _responseDTO;
                    }
                    item.Disc = itemIn.Disc;
                    item.Name = itemIn.Name;
                    item.IsActive = itemIn.IsActive;
                    item.Price = itemIn.Price;
                    item.Currency = itemIn.Currency;
                    item.Health = itemIn.Health;
                    item.CheckedDate = itemIn.CheckedDate;
                    item.Disc = itemIn.Disc;
                    item.UserId = itemIn.UserId;
                    item.Photo = [];
                    item.Invoices = [];
                    item.EmployeeItems = [];
                    item.User = null!;
                    item.BiddingNoNavigation = null;
                    _haganContext.Items.Update(item);
                    await _haganContext.SaveChangesAsync();
                    _responseDTO.Result = "Item Updated";
                }
                else
                {
                    _responseDTO.Message = "It isn't exists";
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

        public async Task<ResponseDTO> GetItemByName(string name = "")
        {
            try
            {
                IQueryable<Item> query = _haganContext.Items;
                if (query != null)
                {
                    query = query.Where(x => x.Name.Contains(name));
                    _responseDTO.Result = await query.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString();
            }
            return _responseDTO;
        }
        public async Task<ResponseDTO> GetItemByUserCreated(int id = 0)
        {
            try
            {
                IQueryable<Item> query = _haganContext.Items.Include(e => e.Photo).Include(e => e.ItemPhysical);
                if (query != null)
                {
                    query = query.Where(x => x.UserId == (id));
                    _responseDTO.Result = await query.ToListAsync();
                }
                return _responseDTO;
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString();
            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> UpdateItemImageById(PhotoDto photoDto)
        {
            try
            {
                var baseUrl = photoDto.ImgLocalPath;
                Photo product = _mapper.Map<Photo>(photoDto);
                var item = await _haganContext.Items.FirstOrDefaultAsync(ele => ele.Id == photoDto.ItemId);
                var Image = await _haganContext.Photos.FirstOrDefaultAsync(ele => ele.PhId == photoDto.PhId);
                if (item != null)
                {
                    if (!string.IsNullOrEmpty(Image!.ImgLocalPath))
                    {
                        var oldFileDirectory = Path.Combine(Directory.GetCurrentDirectory(), Image.ImgLocalPath);
                        FileInfo file = new FileInfo(oldFileDirectory);
                        if (file.Exists)
                        {
                            file.Delete();
                        }
                    }
                    if (Image != null && photoDto.Image != null)
                    {
                        string fileName = Image.PhId + Path.GetExtension(photoDto.Image.FileName);
                        string filePath = @"wwwroot/Items/Images/" + fileName;
                        var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                        using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                        {
                            photoDto.Image.CopyTo(fileStream);
                        }

                        product.ImgUrl = baseUrl + "/Items/Images/" + fileName;
                        product.ImgLocalPath = filePath;
                        Image.ImgUrl = product.ImgUrl;
                        Image.ImgLocalPath = product.ImgLocalPath;
                        Image.IsMain = product.IsMain;
                        Image.ItemId = product.ItemId;
                        _haganContext.Photos.Update(Image);
                        _haganContext.SaveChanges();
                    }
                    else
                    {
                        Image!.ImgUrl = "https://placehold.co/600*400";
                        _haganContext.Photos.Update(Image);
                        _haganContext.SaveChanges();
                    }


                    _responseDTO.Result = "Updated";
                }
                else
                {
                    _responseDTO.Message = "item isn't exists";
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

        public async Task<ResponseDTO> ItemsImage()
        {
            try
            {
                var item = await _haganContext.Photos.ToListAsync();
                _responseDTO.Result = item;
            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message.ToString();
                _responseDTO.IsSuccess = false;
            }
            return _responseDTO;
        }
        public async Task<ResponseDTO> ItemsImageByPhId(int id)
        {
            try
            {
                var item = await _haganContext.Photos.FirstOrDefaultAsync(ele => ele.PhId == id);
                if (item != null)
                    _responseDTO.Result = item;
                else
                {
                    _responseDTO.Message = "It isn't exists";
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

        public async Task<ResponseDTO> ItemsImageByItemId(int id)
        {
            IQueryable<Photo> query = _haganContext.Photos;
            try
            {
                if (query != null)
                {
                    query = _haganContext.Photos.Where(x => x.ItemId == id);
                    _responseDTO.Result = await query.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString();
            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> GetTrainers()
        {
            IQueryable<Trainers> query = _haganContext.Trainer;
            try
            {
                if (query != null)
                {
                    query = _haganContext.Trainer.Where(x => x.status == true);
                    _responseDTO.Result = await query.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString();
            }
            return _responseDTO;
        }
        public async Task<ResponseDTO> GetItemTrainer()
        {
            try
            {
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message.ToString();
            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> AddItem(ItemDto Add)
        {
            try
            {
                Item product = _mapper.Map<Item>(Add);
                var UserId = await _haganContext.Users.FirstOrDefaultAsync(ele => ele.Id == Add.UserId);
                if (UserId != null)
                {
                    var baseUrl = Add.BaseUrl;
                    var ids = _haganContext.Items.ToList();
                    var id = 1;
                    if (ids.Count == 0)
                    {
                        id = 1;
                    }
                    else
                        id = ids.Last().Id + 1;
                    if (Add.Video != null)
                    {
                        string extension = ".mp4";
                        string fileName = id + extension;
                        string filePath = @"wwwroot/Items/Videos/" + fileName;
                        var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                        using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                        {
                            Add.Video.CopyTo(fileStream);
                        }
                        product.VideoUrl = baseUrl + "/Items/Videos/" + fileName;
                        product.VideoLocalPath = filePath;
                    }
                    _haganContext.Items.Add(product);
                    await _haganContext.SaveChangesAsync();
                    ItemPhysical itemPhysical = _mapper.Map<ItemPhysical>(Add.ItemPhysical);
                    itemPhysical.ItemId = id;
                    await _haganContext.ItemPhysical.AddAsync(itemPhysical);
                    await _haganContext.SaveChangesAsync();

                    if (Add.Images != null)
                    {
                        var images = Add.Images.ToList();
                        for (var i = 0; i < Add.Images.Count; i++)
                        {
                            PhotoDto photo = new PhotoDto();
                            var lastPhoto2 = _haganContext.Photos.ToList().LastOrDefault();
                            var lastItems2 = _haganContext.Items.ToList().LastOrDefault();
                            var id2 = 1;
                            if (lastPhoto2 != null)
                                id2 = lastPhoto2.PhId + 1;
                            if (images[i] != null)
                            {
                                string extension = Path.GetExtension(images[i].FileName);
                                string fileName = id2 + extension;
                                string filePath = @"wwwroot/Items/Images/" + fileName;
                                var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                                using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                                {
                                    images[i].CopyTo(fileStream);
                                }

                                photo.ImgUrl = baseUrl + "/Items/Images/" + fileName;
                                photo.ImgLocalPath = filePath;
                                photo.ItemId = _haganContext.Items.Count();
                            }
                            else
                            {
                                photo.ImgUrl = "https://placehold.co/600*400";
                            }
                            Photo photo1 = _mapper.Map<Photo>(photo);
                            photo1.ItemId = lastItems2!.Id;
                            if (i == 0) photo1.IsMain = true;
                            await _haganContext.Photos.AddAsync(photo1);
                            await _haganContext.SaveChangesAsync();
                        }
                    }
                    _responseDTO.Result = "Item Created";
                }
                else
                {
                    _responseDTO.Message = "User isn't exists";
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
    }
}
