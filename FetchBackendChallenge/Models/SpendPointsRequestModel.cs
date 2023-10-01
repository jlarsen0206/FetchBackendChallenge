using System;

namespace FetchBackendChallenge.Models
{
    public class SpendPointsRequestModel
    {
        public int Points { get; set; }

        public SpendPointsRequestModel(int points)
        {
            Points = points;
        }
    }
}

