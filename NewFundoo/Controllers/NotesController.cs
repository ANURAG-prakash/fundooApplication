using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessManager.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FundooApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        private readonly INotesBL _notesBL;
        public NotesController(INotesBL dataRepository)
        {
            _notesBL = dataRepository;
        }
        private string GetTokenType()
        {
            return User.FindFirst("Id").Value;
        }
      

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var getId = Convert.ToInt64(GetTokenType());
                IEnumerable<Note> note = _notesBL.GetAll(getId);
                return Ok(note);
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }




        [HttpPost]
        public IActionResult AddNote([FromBody] RegisterNotes note)
        {
            try
            {
                if (note == null)
                {
                    return BadRequest("Note is null.");
                }

                bool result = _notesBL.PostNote(note);
                if (result == true)
                {
                    return this.Ok(new { success = true, message = "Note created successfully" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Note Creation failed" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }

        }




        [HttpGet("{noteid}")]
        public IActionResult Get(long noteid)
        {
            try
            {
                Note note = _notesBL.Get(noteid);

                if (note == null)
                {
                    return NotFound("The Employee record couldn't be found.");
                }

                return Ok(note);
            }
            catch(Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }

      

        
        [HttpPut("Update")]
        public IActionResult Put(long id, [FromBody] Note note)
        {
            try
            {
                if (note == null)
                {
                    return BadRequest("Note is null.");
                }

                Note userUpdate = _notesBL.Get(id);
                if (userUpdate == null)
                {
                    return NotFound("The User record couldn't be found.");
                }

                bool result = _notesBL.Put(userUpdate, note);
                if (result == true)
                {
                    return this.Ok(new { success = true, message = "Successfully edited" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Editing Failed" });
                }
            }
            catch(Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }

        }

        [HttpPut("Archive")]
        public IActionResult Archive(long id)
        {
            try
            {
                Note userArchive = _notesBL.Get(id);
                if (userArchive == null)
                {
                    return NotFound("The User record couldn't be found.");
                }

                bool result = _notesBL.Archive(userArchive);
                if (result == true)
                {
                    return this.Ok(new { success = true, message = "Successfully moved" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Moving Failed" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }

        }



        [HttpPut("UnArchive")]
        public IActionResult UnArchive(long id)
        {
            try
            {
                Note userUnArchive = _notesBL.Get(id);
                if (userUnArchive == null)
                {
                    return NotFound("The User record couldn't be found.");
                }

                bool result = _notesBL.UnArchive(userUnArchive);
                if (result == true)
                {
                    return this.Ok(new { success = true, message = "Successfully moved" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Moving Failed" });
                }
            }
            catch(Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }

        }



        [HttpPut("Trash")]
        public IActionResult Trash(long id)
        {
            try
            {
                Note note = _notesBL.Get(id);
                if (note == null)
                {
                    return NotFound("The User record couldn't be found.");
                }
                if (note.IsTrash == true)
                {
                    return BadRequest("Note is already in Trash");
                }


                IEnumerable<Note> result = _notesBL.Trash(note);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }



        [HttpPut("Restore")]
        public IActionResult Restore(long id)
        {
            try
            {
                Note note = _notesBL.Get(id);
                if (note == null)
                {
                    return NotFound("The User record couldn't be found");
                }
                if (note.IsTrash == false)
                {
                    return BadRequest("Note is already moved from Trash");
                }


                IEnumerable<Note> result = _notesBL.Trash(note);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }


        [HttpDelete("Delete")]
        public IActionResult Delete(long id)
        {
            try
            {
                Note note = _notesBL.Get(id);
                if (note == null)
                {
                    return NotFound("The User record couldn't be found.");
                }
                if (note.IsTrash == true)
                {
                    _notesBL.Delete(note);
                    return this.Ok(new { success = true, message = "Successfully deleted" });
                }
                else
                {
                    return BadRequest("Note is not in trash");
                }
            }
            catch(Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }

    }
}