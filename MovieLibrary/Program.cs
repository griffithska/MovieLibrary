using System;
using System.IO;
using System.Collections.Generic;
using NLog.Web;
using System.Linq;
using MovieLibrary.Models;
using MovieLibrary.Files;

namespace MovieLibrary
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            logger.Info("Program started");

            string movieFile;
            string showFile;
            string videoFile;

            Console.WriteLine("Please enter title to search:");
            Console.WriteLine("Titles with multiple results: 'Toy Story (1995)' 'Breaking Bad'");
            string input = Console.ReadLine();

            logger.Info("User input: {input}", input);


            movieFile = Path.Combine(Environment.CurrentDirectory, "data", "movies.csv");
            showFile = Path.Combine(Environment.CurrentDirectory, "data", "shows.csv");
            videoFile = Path.Combine(Environment.CurrentDirectory, "data", "videos.csv");
            // make sure movie file exists
            if (!File.Exists(movieFile) || !File.Exists(showFile) || !File.Exists(videoFile))
                {
                    Console.WriteLine("File does not exist");
                    logger.Error("File does not exist: {File}", movieFile);
                }
                else
                {

                List<MovieRaw> movies = new List<MovieRaw>();
                movies = MovieFile.LoadFile(movieFile);

                List<ShowRaw> shows = new List<ShowRaw>();
                shows = ShowFile.LoadFile(showFile);

                List<VideoRaw> videos = new List<VideoRaw>();
                videos = VideoFile.LoadFile(videoFile);

                List<MovieRaw> movieResult = movies.Where(x => x.Title == input).ToList();
                List<ShowRaw> showResult = shows.Where(x => x.Title == input).ToList();
                List<VideoRaw> videoResult = videos.Where(x => x.Title == input).ToList();

                if (movieResult.Count() == 0 && showResult.Count() == 0 && videoResult.Count() == 0)
                {
                    Console.WriteLine("No results found for: {0}", input);
                }
                else
                {
                    movieResult.ForEach(x => Console.WriteLine("Movie: {0}", x.Display()));
                    showResult.ForEach(x => Console.WriteLine("Show: {0}", x.Display()));
                    videoResult.ForEach(x => Console.WriteLine("Video: {0}", x.Display()));
                }
            }

            logger.Info("Program ended");
        }
    }
}