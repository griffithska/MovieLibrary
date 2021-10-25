using System.Collections.Generic;
using CsvHelper.Configuration;

namespace MovieLibrary.Models
{
    public class VideoRaw : Media
    {
        public string Format { get; set; }
        public int Length { get; set; }
        public string Regions { get; set; }

        // public method
        public override string Display()
        {
            return $"Id: {MediaId}  Title: {Title}  Format: {Format}  Length: {Length}  Regions: {Regions}";
        }
    }
    public class Video : Media
    {
        // constructor
        public Video()
        {
            Regions = new List<int>();
        }

        public string Format { get; set; }
        public int Length { get; set; }
        public List<int> Regions { get; set; }

        // public method
        public override string Display()
        {
            return $"Id: {MediaId}  Title: {Title}  Format: {Format}  Length: {Length}  Regions: {string.Join(", ", Regions)}";
        }
    }
    public class VideoMap : ClassMap<VideoRaw>
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