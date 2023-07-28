using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoBlogCore.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string CommentText { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("post")]
        public int PostRefID { get; set; }
        public Post post { get; set; }       
       
    }
}
