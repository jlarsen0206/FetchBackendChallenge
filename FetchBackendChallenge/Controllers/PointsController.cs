using Microsoft.AspNetCore.Mvc;
using FetchBackendChallenge.Domain;
using FetchBackendChallenge.Models;
using FetchBackendChallenge.Service;

namespace FetchBackendChallenge.Controllers;

[ApiController]
[Route("/")]
public class PointsController : ControllerBase
{
    private readonly IPointsService _pointsService;

    public PointsController(IPointsService pointsService)
    {
        _pointsService = pointsService;
    }

    [HttpPost("/points")]
    public IActionResult AddPoints([FromBody] AddPointsRequestModel entry)
    {
        try
        {
            _pointsService.AddPointsTransaction(entry.ToPointsTransaction());
        }
        catch (Exception)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpPost("/spend")]
    public ActionResult<IEnumerable<PointsEntryModel>> SpendPoints([FromBody] SpendPointsRequestModel pointsToSpend)
    {
        if (pointsToSpend.Points == 0)
        {
            return BadRequest("Number of points to spend cannot be zero.");
        }

        var transactions = _pointsService.SpendPoints(pointsToSpend.Points);

        if (transactions.Count() == 0)
        {
            return BadRequest("The user does not have enough points.");
        }

        return Ok(transactions.Select(t => new PointsEntryModel(t.Payer, t.Points)));
    }

    [HttpGet("/balance")]
    public ActionResult<Dictionary<string, int>> GetBalance()
    {
        return Ok(_pointsService.GetAll());
    }
}


