using System.Collections.Generic;
using MovieApi.Models;
using MySql.Data.MySqlClient;
using System;


namespace MovieApi.Repository {

public class MovieRepository : IMovieRepository{
 
    private static readonly List<Movie> movies = new List<Movie>(10)    {

        new Movie {Name = "Rambo", Genre="Action", Year=1998},
        new Movie {Name = "Rush Hour", Genre="Action", Year=2002},
        new Movie {Name = "The GodFather", Genre="Action", Year=1972}

    };

    private MySqlConnection _connection;
    public MovieRepository(){

        string connectionString="server=localhost;userid=csci330user;password=csci330pass;database=entertainment";
        _connection = new MySqlConnection(connectionString);
        _connection.Open();


    }

    ~MovieRepository(){
        _connection.Close();
    }
    public IEnumerable<Movie> GetAll(){
        
        var statement = "Select * from Movies";
        var command = new MySqlCommand(statement, _connection);
        var results = command.ExecuteReader();

        List<Movie> newList = new List<Movie>(20);

        while(results.Read()){
            Movie m = new Movie{
            Name = (string)results[1],
            Genre = (string)results[3],
            Year = (int)results[2]
            };
            newList.Add(m);

        }

        results.Close();
        return newList;
    }
    

    public Movie? GetMovieByName(string name){
        var statement = "Select* from Movies where Name = @newName";
        var command = new MySqlCommand(statement, _connection);
        command.Parameters.AddWithValue("@newName", name);

        var results = command.ExecuteReader();
        Movie m=null;
        if(results.Read()){
            m = new Movie{
            Name = (string)results[1],
            Genre = (string)results[3],
            Year = (int)results[2]
            };
            
        }
        results.Close();
        return m;
        
    }

    public void InsertMovie(Movie m){
        var statement = "Insert into Movies (Name, Year, Genre) Values(@n, @y, @g)";
        var command = new MySqlCommand(statement, _connection);
        command.Parameters.AddWithValue("@n", m.Name);
        command.Parameters.AddWithValue("@y", m.Year);
        command.Parameters.AddWithValue("@g", m.Genre);

        int result = command.ExecuteNonQuery();
        
        Console.WriteLine(result);

        //movies.Add(m);
    }

    public void UpdateMovie (string name, Movie movieIn){
        var statement = "Update Movies Set Name=@newName, Year=@newYear, Genre=@newGenre Where Name= @updateName";
        var command = new MySqlCommand(statement, _connection);
        command.Parameters.AddWithValue("@newName", movieIn.Name);
        command.Parameters.AddWithValue("@newYear", movieIn.Year);
        command.Parameters.AddWithValue("@newGenre", movieIn.Genre);
        command.Parameters.AddWithValue("@updateName", name);


        int result = command.ExecuteNonQuery();
        

    }

    public void DeleteMovie(string name){
    var statement = "Delete from Movies where Name = @newName";
        var command = new MySqlCommand(statement, _connection);
        command.Parameters.AddWithValue("@newName", name);

        var results = command.ExecuteReader();
        Movie m=null;
        if(results.Read()){
            m = new Movie{
            Name = (string)results[1],
            Genre = (string)results[3],
            Year = (int)results[2]
            };
            
            movies.Remove(m);
        }
        results.Close();
        return;
    }
}

}