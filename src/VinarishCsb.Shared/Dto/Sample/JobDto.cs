﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VinarishCsb.Shared.Dto
{
    public class JobDto
    {
        public int Id { get; set; }
        public JobStatuses Status { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdated { get; set; }
    }

    public enum JobStatuses
    {
        Todo,
        Started,
        Completed
    }
}