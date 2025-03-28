﻿using Movies.Application.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Application.Repositories;

public interface IMovieRepository
{
    Task<bool> CreateMovie(Movie movie);
    Task<Movie?> GetById(Guid id);
    Task<Movie?> GetBySlug(string slug);
    IAsyncEnumerable<Movie> GetAll();
    Task<bool> Update(Movie movie);
    Task<bool> DeleteById(Guid id);
}

public class MovieRepository : IMovieRepository
{
    readonly List<Movie> _movies = [];
    readonly StringComparer sComp = StringComparer.Create(new("en-us"), CompareOptions.IgnoreCase | CompareOptions.NumericOrdering);

    public Task<bool> CreateMovie(Movie movie)
    {
        _movies.Add(movie);
        return Task.FromResult(true);
    }

    public Task<Movie?> GetById(Guid id)
    {
        Movie? movie = _movies.SingleOrDefault(x => x.Id == id);
        return Task.FromResult(movie);
    }

    public Task<Movie?> GetBySlug(string slug)
    {
        Movie? movie = _movies.SingleOrDefault(x => sComp.Compare(x.Slug, slug) is 0);
        return Task.FromResult(movie);
    }

    public IAsyncEnumerable<Movie> GetAll() => _movies.ToAsyncEnumerable();

    public Task<bool> Update(Movie movie)
    {
        int movieIndex = _movies.FindIndex(x => x.Id == movie.Id);

        if(movieIndex == -1)
            return Task.FromResult(false);
        
        _movies[movieIndex] = movie;
        return Task.FromResult(true);
    }

    public Task<bool> DeleteById(Guid id)
    {
        int removedCount = _movies.RemoveAll(x => x.Id == id);
        return Task.FromResult(removedCount > 0);
    }
}