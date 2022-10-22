namespace RickandMorty.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;


    public class Episoden
    {
        public Episode[] root { get; set; }
        public Countepisoden info { get; set; }
    }
    public class Countepisoden
    {
        public int count { get; set; }
    }
    public class Episode
    {
        public int id { get; set; }
        public string name { get; set; }
        public string air_date { get; set; }
        public string episode { get; set; }
        public List<string> characters { get; set; }
        public string url { get; set; }
        public DateTime created { get; set; }
    }





}
