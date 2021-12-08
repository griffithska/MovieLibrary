using System;
using System.Collections.Generic;
using NLog;
using NLog.Web;
using System.Linq;
using MovieLibrary.DataModels;
using MovieLibrary.Context;
using Microsoft.EntityFrameworkCore;

namespace MovieLibrary
{
    public class UserManager
    {
        private readonly Logger _logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        public List<User> UserList { get; set; }

        public UserManager()
        {
            UserList = new List<User>();
        }

        public static List<User> UserSearch(int? age, string gender, string zipCode, string occupation, MovieContext db)
        {
                return db.Users
                    .Include(o => o.Occupation)
                    .Where(u => u.Age == age || age == null)
                    .Where(u => u.Gender.ToLower() == gender.ToLower() || gender == "")
                    .Where(u => u.ZipCode.ToLower() == zipCode.ToLower() || zipCode == "")
                    .Where(u => u.Occupation.Name.ToLower() == occupation.ToLower() || occupation == "")
                    .OrderBy(u => u.Id)
                    .ToList();
        }

        public static User UserById(long userId, MovieContext db)
        {
                return db.Users.Where(u => u.Id == userId).FirstOrDefault();
        }

        public static void AddUser(User user, MovieContext db)
        {
                db.Users.Add(user);
                db.SaveChanges();
        }

        public static void ListUsers(List<User> users)
        {
            if (users.Count <= 10)
            {
                Console.WriteLine("{0} users matched", users.Count);
                users.ForEach(x => Console.WriteLine(x.Display()));
            }
            else
            {
                Console.WriteLine("{0} users matched", users.Count);
                for (var i = 0; i < users.Count; i += 10)
                {
                    users.Skip(i).Take(10).ToList().ForEach(x => Console.WriteLine(x.Display()));
                    Console.WriteLine("Press any key to display next 10 users");
                    Console.ReadKey();
                }
            }
        }
    }
}
