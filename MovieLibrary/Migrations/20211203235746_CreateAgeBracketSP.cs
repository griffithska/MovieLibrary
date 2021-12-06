using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieLibrary.Migrations
{
    public partial class CreateAgeBracketSP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

			var sp = @"CREATE PROCEDURE dbo.TopRatedMovieByAgeBracket

			AS

			DROP TABLE IF EXISTS  #AgeBrackets

			SELECT*
			INTO #AgeBrackets
			FROM(
				SELECT '1-9 Years Old' AS AgeBracket, 1 AS MinAge, 9 AS MaxAge
				UNION ALL
				SELECT '10-19 Years Old' AS AgeBracket, 10 AS MinAge, 19 AS MaxAge
				UNION ALL
				SELECT '20-29 Years Old' AS AgeBracket, 20 AS MinAge, 29 AS MaxAge
				UNION ALL
				SELECT '30-39 Years Old' AS AgeBracket, 30 AS MinAge, 39 AS MaxAge
				UNION ALL
				SELECT '40-49 Years Old' AS AgeBracket, 40 AS MinAge, 49 AS MaxAge
				UNION ALL
				SELECT '50-59 Years Old' AS AgeBracket, 50 AS MinAge, 59 AS MaxAge
				UNION ALL
				SELECT '60-69 Years Old' AS AgeBracket, 60 AS MinAge, 69 AS MaxAge
				UNION ALL
				SELECT '70-79 Years Old' AS AgeBracket, 70 AS MinAge, 79 AS MaxAge
				UNION ALL
				SELECT '80-89 Years Old' AS AgeBracket, 80 AS MinAge, 89 AS MaxAge
			) x

			DROP TABLE IF EXISTS  #Ratings

			SELECT
				ab.AgeBracket,
				m.Title,
				COUNT(um.Rating) AS TotalRatings,
				AVG(CAST(um.Rating as numeric(3, 2))) AS AverageRating,
				 ROW_NUMBER() OVER(PARTITION BY ab.AgeBracket ORDER BY AVG(CAST(um.Rating as numeric(3, 2))) desc) AS RowNum,
				 MAX(COUNT(um.Rating)) OVER(PARTITION BY ab.AgeBracket) MaxRatingsInAgeBracket
			INTO #Ratings
			FROM Users u
			INNER JOIN #AgeBrackets ab
				ON u.Age BETWEEN ab.MinAge AND ab.MaxAge
			INNER JOIN UserMovies um
				ON u.Id = um.UserId
			INNER JOIN Movies m
				ON um.MovieId = m.Id
			GROUP BY ab.AgeBracket, m.Title
			ORDER BY ab.AgeBracket, AverageRating desc, m.Title

			SELECT* FROM(
				SELECT*
				FROM #Ratings
				WHERE RowNum = 1
				AND MaxRatingsInAgeBracket < 5

				UNION ALL

				SELECT DISTINCT
					FIRST_VALUE(AgeBracket) OVER(PARTITION BY AgeBracket ORDER BY AverageRating desc),
					FIRST_VALUE(Title) OVER(PARTITION BY AgeBracket ORDER BY AverageRating desc),
					FIRST_VALUE(TotalRatings) OVER(PARTITION BY AgeBracket ORDER BY AverageRating desc),
					FIRST_VALUE(AverageRating) OVER(PARTITION BY AgeBracket ORDER BY AverageRating desc),
					FIRST_VALUE(RowNum) OVER(PARTITION BY AgeBracket ORDER BY AverageRating desc),
					FIRST_VALUE(MaxRatingsInAgeBracket) OVER(PARTITION BY AgeBracket ORDER BY AverageRating desc)
				FROM #Ratings
				WHERE TotalRatings >= 5
			) x
			ORDER BY AgeBracket


			GO";

			migrationBuilder.Sql(sp);

		}

		protected override void Down(MigrationBuilder migrationBuilder)
        {
			var dropProcSql = "DROP PROCEDURE dbo.TopRatedMovieByAgeBracket";
			migrationBuilder.Sql(dropProcSql);
		}
    }
}
