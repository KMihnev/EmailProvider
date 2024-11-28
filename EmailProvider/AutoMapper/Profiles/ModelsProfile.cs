using AutoMapper;
using EmailServiceIntermediate.Models.Serializables;
using EmailServiceIntermediate.Models;

namespace EmailProvider.AutoMapper.Profiles
{
    public class ModelsProfile : Profile
    {
        public ModelsProfile()
        {
            // Country Mapping
            CreateMap<Country, CountrySerializable>();
            CreateMap<CountrySerializable, Country>();

            // User Mapping
            CreateMap<User, UserSerializable>();
            CreateMap<UserSerializable, User>();

            // File Mapping
            CreateMap<EmailServiceIntermediate.Models.File, FileSerializable>();
            CreateMap<FileSerializable, EmailServiceIntermediate.Models.File>();

            // Status Mapping
            CreateMap<Status, StatusSerializable>();
            CreateMap<StatusSerializable, Status>();

            // Message Mapping
            CreateMap<Message, MessageSerializable>()
                .ForMember(dest => dest.Files, opt => opt.MapFrom(src => src.Files))
                .ForMember(dest => dest.ReceiverEmail, opt => opt.MapFrom(src => src.Receiver.Email))
                .ForMember(dest => dest.SenderId, opt => opt.MapFrom(src => src.Sender.Id))
                .ForMember(dest => dest.StatusID, opt => opt.MapFrom(src => src.Status.Id));

            CreateMap<MessageSerializable, Message>()
                .ForMember(dest => dest.Files, opt => opt.MapFrom(src => src.Files))
                .ForMember(dest => dest.Receiver, opt => opt.Ignore())
                .ForMember(dest => dest.Sender, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusID));

            // BulkIncomingMessage Mapping
            CreateMap<BulkIncomingMessage, BulkIncomingMessageSerializable>();
            CreateMap<BulkIncomingMessageSerializable, BulkIncomingMessage>();

            // BulkOutgoingMessage Mapping
            CreateMap<BulkOutgoingMessage, BulkOutgoingMessageSerializable>();
            CreateMap<BulkOutgoingMessageSerializable, BulkOutgoingMessage>();

            // Category Mapping
            CreateMap<Category, CategorySerializable>();
            CreateMap<CategorySerializable, Category>();
        }
    }
}
