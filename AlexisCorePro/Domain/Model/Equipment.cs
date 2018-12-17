using AlexisCorePro.Domain.Enums;
using AlexisCorePro.Infrastructure.Helpers;
using DelegateDecompiler;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlexisCorePro.Domain.Model
{
    public class Equipment : BaseModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Position { get; set; }

        public string SerialNumber { get; set; }

        public int RunningHours { get; set; }

        public EquipmentCriticality Criticality { get; set; }

        public int ShipId { get; set; }

        public int EquipmentTypeId { get; set; }

        public virtual Ship Ship { get; set; }

        public virtual List<BulletinEquipment> BulletinEquipments { get; set; }

        [Computed]
        public virtual bool IsUpdatedInMonth(DateTime date)
        {
            var range = DateHelper.GetFirstAndLastDay(date);
            return UpdateAt <= range.DateTo && UpdateAt >= range.DateFrom;
        }

        [Computed]
        [NotMapped]
        public virtual bool IsCreatedInMonth
        {
            get
            {
                var range = DateHelper.GetFirstAndLastDay(DateTime.Now);
                return CreatedAt <= range.DateTo && CreatedAt >= range.DateFrom;
            }
        }
    }
}
