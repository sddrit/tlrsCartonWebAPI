namespace tlrsCartonManager.DAL.Models.Base
{
    public interface ISoftDelete
    {
        public bool Deleted { get; set; }
    }
}
