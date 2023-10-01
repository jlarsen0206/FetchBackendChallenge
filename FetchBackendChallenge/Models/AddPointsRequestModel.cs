using System;
using FetchBackendChallenge.Domain;

namespace FetchBackendChallenge.Models
{
	public class AddPointsRequestModel
	{
		public string Payer { get; set; }
		public int Points { get; set; }
		public DateTime Timestamp { get; set; }

		public AddPointsRequestModel(string payer, int points, DateTime timestamp)
		{
			Payer = payer;
			Points = points;
			Timestamp = timestamp;
		}

		public PointsTransaction ToPointsTransaction() => new PointsTransaction(Payer, Points, Timestamp);
	}
}

