using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieLibrary.DataModels
{
    public class MovieGenre
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id {get;set;}
        [Required]
        public virtual Movie Movie { get; set; }
        [Required]
        public virtual Genre Genre { get; set; }
    }
}
