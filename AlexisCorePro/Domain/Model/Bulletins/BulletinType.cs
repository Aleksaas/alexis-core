using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexisCorePro.Domain.Model
{
    public class BulletinType : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual List<BulletinTypeBulletin> BulletinTypes { get; set; }
    }
}
