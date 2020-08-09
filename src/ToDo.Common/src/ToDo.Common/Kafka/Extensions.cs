using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDo.Common.Options;
using HostedServices = Microsoft.Extensions.Hosting;


namespace ToDo.Common.Kafka
{
    public static class Extensions
    {
        public static IServiceCollection AddKafka(this IServiceCollection services)
        {

            var producerConfig = new ProducerConfig();
            var consumerConfig = new ConsumerConfig();

            using (var serviceProvider = services.BuildServiceProvider())
            {
                var configuration = serviceProvider.GetService<IConfiguration>();
                services.Configure<ProducerConfig>(configuration.GetSection("producer"));
                producerConfig = configuration.GetOptions<ProducerConfig>("producer");
                services.Configure<ConsumerConfig>(configuration.GetSection("consumer"));
                consumerConfig = configuration.GetOptions<ConsumerConfig>("consumer");
            }

            services.AddSingleton<ProducerConfig>(producerConfig);
            services.AddSingleton<ConsumerConfig>(consumerConfig);
            return services;
        }
        
    }
}
