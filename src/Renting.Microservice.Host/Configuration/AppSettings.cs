using System;

[assembly: CLSCompliant(false)]

namespace Renting.Microservice.Host.Configuration
{
    internal sealed class AppSettings
    {
        public string JwtAuthority { get; set; }
    }
}
