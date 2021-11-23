using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieLibrary.DataModels
{
    public class User
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public long Age { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string ZipCode { get; set; }

        public virtual Occupation Occupation { get; set; }
        public virtual ICollection<UserMovie> UserMovies {get;set;}

        public string Display()
        {
            return string.Format($"Id: {Id,-5}  Age: {Age,-5} Gender: {Gender,-5} ZipCode: {ZipCode,-8} Occupation: {Occupation.Name,-10} ");
        }
    }
}
