using DataAccsess;
using HelloJWT.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace HelloJWT.Extensions
{
    public  static class DataBaseExtensions
    {


        public static IServiceCollection AddDtoOption(this IServiceCollection services, IConfiguration configuration)
        {
            BsonClassMap.RegisterClassMap<TokenModel>(pm =>
            {
                pm.AutoMap();
                pm.MapIdProperty(p => p.Id)
                .SetSerializer(new StringSerializer(BsonType.ObjectId))
                .SetIdGenerator(StringObjectIdGenerator.Instance);
                pm.SetIgnoreExtraElements(true);
            });

            return services;
        }

        public static IServiceCollection AddDbOptionManager(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DbOptions>(configuration.GetSection("DbOptions"));

            services.AddSingleton(typeof(IGenericRepository<,>), typeof(DbRepository<,>));
            return services;

        }

    }
    
}
