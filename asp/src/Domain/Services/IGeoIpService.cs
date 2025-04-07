using Domain.Entities;
using System.Net;


namespace Domain.Services;

public interface IGeoIpService
{
    IpInfo? GetIpInfo(IPAddress ipAddress);
}

