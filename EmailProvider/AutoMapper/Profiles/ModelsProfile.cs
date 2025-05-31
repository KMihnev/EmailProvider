//Includes
using AutoMapper;
using EmailServiceIntermediate.Models.Serializables;
using EmailProvider.Models.Serializables;
using EmailServiceIntermediate.Models;
using File = EmailServiceIntermediate.Models.File;

namespace EmailServiceIntermediate.AutoMapper.Profiles
{
    //------------------------------------------------------
    //  ModelsProfile
    //------------------------------------------------------

    /// <summary>
    /// Профил за основни модели
    /// </summary>
    public class ModelsProfile : Profile
    {
        //Constructor
        public ModelsProfile()
        {
            CreateMap<BulkIncomingMessage, BulkIncomingMessageSerializable>().ReverseMap();
            CreateMap<BulkOutgoingMessage, BulkIncomingMessageSerializable>().ReverseMap();
            CreateMap<Country, CountryViewModel>().ReverseMap();
            CreateMap<File, FileViewModel>().ReverseMap();
            CreateMap<Folder, FolderViewModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<MessageRecipient, MessageRecipientSerializable>().ReverseMap();

            CreateMap<Message, EmailViewModel>()
                .ForMember(dest => dest.Recipients, opt => opt.MapFrom(src => src.MessageRecipients))
                .ReverseMap()
                .ForMember(dest => dest.MessageRecipients, opt => opt.MapFrom(src => src.Recipients));

            CreateMap<UserMessage, EmailListModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Message.Id))
                .ForMember(dest => dest.FromEmail, opt => opt.MapFrom(src => src.Message.FromEmail))
                .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Message.Subject ?? string.Empty))
                .ForMember(dest => dest.Body, opt => opt.MapFrom(src => src.Message.Body ?? string.Empty))
                .ForMember(dest => dest.DateOfRegistration, opt => opt.MapFrom(src => src.Message.DateOfRegistration ?? DateTime.MinValue))
                .ForMember(dest => dest.Direction, opt => opt.MapFrom(src => src.Message.Direction))
                .ForMember(dest => dest.Recipients, opt => opt.MapFrom(src => src.Message.MessageRecipients))
                .ForMember(dest => dest.FolderId, opt => opt.MapFrom(src => src.UserMessageFolders.FirstOrDefault() != null ? src.UserMessageFolders.First().FolderId : 0));
        }
    }
}
