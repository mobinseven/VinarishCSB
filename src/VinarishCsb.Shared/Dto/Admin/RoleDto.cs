﻿using System;
using System.Collections.Generic;

namespace VinarishCsb.Shared.Dto
{
    public class RoleDto
    {
        public string Name { get; set; }

        public List<string> Permissions { get; set; }

        public string FormattedPermissions
        {
            get
            {
                return String.Join(", ", Permissions.ToArray());
            }
        }
    }
}