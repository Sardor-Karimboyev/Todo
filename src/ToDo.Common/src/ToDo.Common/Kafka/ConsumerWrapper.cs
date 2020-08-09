using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.Common.Kafka
{
    public class ConsumerWrapper
    {
        private string _topicName;
        private ConsumerConfig _consumerConfig;
        private IConsumer<string, string> _consumer;
        public ConsumerWrapper(ConsumerConfig config, string topicName)
        {
            this._topicName = topicName;
            this._consumerConfig = config;
            this._consumer = new ConsumerBuilder<string, string>(this._consumerConfig).Build();
        }

        public string ReadMessage()
        {
            var consumeResult = this._consumer.Consume();
            return consumeResult.Message.Value;
        }
    }
}
