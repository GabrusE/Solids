using System.Collections.Generic;
using System.Collections;
using MovieApi.Models;
using MovieApi.Repository;

namespace MovieApi.Services{

    public interface IMovieService{

        public IEnumerable<Movie> GetMovies();

        public Movie GetMovieByName(string name);

        public Movie GetMoviesByYear(int year);

        public void CreateMovie(Movie m);

        public void UpdateMovie(string name, Movie m);

        public void DeleteMovie(string name);

    }
}