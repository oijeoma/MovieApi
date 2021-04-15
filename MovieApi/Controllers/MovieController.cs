using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieApi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private List<MovieMetaData> _postedMovies;
        const string statsFile = "\\netcoreapp3.1\\DataFiles\\stats.csv";
        const string movieFile = "\\netcoreapp3.1\\DataFiles\\metadata.csv";
        string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);

        private readonly ILogger<MovieController> _logger;

        public MovieController(ILogger<MovieController> logger)
        {
            _postedMovies = new List<MovieMetaData>();
            _logger = logger;
        }
        // GET: api/<MovieController>
        [HttpGet("movies/stats")]
        public IEnumerable<MovieStats> Get()
        {

            
            filePath = Directory.GetParent(filePath).FullName;
            filePath += statsFile;
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<MovieStats>();
                var result = records.OrderBy(x => x.watchDurationMs);
                return result.ToList();
            }
        }

        // GET metadata/:movieId
        [HttpGet("metadata/{movieId}")]
        public IActionResult Get(int movieId)
        {
            filePath = Directory.GetParent(filePath).FullName;
            filePath += movieFile;

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<MovieMetaData>();
                var result = _postedMovies.Where(x => x.MovieId == movieId);
                if (result != null)
                {
                    var retRecords = records.Where(x => x.MovieId == movieId).OrderBy(x => x.MovieId).ThenBy(x => x.Language);
                    return Ok(retRecords.ToList());
                }
                else
                { return NotFound(); }
            }
           
        }

        // POST api/<MovieController>
        [HttpPost("metadata")]
        public void Post([FromBody] MovieMetaData value)
        {
            _postedMovies.Add(value); 
            
        }       
    }
}
