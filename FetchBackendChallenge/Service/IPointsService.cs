using System;
using FetchBackendChallenge.Domain;
using FetchBackendChallenge.Models;

namespace FetchBackendChallenge.Service
{
    public interface IPointsService
    {
        bool AddPointsTransaction(PointsTransaction transaction);
        IEnumerable<PointsEntry> SpendPoints(int points);
        Dictionary<string, int> GetAll();
    }
}


