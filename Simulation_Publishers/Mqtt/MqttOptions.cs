﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation_Publishers.Mqtt
{
    public class MqttOptions
    {
        public int CommunicationTimeout { get; set; }
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public int KeepAliveInterval { get; set; }
        public string ClientId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
