//
// Copyright:   Copyright (c) 
//
// Description: Audit Interface
//
// Project: 
//
// Author:  Accenture
//
// Created Date:  
//

using System;

namespace Demo.Entities.DomainEntity.Contract
{
    /// <summary>
    /// Audit Interface
    /// </summary>
    public interface IAudit
    {
        string CreatedBy { get; set; }

        DateTime? CreatedDate { get; set; }

        string UpdatedBy { get; set; }

        DateTime? UpdatedDate { get; set; }
    }
}
