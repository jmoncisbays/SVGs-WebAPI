using System.ComponentModel.DataAnnotations.Schema;

namespace SVGsWebAPI.Models
{
    public class SVG
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Specification { get; set; }
        public byte[] PNG { get; set; }
        [NotMapped]
        public string PNGBase64 { get; set; }
    }
}
