using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.HttpOverrides;
using System.Net;

namespace IoC.IP;

public static class BuilderIP
{
     public static WebApplicationBuilder AddIPConf(this WebApplicationBuilder builder)
     {
        builder.Services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = 
                ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;

            // Se quiser permitir todos os proxies (dev only):
            // options.KnownNetworks.Clear(); // Remove restrições de IP
            options.KnownProxies.Clear();
            options.KnownNetworks.Add(new Microsoft.AspNetCore.HttpOverrides.IPNetwork(IPAddress.Parse("192.168.1.107"), 16)); // sua rede interna

        }); 

        return builder;
     }
}