using AutoMapper;
using Demo.Entities;
using Demo.Models;

namespace Demo.BusinessLogic.AutoMapperProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TodoItemCreateOrUpdateDTO, TodoItem>(MemberList.Source);
            CreateMap<TodoItem, TodoItemDTO>(MemberList.Source);
        }
    }
}