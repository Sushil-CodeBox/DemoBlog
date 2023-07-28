using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoBlogCore.Models
{
    public class Post
    {
        [Key]
        public int id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public string Image { get; set; }       
        public string? UserRefId { get; set; }
        public int? CommentCount { get; set; }
        public int? LikeCount { get; set; }

        public IEnumerable<Like>? like { get; set; }
        public IEnumerable<Comment>? comments { get; set; }
    }
}
