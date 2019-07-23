using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexisCorePro.Domain.Model
{
    public class BulletinEquipment
    {
        public int Id { get; set; }

        public int BulletinId { get; set; }
        public virtual Bulletin Bulletin { get; set; }

        public int EquipmentId { get; set; }
        public virtual Equipment Equipment { get; set; }
    }
}
