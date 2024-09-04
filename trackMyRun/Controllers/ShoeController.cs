using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using trackMyRun.Data.ReqObjects;
using trackMyRun.Data.ResponseObjects;
using trackMyRun.DbEntities;

namespace trackMyRun.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoeController : ControllerBase
    {
        private TrackMyRunContext dbContext;

        public ShoeController(TrackMyRunContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("get-shoes")]
        public IActionResult GetShoes(int shoesId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var shoes = dbContext.Shoes.FirstOrDefault(x => x.ShoeId == shoesId);
                if (shoes == null)
                {
                    return BadRequest();
                }
                var shoesDto = dbContext.Shoes
                    .Where(x => x.ShoeId == shoesId)
                    .Select(
                        x =>
                            new ShoeDto
                            {
                                Id = x.ShoeId,
                                ShoeName = x.ShoeName,
                                Width = x.Width,
                                Size = x.Size,
                                ShoeImg = x.ShoeImg
                            }
                    )
                    .ToList();
                return Ok(shoesDto);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("add-shoe")]
        public async Task<IActionResult> AddShoe([FromBody] AddShoeRequestBody req)
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
                dbContext.Shoes.Add(
                    new Shoe
                    {
                        ShoeName = req.ShoeName,
                        Width = req.Width,
                        Size = req.Size,
                        ShoeImg = req.ShoeImg
                    }
                );

                await dbContext.SaveChangesAsync();

                var newShoe = dbContext.Shoes.First(x => x.ShoeName == req.ShoeName);

                return Ok(
                    new ShoeDto
                    {
                        Id = newShoe.ShoeId,
                        ShoeName = newShoe.ShoeName,
                        Width = newShoe.Width,
                        Size = newShoe.Size,
                        ShoeImg = newShoe.ShoeImg
                    }
                );
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        [Route("update-shoe")]
        public async Task<IActionResult> UpdateShoe([FromBody] UpdateShoeRequestBody req)
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
                var shoe = dbContext.Shoes.FirstOrDefault(x => x.ShoeId == req.ShoeId);
                if (shoe == null)
                {
                    return BadRequest();
                }
                shoe.ShoeName = req.ShoeName;
                shoe.Size = req.Size;
                shoe.Width = req.Width;
                shoe.ShoeImg = req.ShoeImg;
                await dbContext.SaveChangesAsync();

                return Ok(
                    new ShoeDto
                    {
                        Id = req.ShoeId,
                        ShoeName = req.ShoeName,
                        Width = req.Width,
                        Size = req.Size,
                        ShoeImg = req.ShoeImg
                    }
                );
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("delete-shoe")]
        public async Task<IActionResult> DeleteShoe(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var shoeToDelete = dbContext.Shoes.FirstOrDefault(x => x.ShoeId == id);
                if (shoeToDelete == null)
                {
                    return BadRequest();
                }

                dbContext.Shoes.Remove(shoeToDelete);
                await dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
