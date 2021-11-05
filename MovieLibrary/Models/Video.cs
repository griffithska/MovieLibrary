using System.Collections.Generic;
using CsvHelper.Configuration;

namespace MovieLibrary.Models
{
    public class Video : Media
    {
        // constructor
        public Video()
        {
            Regions = new List<int>();
        }

        public override string Format { get; set; }
        public override int Length { get; set; }
        public override List<int> Regions { get; set; }

        // public method
        public override string Display()
        {
            return $"Id: {MediaId}  Title: {Title}  Format: {Format}  Length: {Length}  Regions: {string.Join(", ", Regions)}";
        }
    }
    public class VideoMap : ClassMap<Video>
    {
        public VideoMap()
        {
            Map(m => m.MediaId).Index(0).Name("videoId");
            Map(m => m.Title).Index(1).Name("title");
            Map(m => m.Format).Index(2).Name("format");
            Map(m => m.Length).Index(3).Name("length");
            Map(m => m.Regions).Index(4).Name("regions");
        }
    }
}