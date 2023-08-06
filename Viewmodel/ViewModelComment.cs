using DemoBlogCore.Models;

namespace DemoBlogCore.Viewmodel
{
    public class ViewModelComment
    {
        public string? CommentText { get; set; }
        public int? PostRefID { get; set; }

        public string? UserID { get; set; }

        public string? UserName { get; set; }
    }

}
