using FetchBackendChallenge.Models;

namespace FetchBackendChallenge.Domain;

public class PointsTransaction
{
    public string Payer { get; set; }
    public int Points { get; set; }
    public DateTime Timestamp { get; set; }

    public PointsTransaction(string payer, int points, DateTime timestamp)
    {
        Payer = payer;
        Points = points;
        Timestamp = timestamp;
    }

    public PointsEntryModel ToPointsEntryModel() => new PointsEntryModel(Payer, Points);
}

