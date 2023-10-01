using System;
using System.Linq;
using FetchBackendChallenge;
using FetchBackendChallenge.Domain;

namespace FetchBackendChallenge.Service
{
	public class PointsService : IPointsService
	{
		private PriorityQueue<PointsTransaction, DateTime> _pointsEntries;

		private Dictionary<string, int> _pointsDict;
		private int _totalPoints;

		public PointsService()
		{
			_pointsEntries = new PriorityQueue<PointsTransaction, DateTime>();
			_pointsDict = new Dictionary<string, int>();
			_totalPoints = 0;
		}

		public Dictionary<string, int> GetAll()
		{
			return _pointsDict;
		}

		public bool AddPointsTransaction(PointsTransaction transaction)
		{
			if (!_pointsDict.ContainsKey(transaction.Payer))
			{
				_pointsDict.Add(transaction.Payer, transaction.Points);
			}
			else
			{
				_pointsDict[transaction.Payer] += transaction.Points;
			}

			_pointsEntries.Enqueue(transaction, transaction.Timestamp);
			_totalPoints += transaction.Points;
			return true;
		}

		public IEnumerable<PointsEntry> SpendPoints(int points)
		{
			Dictionary<string, int> transactions = new Dictionary<string, int>();

			if (points > _totalPoints)
			{
				return new List<PointsEntry>();
			}
			else
			{
				_totalPoints -= points;
			}

			while (points != 0 && _pointsEntries.TryDequeue(out PointsTransaction? earliest, out DateTime _))
			{
				if (!_pointsDict.ContainsKey(earliest.Payer))
				{
					continue;
				}

				if (points > earliest.Points)
				{
					if (transactions.ContainsKey(earliest.Payer))
					{
						transactions[earliest.Payer] += -1 * earliest.Points;
					}
					else
					{
						transactions.Add(earliest.Payer, -1 * earliest.Points);
					}

					_pointsDict[earliest.Payer] -= earliest.Points;
					points -= earliest.Points;
				}
				else
				{
					if (transactions.ContainsKey(earliest.Payer))
					{
						transactions[earliest.Payer] += -1 * points;
					}
					else
					{
						transactions.Add(earliest.Payer, -1 * points);
					}

					_pointsEntries.Enqueue(new PointsTransaction(earliest.Payer, earliest.Points - points, earliest.Timestamp), earliest.Timestamp);
					_pointsDict[earliest.Payer] -= points;
					points = 0;
				}
			}

			return transactions.Select(t => new PointsEntry(t.Key, t.Value));
		}
	}
}

