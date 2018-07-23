using System;
using System.ComponentModel.DataAnnotations;

namespace AlexisCorePro.Domain.Model
{
    public abstract class BaseModel
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public int CreatedById { get; set; }
        public int UpdatedById { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
