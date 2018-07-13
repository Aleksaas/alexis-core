using AlexisCorePro.Domain.Enums;

namespace AlexisCorePro.Domain.Model
{
    public class Equipment : BaseModel
    {
        public string Name { get; set; }

        public EquipmentCriticality Criticality { get; set; }

        public virtual Ship Ship { get; set; }
    }
}
