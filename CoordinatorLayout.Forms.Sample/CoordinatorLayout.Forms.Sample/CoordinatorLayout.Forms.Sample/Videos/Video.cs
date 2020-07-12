namespace CoordinatorLayout.XamarinForms.Sample.Videos
{
    public class Video
    {
        public Video(string name, string source, string thumbnail)
        {
            Source = source;
            Thumbnail = thumbnail;
            Name = name;
        }

        public string Name { get; }
        public string Source { get; }
        public string Thumbnail { get; }
    }
}