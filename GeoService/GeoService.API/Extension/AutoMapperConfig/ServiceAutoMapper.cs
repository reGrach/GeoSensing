using AutoMapper;
using GeoService.BLL.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace GeoService.API.Extension
{
    public static class ServiceAutoMapper
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}
