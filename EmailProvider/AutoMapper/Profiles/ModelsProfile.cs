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

            CreateMap<Message, MessasgeSerializable>()
                .ForMember(dest => dest.Files, opt => opt.MapFrom(src => src.Files))
                .ForMember(dest => dest.Receiver, opt => opt.MapFrom(src => src.Receiver))
                .ForMember(dest => dest.Sender, opt => opt.MapFrom(src => src.Sender))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

            CreateMap<BulkIncomingMessage, BulkIncomingMessageSerializable>();

            CreateMap<BulkOutgoingMessage, BulkOutgoingMessageSerializable>();

            CreateMap<Category, CategorySerializable>();
        }
    }
}
