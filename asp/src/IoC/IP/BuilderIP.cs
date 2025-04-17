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
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;

            options.KnownNetworks.Clear();
            options.KnownProxies.Clear();
        }); 

        return builder;
     }
}