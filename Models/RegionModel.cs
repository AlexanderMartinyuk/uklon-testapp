namespace WebAPI.Models
{
    public class RegionModel
    {
        public RegionModel(long code, string name)
        {
            Code = code;
            Name = name;
        }

        public long Code { get; set; }
        public string Name { get; set; }
    }
}