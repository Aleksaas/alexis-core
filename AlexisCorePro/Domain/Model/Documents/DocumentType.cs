using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexisCorePro.Domain.Model
{
    public class DocumentType : BaseModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual List<Document> Documents { get; set; }
    }
}
