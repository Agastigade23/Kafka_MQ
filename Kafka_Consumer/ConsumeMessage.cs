using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka_Consumer
{
    public class ConsumeMessage
    {
        public void ReadMessage()
        {
            var clientConfig = new ClientConfig();

            clientConfig.BootstrapServers = "<bootstrap-host-port-pair>";
            clientConfig.SecurityProtocol = Confluent.Kafka.SecurityProtocol.SaslSsl;
            clientConfig.SaslMechanism = Confluent.Kafka.SaslMechanism.Plain;
            clientConfig.SaslUsername = "<api-key>";
            clientConfig.SaslPassword = "<api-secret>";
            clientConfig.SslCaLocation = "probe"; // /etc/ssl/certs

            var consumerConfig = new ConsumerConfig(clientConfig);
            consumerConfig.GroupId = "wiki-edit-stream-group-1";
            consumerConfig.AutoOffsetReset = AutoOffsetReset.Earliest;
            consumerConfig.EnableAutoCommit = false;

            using var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();

            consumer.Subscribe("my-topic");

            try
            {
                while (true)
                {
                    var consumeResult = consumer.Consume();
                    Console.WriteLine($"Message received from {consumeResult.TopicPartitionOffset}: {consumeResult.Message.Value}");
                }
            }
            catch (OperationCanceledException)
            {
                // The consumer was stopped via cancellation token.
            }
            finally
            {
                consumer.Close();
            }

            Console.ReadLine();

        }
    }
}
