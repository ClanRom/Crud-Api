using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Roman
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql("Host=localhost;Database=Movies;Username=postgres;Password=Flash626"));
            builder.Services.AddScoped<MovieController>();

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.MapGet("/movies", async (MovieController movieController) => await movieController.GetAllMoviesAsync());

            app.MapGet("/movies/{id:int}", async (int id, MovieController movieController) =>
            {
                Movies? movie = await movieController.GetMovieByIdAsync(id);
                return movie == null ? Results.NotFound(new { message = "Фильм не найден" }) : Results.Json(movie);
            });

            app.MapDelete("/movies/{id:int}", async (int id, MovieController movieController) =>
            {
                Movies? movie = await movieController.DeleteMovieAsync(id);
                return movie == null ? Results.NotFound(new { message = "Фильм не найден" }) : Results.StatusCode(204);
            });

            app.MapPost("/movies", async ([FromBody] Movies movie, MovieController movieController) =>
            {
                try
                {
                    var createdMovie = await movieController.CreateMovieAsync(movie);
                    return Results.Created($"/movies/{createdMovie.Id}", createdMovie);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { message = ex.Message });
                }
            });

            app.MapPut("/movies/{id:int}", async (int id, [FromBody] Movies userData, MovieController movieController) =>
            {
                try
                {
                    Movies? updatedMovie = await movieController.UpdateMovieAsync(id, userData);
                    return updatedMovie == null ? Results.NotFound(new { message = "Фильм не найден" }) : Results.Json(updatedMovie);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { message = ex.Message });
                }
            });

            app.Run();
        }

    }
}
