using System;
using System.Collections.Generic;
using NLog;
using NLog.Web;
using System.Linq;
using MovieLibrary.DataModels;
using MovieLibrary.Context;

namespace MovieLibrary
{
    public class OccupationManager
    {
        private readonly Logger _logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        public List<Occupation> MovieList { get; set; }

        public OccupationManager()
        {
            MovieList = new List<Occupation>();
        }

        public static List<Occupation> OccupationSearch(string name, MovieContext db)
        {
                return db.Occupations
                    .Where(o => o.Name == name)
                    .OrderBy(o => o.Name)
                    .ToList();
        }

        public static List<long> OccupationIdDump()
        {
            using (var db = new MovieContext())
            {
                return db.Occupations
                    .OrderBy(o => o.Name)
                    .Select(o => o.Id)
                    .ToList();
            }
        }
        public static void AddOccupation(string occ)
        {
            using (var db = new MovieContext())
            {
                Occupation occupation = new()
                {
                    Name = occ
                };
                db.Occupations.Add(occupation);
                db.SaveChanges();
            }
        }

        public static void ListOccupations(List<Occupation> occ)
        {
            if (occ.Count <= 10)
            {
                Console.WriteLine("{0} users matched", occ.Count);
                occ.ForEach(x => Console.WriteLine(x.Display()));
            }
            else
            {
                Console.WriteLine("{0} occupations matched", occ.Count);
                for (var i = 0; i < occ.Count; i += 10)
                {
                    occ.Skip(i).Take(10).ToList().ForEach(x => Console.WriteLine(x.Display()));
                    Console.WriteLine("Press any key to display next 10 occupations");
                    Console.ReadLine();
                }
            }
        }
    }
}
