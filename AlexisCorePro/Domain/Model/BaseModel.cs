﻿using System;
using System.ComponentModel.DataAnnotations;

namespace AlexisCorePro.Domain.Model
{
    public abstract class BaseModel
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public string CreatedById { get; set; }
        public string UpdatedById { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
