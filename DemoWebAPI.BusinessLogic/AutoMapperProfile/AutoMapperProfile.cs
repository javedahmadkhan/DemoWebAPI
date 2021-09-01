//
// Copyright:   Copyright (c) 
//
// Description: Auto Mapper Profile Class
//
// Project: 
//
// Author:  Accenture
//
// Created Date:  
//
using AutoMapper;
using Demo.Entities;
using Demo.Models;

namespace Demo.BusinessLogic.AutoMapperProfile
{
    /// <summary>
    /// This class is used for auto mapper profile
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// Constructor for Auto Mapper Profile
        /// </summary>
        public AutoMapperProfile()
        {
            CreateMap<TodoItemCreateOrUpdateDTO, TodoItem>(MemberList.Source);
            CreateMap<TodoItem, TodoItemDTO>(MemberList.Source);
        }
    }
}