namespace WebAPI.Models
{
    public class RegionModel
    {
        public long Code { get; set; }
        public string Name { get; set; }
        public RegionModel(long code, string name)
        {
            this.Code = code;
            this.Name = name;
        }
    }
}
