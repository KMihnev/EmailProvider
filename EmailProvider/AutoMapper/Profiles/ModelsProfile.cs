using AutoMapper;
using EmailProvider.Models.Serializables;
using EmailServiceIntermediate.Models;

namespace EmailProvider.AutoMapper.Profiles
{
    public class ModelsProfile : Profile
    {
        public ModelsProfile()
        {
            CreateMap<Country, CountrySerializable>();

            CreateMap<User, UserSerializable>();

            CreateMap<EmailServiceIntermediate.Models.File, FileSerializable>();

            CreateMap<IncomingMessage, IncomingMessasgeSerializable>();

            CreateMap<OutgoingMessage, OutgoingMessageSerializable>();

            CreateMap<BulkIncomingMessage, BulkIncomingMessageSerializable>();

            CreateMap<BulkOutgoingMessage, BulkOutgoingMessageSerializable>();

            CreateMap<Category, CategorySerializable>();
        }
    }
}
