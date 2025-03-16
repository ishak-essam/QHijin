using AutoMapper;
using BackEndHagan.Entities;
using BackEndHagan.Models.Dto;
using BackEndHagan.Services.IService;
using BackEndHagan.Services.Service;
using QHijin.Entities;
using QHijin.Models;
using QHijin.Models.Dto;

namespace BackEndHagan.Mapper
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMap ( ) {
            var configMap=new MapperConfiguration (config=>{
                config.CreateMap<Item,ItemDto>().ReverseMap();
                config.CreateMap<PaymentRequest, PaymentRequestDto>().ReverseMap();
                config.CreateMap<User,UserDTO>().ReverseMap();
                config.CreateMap<User,UserDTO2>();
                config.CreateMap<UserDTO2,User>();
                config.CreateMap<AboutPhotosDTO, AboutPhotos>();
                config.CreateMap<ContactUsDto, ContactUs>();
                config.CreateMap<Photo,PhotoDto>();
                config.CreateMap<Trainers, trainerDto>().ReverseMap();
                config.CreateMap<ItemPhysicalDto,ItemPhysical>().ReverseMap();
                config.CreateMap<PhotoDto,Photo>();
                config.CreateMap<SocialMediaDto,SocialMedia>();
                config.CreateMap<SocialMedia,SocialMediaDto>();
                config.CreateMap<Ads, AdsDto>().ReverseMap();
                config.CreateMap<ServiceDto, ServicesSite>().ReverseMap();
                config.CreateMap<AdvantageDto, Advantages>().ReverseMap();
                config.CreateMap<Employee,EmployeeDto>();
                config.CreateMap<EmployeeDto,Employee>();
                config.CreateMap<Feedback, FeedbackDto>().ReverseMap();
                config.CreateMap<Policy_Refund, Policy_RefundDto>().ReverseMap();
                config.CreateMap<HowTobuy, HowTobuyDto>().ReverseMap();
                config.CreateMap<Contracting_Policy, Contracting_PolicyDto>().ReverseMap();
                config.CreateMap<Delivery_Period, Delivery_PeriodDto>().ReverseMap();
                config.CreateMap<Banner,BannerDto>();
                config.CreateMap<BannerDto,Banner>();

                config.CreateMap<TypeDto,Entities.Type>().ReverseMap();
                config.CreateMap<Title,TitleDto>().ReverseMap();
                config.CreateMap<ITypeService, ITypeService>().ReverseMap();
                config.CreateMap<PriceAndRateDto,PriceAndRate>().ReverseMap();
                config.CreateMap<PrivacyAndPolicyDto,PrivacyAndPolicy>().ReverseMap();
                config.CreateMap<SalaryDto,Salary>().ReverseMap();
                config.CreateMap<WorkDto,Work>().ReverseMap();
                config.CreateMap<ContactDto,Contact>().ReverseMap();
                config.CreateMap<AdminDto,Admin>().ReverseMap();
                config.CreateMap<InvoiceDto,Invoice>().ReverseMap();
                config.CreateMap<AboutDto,About>().ReverseMap();
                config.CreateMap<SocialMediaDto,SocialMedia>().ReverseMap();
                config.CreateMap<TermsAndConditionDto,TermsAndCondition>().ReverseMap();
                config.CreateMap<TypeDto,Entities.Type>().ReverseMap();
                config.CreateMap<BiddingDto,Bidding>().ReverseMap();
                config.CreateMap<IAdminService,AdminService>().ReverseMap();
            });
            return configMap;
        }
    }
}
