using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexisCorePro.Domain.Model
{
    public class Document : BaseModel
    {
        public string Name { get; set; }

        public string Revision { get; set; }

        public string Sender { get; set; }

        public string Transmitter { get; set; }

        public string FileUrl { get; set; }

        public DateTime Date { get; set; }

        public int DocumentTypeId { get; set; }
        public virtual DocumentType DocumentType { get; set; }
    }
}
