using AlexisCorePro.Domain.Enums;

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
    }
}
