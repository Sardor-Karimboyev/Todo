using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Common.Kafka
{
    public class ProducerWrapper
    {
        private string _topicName;
        private ProducerBuilder<string, string> _producer;
        private ProducerConfig _config;
        private static readonly Random rand = new Random();

        public ProducerWrapper(ProducerConfig config, string topicName)
        {
            this._topicName = topicName;
            this._config = config;
            this._producer = new ProducerBuilder<string, string>(this._config);
        }
        public async Task WriteMessage(string message)
        {
            var dr = await this._producer.Build().ProduceAsync(this._topicName, new Message<string, string>()
            {
                //Is Key optional?
                Key = rand.Next(5).ToString(),
                Value = message
            });
            return;
        }
    }
}
