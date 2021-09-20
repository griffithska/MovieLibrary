using System;

namespace MovieLibrary
{
    public class Logic
    {
        //var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        public bool DuplicateTitle(string title)
        {
            //if (Movies.title.ConvertAll(m => m.title.ToLower()).Contains(title.ToLower()))
            //works if I just test with a static string but not sure how to make it work with my dataset
            string testTitle = "Toy Story (1995)";
            if (testTitle.ToLower().Contains(title.ToLower()))
            {
                //logger.Info("Duplicate movie title {Title}", title);
                return true;
            }
            return false;
        }
        
        // public bool DuplicateTitle2(Movies m)
        // {
        //     if (Movies.title.ConvertAll(m => m.title.ToLower()).Contains(title.ToLower()))
        //     {
        //         //logger.Info("Duplicate movie title {Title}", title);
        //         return true;
        //     }
        //     return false;
        // }
    }
}