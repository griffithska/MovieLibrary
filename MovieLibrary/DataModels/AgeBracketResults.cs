using System.ComponentModel.DataAnnotations;

namespace MovieLibrary.DataModels
{
    public partial class AgeBracketResult
    {
        [Key]
        public string AgeBracket { get; set; }
        public string Title { get; set; }
        public int TotalRatings { get; set; }
        public decimal AverageRating { get; set; }
        public long RowNum { get; set; }
        public int MaxRatingsInAgeBracket { get; set; }

        public string Display()
        {
            return string.Format($"{AgeBracket,-20} {AverageRating,-5:0.00}           {Title,-50} {TotalRatings,-5}");
        }
    } 
}