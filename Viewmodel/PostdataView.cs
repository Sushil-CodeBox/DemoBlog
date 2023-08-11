namespace DemoBlogCore.Viewmodel
{
    public class PostdataView
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }

        public string? logedInUserID { get; set; }
    }
}
