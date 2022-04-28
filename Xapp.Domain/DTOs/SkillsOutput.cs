namespace Xapp.Web.Models
{
    public class SkillsOutput
    {
        public string Id { get; set; }
        public string Values { get; set; }
        public string SkillId { get; set; }
        public int User { get; set; }
        public string Nombre { get; set; }
        public int Nivel { get; set; }
        public bool IsActive { get; set; }
    }
}