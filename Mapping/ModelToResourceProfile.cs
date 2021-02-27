using AutoMapper;
using seventh.Models;
using seventh.Resources;

namespace seventh.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<CreateServerResource, Server>();
            CreateMap<Server, ServerResource>();
            CreateMap<ServerResource, Server>();
            CreateMap<Videos, CreateVideoResource>();
            CreateMap<CreateVideoResource, Videos>();


        }

    }
}