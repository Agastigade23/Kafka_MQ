using Confluent.Kafka;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka_Producer
{
    public class ProduceMessage
    {
        public async Task CreateMessage()
        {
            var clientConfig = new ClientConfig();

            clientConfig.BootstrapServers = "<bootstrap-host-port-pair>";
            clientConfig.SecurityProtocol = Confluent.Kafka.SecurityProtocol.SaslSsl;
            clientConfig.SaslMechanism = Confluent.Kafka.SaslMechanism.Plain;
            clientConfig.SaslUsername = "<api-key>";
            clientConfig.SaslPassword = "<api-secret>";
            clientConfig.SslCaLocation = "probe"; // /etc/ssl/certs


            using var producer = new ProducerBuilder<Null, string>(clientConfig).Build();

            Console.WriteLine("Please enter the message you want to send");
            var input = Console.ReadLine();

            var message = new Message<Null, string>
            {
                Value = input
            };

            var deliveryReport = await producer.ProduceAsync("my-topic", message);
            Console.WriteLine($"Message delivered to {deliveryReport.TopicPartitionOffset}");

        }
    }
}
