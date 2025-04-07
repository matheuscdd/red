using System.Text.RegularExpressions;
using Domain.Services;
using Domain.Entities;
using MaxMind.GeoIP2;
using MaxMind.GeoIP2.Exceptions;
using MaxMind.GeoIP2.Responses;
using System.Net;

namespace Api.Services;

public partial class GeoIpService: IGeoIpService
{
    private readonly DatabaseReader _reader;

    public GeoIpService(string geoIpPath)
    {
        _reader = new DatabaseReader(geoIpPath);
    }

    public IpInfo? GetIpInfo(IPAddress ipAddress)
    {
        CityResponse result;
        try
        {
            result = _reader.City(ipAddress);
        }
        catch (AddressNotFoundException)
        {
            return null;
        }

        if (result?.City == null && result?.Country == null)
        {
            return null;
        }
        
        return new IpInfo{
            Country = $"{result.Country.Name} - [{result.Country.IsoCode}]",
            Region = $"{result.MostSpecificSubdivision.Name} - [{result.MostSpecificSubdivision.IsoCode}]",
            City = result.City.Name
        };
    }
}