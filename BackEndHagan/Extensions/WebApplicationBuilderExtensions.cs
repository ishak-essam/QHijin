using BackEndHagan.Services.IService;
using BackEndHagan.Services.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using QHijin.ContactUs.IContactUs;
using QHijin.Services.ContactUs;
using QHijin.Services.IService;
using QHijin.Services.Service;
using System.Text;

namespace BackEndHagan.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder AddAppAuthetication(this WebApplicationBuilder builder)
        {
            #region Token Auth
            var settingsSection = builder.Configuration.GetSection("ApiSettings");
            var secret = settingsSection.GetValue<string>("Secret");
            var issuer = settingsSection.GetValue<string>("Issuer");
            var audience = settingsSection.GetValue<string>("Audience");

            var key = Encoding.UTF8.GetBytes(secret);

         
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    ValidateAudience = true
                };
            });

            #endregion

            #region Cors

            builder.Services.AddCors (opt => opt.AddPolicy ("CorsPolicy", Policy =>
            {
                Policy
                .WithOrigins ("*").AllowAnyMethod ().AllowAnyHeader () ;
           //     Policy.AllowAnyOrigin()
                //     .AllowAnyHeader()
              //       .AllowAnyMethod();
            }));
            #endregion

            #region Service App

            builder.Services.AddScoped<IAuthService, AuthService> ();
            builder.Services.AddScoped<IUserService, UserService> ();
            builder.Services.AddScoped<ITitleService, TitleService> ();
            builder.Services.AddScoped<IContactService, ContactService> ();
            builder.Services.AddScoped<IPaymentRequestService, PaymentRequestService> ();
            builder.Services.AddScoped<ITypeService, TypeService> ();
            builder.Services.AddScoped<IInvoiceService, InvoiceService> ();
            builder.Services.AddScoped<IItemService, ItemService> ();
            builder.Services.AddScoped<IServices, ServiceService> ();
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<IAdsService, AdsService>();
            builder.Services.AddScoped<IContactUsService, ContactUsService>();
            builder.Services.AddScoped<IBannerService, BannerService> ();
            builder.Services.AddScoped<IBiddingService, BiddingService> ();
            builder.Services.AddScoped<IPolicy_RefundService, Policy_RefundService> ();
            builder.Services.AddScoped<IHowTobuyService, HowTobuyService> ();
            builder.Services.AddScoped<IContracting_PolicyService, Contracting_PolicyService> ();
            builder.Services.AddScoped<IDelivery_PeriodService, Delivery_PeriodService> ();
            builder.Services.AddScoped<ITrainerService, TrainerService> ();
            builder.Services.AddScoped<IFeedbackService, FeedbackService> ();
            builder.Services.AddScoped<IPriceAndRateService, PriceAndRateService> ();
            builder.Services.AddScoped<IPrivacyAndPolicyService, PrivacyAndPolicyService> ();
            builder.Services.AddScoped<ITypeService, TypeService> ();
            builder.Services.AddScoped<IAdvantages, AdvantagesService> ();
            builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGeneratorService> ();
            builder.Services.AddScoped<ISalaryService, SalaryService> ();
            builder.Services.AddScoped<IEmployeeService, EmployeeService> ();
            builder.Services.AddScoped<IAboutService, AboutService> ();
            builder.Services.AddScoped<ISocialMediaService, SocialMediaService> ();
            builder.Services.AddScoped<ITermsAndConditionService, TermsAndConditionService> ();

            #endregion

            #region BidUpdateService

            builder.Services.AddHostedService<BidUpdateService> ();
            #endregion

            return builder;
        }
    }
}
