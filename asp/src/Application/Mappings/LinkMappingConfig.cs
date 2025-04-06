using Mapster;
using Domain.Entities;
using Application.Contexts.Links.Dtos;

namespace Application.Mappings;
public class LinkMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Link, LinkDto>()
            .Map(dest => dest.UserName, src => src.User!.UserName);
    }
}
