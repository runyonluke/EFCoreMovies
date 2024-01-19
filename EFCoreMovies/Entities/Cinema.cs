using EFCoreMovies.Entities;
using NetTopologySuite.Geometries;

public class Cinema
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Point Location { get; set; }
    public CinemaOffer CinemaOffer { get; set; }
    public HashSet<CinemaHall> CinemasHalls { get; set; }
}