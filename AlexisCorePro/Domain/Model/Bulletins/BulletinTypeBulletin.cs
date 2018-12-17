using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexisCorePro.Domain.Model
{
    public class BulletinTypeBulletin
    {
        public int Id { get; set; }

        public int BulletinId { get; set; }
        public virtual Bulletin Bulletin { get; set; }

        public int BulletinTypeId { get; set; }
        public virtual BulletinType BulletinType { get; set; }
    }
}
