using FetchBackendChallenge.Models;

namespace FetchBackendChallenge.Domain;

public class PointsEntry
{
    public string Payer { get; set; }
    public int Points { get; set; }

    public PointsEntry(string payer, int points)
    {
        Payer = payer;
        Points = points;
    }
}

