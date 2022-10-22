﻿// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class Location
{
    public string name { get; set; }
    public string url { get; set; }
}

public class Origin
{
    public string name { get; set; }
    public string url { get; set; }
}
public class Characters
{
    public Character[] root{ get; set; }
    public Countchars info { get; set; }
}
public class Countchars
{
    public int count { get; set; }
}

public class Character
{
    public int id { get; set; }
    public string name { get; set; }
    public string status { get; set; }
    public string species { get; set; }
    public string type { get; set; }
    public string gender { get; set; }
    public Origin origin { get; set; }
    public Location location { get; set; }
    public string image { get; set; }
    public List<string> episode { get; set; }
    public string url { get; set; }
    public DateTime created { get; set; }
}

