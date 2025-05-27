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
            CreateMap<Country, CountrySerializable>().ReverseMap();
            CreateMap<File, FileSerializable>().ReverseMap();
            CreateMap<Folder, FolderSerializable>().ReverseMap();
            CreateMap<User, UserSerializable>().ReverseMap();

            CreateMap<MessageSerializable, Message>();
            //TO DO много мазно
            CreateMap<Message, MessageSerializable>();
        }
    }
}
