using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka_Producer
{
    public class Settings
    {
        public string BootstrapServers { get; set; } = string.Empty;
        public string SaslUsername { get; set; } = string.Empty;
        public string SaslPassword { get; set; } = string.Empty;
        public string SslCaLocation { get; set; } = string.Empty;

    }
}
