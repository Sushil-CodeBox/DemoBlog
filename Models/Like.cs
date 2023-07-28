using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoBlogCore.Models
{
    public class Like
    {
        [Key]
        public int id { get; set; }
       

        [ForeignKey("post")]
        public int PostRefID { get; set; }

        public Post post { get; set; }
    
    }
}
