using AutoMapper;
using EmailServiceIntermediate.Models.Serializables;
using EmailServiceIntermediate.Models;

namespace EmailServiceIntermediate.AutoMapper.Profiles
{
    public class ModelsProfile : Profile
    {
        public ModelsProfile()
        {
            CreateMap<BulkIncomingMessage, BulkIncomingMessageSerializable>().ReverseMap();
            CreateMap<BulkOutgoingMessage, BulkIncomingMessageSerializable>().ReverseMap();
            CreateMap<Category, CategorySerializable>().ReverseMap();
            CreateMap<Country, CountrySerializable>().ReverseMap();
            CreateMap<Models.File, FileSerializable>().ReverseMap();

            CreateMap<SendMessageSerializable, Message>().ReverseMap();

            CreateMap<User, UserSerializable>().ReverseMap();
        }
    }
}
