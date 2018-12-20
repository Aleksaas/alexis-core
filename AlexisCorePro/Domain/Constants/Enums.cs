using Localization.Resources;
using System.ComponentModel.DataAnnotations;

namespace AlexisCorePro.Domain.Enums
{
    public enum ResourceType
    {
        Ship,
        Customer,
        Equipment,
        Report,
        Bulletin,
        Document,
        Task,
        User,
        SparePart
    }

    public enum EquipmentCriticality
    {
        Normal,
        Important,
        Critical
    }

    public enum AuditAction
    {
        Created,
        Updated,
        Deleted
    }

    public enum TaskOrigin
    {
        Bulletin,
        ServiceReport,
        SmartMonitoring 
    }

    public enum TaskCriticality
    {
        Normal,
        Important,
        Critical
    }

    public enum TaskDifficulty
    {
        Low,
        Normal,
        High
    }

    public enum TaskStatus
    {
        Created,
        InWork,
        Postponed,
        Completed,
        Cancelled
    }

    public enum TaskMaintananceType
    {
        Guarantee,
        Inspection,
        Installation,
        ServiceOther,
        UpgradeAndServices,
        SafetyAndQuality
    }

    public enum EquipmentStatus
    {
        InProgress,
        Cancelled,
        Postponed
    }

    public enum ServiceReportStatus
    {
        Draft,
        Published,
        Deleted
    }

    public enum AisVesselType
    {
        [Display(Name = nameof(SharedResource.AisVesselTypeOther), ResourceType = typeof(SharedResource))]
        Other
    }

    public enum NotificationType
    {
        ServiceReportCreated,
        BulletinCreated,
        ShipCreated,
        ShipStatusChanged,
        EquipmentCreated,
        EquipmentCriticalityChanged,
        TaskCreated,
        TaskCriticalityChanged
    }
}
