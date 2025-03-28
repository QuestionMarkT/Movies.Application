﻿using Movies.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Movies.Application.Models;

public partial class Movie
{
    public required Guid Id { get; init; }
    public required string Title { get; set; }
    public string Slug => GenerateSlug();
    public required int YearOfRelease { get; set; }
    public required List<string> Genres { get; init; } = [];

    private string GenerateSlug()
    {
        string sluggedTitle = SlugRegex()
            .Replace(Title, string.Empty)
            .ToLower()
            .Replace(' ', '-');
        return $"{sluggedTitle}-{YearOfRelease}";
    }

    [GeneratedRegex("[^0-9A-Za-z _-]", RegexOptions.NonBacktracking, 8)]
    private static partial Regex SlugRegex();
}