using Alively.Core.Entities;
using Alivley.Api.DTOs;
using AutoMapper;

namespace Alivley.Api.ConfigurationExtensions
{
    public static class AdapterConfigurationExtensions
    {
        public static MapperConfiguration GetConfig()
        {
            return new MapperConfiguration(config =>
            {
                config.CreateMap<User, UserDTO>();
            });
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services), "Services are missing. ");
            }

            services.AddSingleton<IMapper>(GetConfig().CreateMapper());

            return services;
        }
    }
}
