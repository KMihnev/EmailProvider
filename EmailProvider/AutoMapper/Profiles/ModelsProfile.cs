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

            CreateMap<User, UserSerializable>().ReverseMap();

            CreateMap<MessageSerializable, Message>();

            //TO DO много мазно
            CreateMap<Message, MessageSerializable>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Subject))
           .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
           .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
           .ForMember(dest => dest.DateOfCompletion, opt => opt.MapFrom(src => src.DateOfCompletion))
           .ForMember(dest => dest.Files, opt => opt.MapFrom(src => src.Files))
           .ForMember(dest => dest.ReceiverEmails, opt => opt.Ignore())
           .ForMember(dest => dest.SenderEmail, opt => opt.Ignore())
           .ForMember(dest => dest.SenderId, opt => opt.Ignore())
           .AfterMap((src, dest, ctx) =>
           {
               var incomingReceivers = src.IncomingMessages.Select(im => im.Receiver.Email);
               var outgoingReceivers = src.OutgoingMessages.Select(om => om.ReceiverEmail);
               var innerReceivers = src.InnerMessages.Select(im => im.Receiver.Email);

               var allReceivers = incomingReceivers
                   .Concat(outgoingReceivers)
                   .Concat(innerReceivers)
                   .Distinct()
                   .ToList();

               dest.ReceiverEmails = allReceivers;

               if (src.OutgoingMessages.Any())
               {
                   var firstOutgoing = src.OutgoingMessages.First();
                   dest.SenderId = firstOutgoing.SenderId;
                   dest.SenderEmail = firstOutgoing.Sender.Email;
               }
               else if (src.InnerMessages.Any())
               {
                   var firstInner = src.InnerMessages.First();
                   dest.SenderId = firstInner.SenderId;
                   dest.SenderEmail = firstInner.Sender.Email;
               }
               else if (src.IncomingMessages.Any())
               {
                   var firstIncoming = src.IncomingMessages.First();
                   dest.SenderEmail = firstIncoming.SenderEmail;
               }
           });
        }
    }
}
