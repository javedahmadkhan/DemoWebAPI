//
// Copyright:   Copyright (c) 
//
// Description: Base Entity Class
//
// Project: 
//
// Author:  Accenture
//
// Created Date:  
//
using Demo.Entities.DomainEntity.Contract;
using System;

namespace Demo.Entities.DomainEntity.Base
{
    /// <summary>
    /// Base Entity Class
    /// </summary>
    public abstract class BaseEntity : IEntity, IAudit
    {
        protected BaseEntity()
        {
            CreatedBy = "";
            UpdatedBy = "";
            CreatedDate = DateTime.UtcNow;
            UpdatedDate = DateTime.UtcNow;
        }

        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public abstract int Id { get; set; }
    }
}
