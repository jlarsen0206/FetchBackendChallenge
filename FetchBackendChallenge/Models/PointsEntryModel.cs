using System;
using System.Text;
using FetchBackendChallenge.Domain;

namespace FetchBackendChallenge.Models
{
    public class PointsEntryModel
    {
        public string Payer { get; set; }

        public int Points { get; set; }

        public PointsEntryModel(string payer, int points)
        {
            Payer = payer;
            Points = points;
        }
    }
}

