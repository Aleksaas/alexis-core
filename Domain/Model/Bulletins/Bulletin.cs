using System;
using System.Collections.Generic;

namespace AlexisCorePro.Domain.Model
{
    public class Bulletin : BaseModel
    {
        public string Name { get; set; }

        public string IssueReason { get; set; }

        public DateTime Date { get; set; }

        public string TimeFrame { get; set; }

        public virtual List<BulletinEquipment> BulletinEquipments { get; set; }

        public virtual List<BulletinAttachment> Attachments { get; set; }

        public virtual List<BulletinTypeBulletin> BulletinTypes { get; set; }
    }
}
