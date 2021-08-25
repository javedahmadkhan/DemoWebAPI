using AutoMapper;
using Demo.Entities;
using Demo.Models;

namespace Demo.BusinessLogic.AutoMapperProfile
{
    /// <summary>
    /// 
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public AutoMapperProfile()
        {
            CreateMap<TodoItemCreateOrUpdateDTO, TodoItem>(MemberList.Source);
            CreateMap<TodoItem, TodoItemDTO>(MemberList.Source);
        }
    }
}