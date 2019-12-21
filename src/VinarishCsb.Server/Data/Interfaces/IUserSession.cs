﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VinarishCsb.Server.Data.Interfaces
{
    public interface IUserSession
    {
        Guid UserId { get; set; }
        int TenantId { get; set; }
        List<string> Roles { get; set; }
        string UserName { get; set; }
        bool DisableTenantFilter { get; set; }
    }
}
