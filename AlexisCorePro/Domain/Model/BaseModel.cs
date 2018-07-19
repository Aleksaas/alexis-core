using System;
using System.ComponentModel.DataAnnotations;

namespace AlexisCorePro.Domain.Model
{
    public abstract class BaseModel
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdateAt { get; set; } = DateTime.Now;

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
