using Microsoft.EntityFrameworkCore;


namespace Roman
{
    public class MovieController
    {
        private readonly AppDbContext _dbContext;

        public MovieController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Movies>> GetAllMoviesAsync()
        {
            return await _dbContext.Movies.ToListAsync();
        }

        public async Task<Movies?> GetMovieByIdAsync(int id)
        {
            return await _dbContext.Movies.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Movies?> DeleteMovieAsync(int id)
        {
            var movie = await GetMovieByIdAsync(id);
            if (movie == null) return null;

            _dbContext.Movies.Remove(movie);
            await _dbContext.SaveChangesAsync();
            return movie;
        }

        public async Task<Movies> CreateMovieAsync(Movies movie)
        {
            if (string.IsNullOrWhiteSpace(movie.Title) || string.IsNullOrWhiteSpace(movie.Description))
            {
                throw new ArgumentException("Название и описание фильма обязательны.");
            }

            await _dbContext.Movies.AddAsync(movie);
            await _dbContext.SaveChangesAsync();
            return movie;
        }

        public async Task<Movies?> UpdateMovieAsync(int id, Movies movieData)
        {
            var movie = await GetMovieByIdAsync(id);
            if (movie == null) return null;

            if (string.IsNullOrWhiteSpace(movieData.Title) || string.IsNullOrWhiteSpace(movieData.Description))
            {
                throw new ArgumentException("Название и описание фильма обязательны.");
            }

            movie.Title = movieData.Title;
            movie.Description = movieData.Description;
            _dbContext.Movies.Update(movie);
            await _dbContext.SaveChangesAsync();
            return movie;
        }
    }
}
