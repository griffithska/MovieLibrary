using System;
using System.Collections.Generic;

namespace MovieLibrary.DataModels
{
    public class User
    {
        public long Id { get; set; }
        public long Age { get; set; }
        public string Gender { get; set; }
        public string ZipCode { get; set; }

        public virtual Occupation Occupation { get; set; }
        public virtual ICollection<UserMovie> UserMovies {get;set;}

        public string Display()
        {
            return string.Format($"Id: {Id,-5}  Age: {Age,-5} Gender: {Gender,-5} ZipCode: {ZipCode,-8} Occupation: {Occupation.Name,-10} ");
        }
    }
}
