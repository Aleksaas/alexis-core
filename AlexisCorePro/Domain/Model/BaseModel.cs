using System;
using System.ComponentModel.DataAnnotations;

namespace AlexisCorePro.Domain.Model
{
    public abstract class BaseModel
    {
        public int Id { get; set; }

        public bool IsPublished { get; set; }

        public bool HasOwner { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; }

        public int? CreatedById { get; set; }
        public int? UpdatedById { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
