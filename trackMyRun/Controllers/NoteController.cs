using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using trackMyRun.Data.ReqObjects;
using trackMyRun.Data.ResponseObjects;
using trackMyRun.DbEntities;

namespace trackMyRun.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private TrackMyRunContext dbContext;

        public NoteController(TrackMyRunContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("get-note")]
        public IActionResult GetNote(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var note = dbContext.Notes.FirstOrDefault(x => x.NoteId == id);
                if (note == null)
                {
                    return BadRequest(500);
                }

                var noteDto = dbContext.Notes.Where(x => x.NoteId == id).Select(x => new NoteDto { NoteId = x.NoteId, NoteName = x.NoteName,NoteText = x.NoteText, RunId = x.RunId }).ToList();

                return Ok(noteDto);

               
            }
            catch (Exception)
            {
                return BadRequest(500);
            }
        }

        [HttpPost]
        [Route("add-note")]
        public async Task<IActionResult> AddNote([FromBody] AddNoteRequestBody req)
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
                dbContext.Add(
                    new Note
                    {
                        NoteName = req.NoteName,
                        NoteText = req.NoteText,
                        RunId = req.RunId,
                    }
                );
                await dbContext.SaveChangesAsync();

                var newNote = dbContext.Notes.First(x => x.NoteName == req.NoteName);

                return Ok(
                    new NoteDto
                    {
                        NoteId = newNote.NoteId,
                        NoteText = newNote.NoteText,
                        RunId = newNote.RunId,
                        NoteName = newNote.NoteName,
                    }
                );
            }
            catch (Exception)
            {
                return BadRequest(500);
            }
        }

        [HttpPut]
        [Route("update-note")]
        public async Task<IActionResult> UpdateNote([FromBody] UpdateNoteRequestBody req)
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
                var note = dbContext.Notes.FirstOrDefault(x => x.NoteId == req.NoteId);
                if (note == null)
                {
                    return BadRequest(500);
                }
                note.NoteName = req.NoteName;
                note.NoteText = req.NoteText;
                note.RunId = req.RunId;
                await dbContext.SaveChangesAsync();

                return Ok(
                    new NoteDto
                    {
                        NoteId = note.NoteId,
                        NoteName = note.NoteName,
                        NoteText = note.NoteText,
                        RunId = note.RunId,
                    }
                );
            }
            catch (Exception)
            {
                return BadRequest(500);
            }
        }

        [HttpDelete]
        [Route("delete-note")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var note = dbContext.Notes.FirstOrDefault(x => x.NoteId == id);
                if (note == null)
                {
                    return BadRequest(500);
                }
                dbContext.Notes.Remove(note);
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
