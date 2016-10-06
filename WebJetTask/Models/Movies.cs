﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebJetTask.Models
{
    public class MovieBank
    {
        public String MovieProvider { get; set; }
        public IEnumerable<Movie> Movies { get; set; }
    }
    public class Movie
    {

        public String Title { get; set; }
        public int Year { get; set; }
        public string ID { get; set; }
        public String Type { get; set; }
        public String Poster { get; set; }
    }
    public class MovieDetails
    {

        public string Title { get; set; }
        public string Year { get; set; }
        public string Rated { get; set; }
        public string Released { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public string Poster { get; set; }
        public string Metascore { get; set; }
        public string Rating { get; set; }
        public string Votes { get; set; }
        public string ID { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
        public string MovieProvider { get; set; }





    }
}