namespace soft20181_starter.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public string FullDate { get; set; }

        public string Time { get; set; }
        
        public string Location { get; set; }
        public string FullLocation { get; set; }

        public string LocationUrl { get; set; }

        public string Brief { get; set; }
        public string Image { get; set; }

        public string FullDescription { get; set; }

        public string Category { get; set; }
        public string AgeGroup { get; set; }
        public string Price { get; set; }

    }
}
