using Application.Chats.DTOs;
using AutoMapper;
using Domain.Entities.ChatAggregate;

namespace Application.Chats.Mappings
{
    internal class ChatProfile : Profile
    {
        public ChatProfile()
        {

            CreateMap<Message, ChatMessageDto>().ReverseMap();
            CreateMap<ApplicationUser, ChatParticipantDto>().ReverseMap();
        }
    }
}
