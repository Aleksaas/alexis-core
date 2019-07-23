namespace AlexisCorePro.Domain.Model
{
    public class BulletinAttachment : BaseModel
    {
        public string FileUrl { get; set; }

        public int BulletinId { get; set; }
        public virtual Bulletin Bulletin { get; set; }
    }
}
