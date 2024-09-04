using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using trackMyRun.Data.ReqObjects;
using trackMyRun.Data.ResponseObjects;
using trackMyRun.DbEntities;

namespace trackMyRun.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RunController : ControllerBase
    {
        private TrackMyRunContext dbContext;

        public RunController(TrackMyRunContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("get-run")]
        public IActionResult GetRun(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var run = dbContext.Runs.FirstOrDefault(x => x.RunId == id);
                if (run == null)
                {
                    return BadRequest(500);
                }
                var runDto = dbContext.Runs
                    .Where(x => x.RunId == id)
                    .Select(
                        x =>
                            new RunDto
                            {
                                RunName = x.RunName,
                                RunId = x.RunId,
                                Distance = x.Distance,
                                HeartRate = x.HeartRate,
                                AvgPace = x.AvgPace,
                                ShoeId = x.ShoeId,
                                Time = x.Time
                            }
                    );
                return Ok(runDto);
            }
            catch (Exception)
            {
                return BadRequest(500);
            }
        }

        [HttpPost]
        [Route("add-run")]
        public async Task<IActionResult> AddRun([FromBody] AddRunRequestBody req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (req == null)
            {
                return BadRequest();
            }

            try
            {
                var run = dbContext.Runs.Add(
                    new Run
                    {
                        RunName = req.RunName,
                        Distance = req.Distance,
                        HeartRate = req.HeartRate,
                        AvgPace = req.AvgPace,
                        Time = req.Time,
                        ShoeId = req.ShoeId,
                    }
                );
                await dbContext.SaveChangesAsync();

                var newRun = dbContext.Runs.First(x => x.RunName == req.RunName);

                return Ok(
                    new RunDto
                    {
                        RunId = newRun.RunId,
                        RunName = newRun.RunName,
                        Distance = newRun.Distance,
                        HeartRate = newRun.HeartRate,
                        AvgPace = newRun.AvgPace,
                        Time = newRun.Time,
                        ShoeId = newRun.ShoeId,
                    }
                );
            }
            catch (Exception)
            {
                return BadRequest(500);
            }
        }

        [HttpPut]
        [Route("update-run")]
        public async Task<IActionResult> UpdateRun([FromBody] UpdateRunRequestBody req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (req == null)
            {
                return BadRequest();
            }

            try
            {
                var run = dbContext.Runs.FirstOrDefault(x => x.RunId == req.RunId);
                if (run == null)
                {
                    return BadRequest();
                }

                run.RunName = req.RunName;
                run.Distance = req.Distance;
                run.Time = req.Time;
                run.AvgPace = req.AvgPace;
                run.HeartRate = req.HeartRate;
                run.ShoeId = req.ShoeId;

                await dbContext.SaveChangesAsync();

                var newRun = dbContext.Runs.First(x => x.RunName == req.RunName);

                return Ok(
                    new RunDto
                    {
                        RunId = newRun.RunId,
                        RunName = newRun.RunName,
                        Distance = newRun.Distance,
                        Time = newRun.Time,
                        AvgPace = newRun.AvgPace,
                        HeartRate = newRun.HeartRate,
                        ShoeId = newRun.ShoeId,
                    }
                );
            }
            catch (Exception)
            {
                return BadRequest(500);
            }
        }

        [HttpDelete]
        [Route("delete-run")]
        public async Task<IActionResult> DeleteRun(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var run = dbContext.Runs.FirstOrDefault(x => x.RunId == id);
                if (run == null)
                {
                    return BadRequest(500);
                }
                dbContext.Runs.Remove(run);
                await dbContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest(500);
            }
        }
    }
}
