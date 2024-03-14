using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;

namespace FundooNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly IUserNotesBusiness _userNotes;

        public NotesController(IUserNotesBusiness userNotes)
        {
           this. _userNotes = userNotes;
        }

        [HttpPost]
        [Route("CreateNotes")]

        [Authorize]

        public IActionResult UserNoteCreation(UserNotesModel userNotes)
        {
            try
            {
                long userId = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
                var result = _userNotes.CreateUserNotes(userNotes, userId);
                if (result != null)
                {
                    return Ok(new ResponseModel<UserNotesEntity> { Success = true, Message = "created Notes Successfully.", Data = result });

                }
                else
                {
                    return BadRequest(new ResponseModel<UserNotesEntity> { Success = false, Message = "failed to create Notes" });
                }
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }


        [Authorize]
        [HttpPut]
        [Route("UpdateNotes")]

        public IActionResult UpdateUserNotes(long noteId, NoteUpdateModel noteModel)
        {
            long userId = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            if (userId != 0)
            {
                var updateNote = _userNotes.UpdateNotes((int)noteId, userId, noteModel);
                if (updateNote != null)
                {
                    return Ok(updateNote);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest("Invalid Note Data");
            }
        }

        [HttpGet("GetAllNodes")]
        public IActionResult GetAllNodes()
        {
            var notesInfo = _userNotes.GetAllNodes();
            if (notesInfo != null)
            {
                return Ok(notesInfo);
            }
            else
            {
                return BadRequest();
            }

        }



        [Authorize]
        [HttpGet]
        [Route("GetUserNotesById")]
        public IActionResult GetUserNotes(long UserId)
        {
            
            if (UserId != 0)
            {
                var userNote = _userNotes.GetUserNotesById(UserId);
                return Ok(userNote);
            }
            else
            {

                return BadRequest();
            }

        }

        [HttpDelete]
        [Route("NoteDelete")]
        [Authorize]

        public IActionResult DeleteUserNode(int noteId)
        {
            long userId = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            if (userId != 0)
            {
                var delNode = _userNotes.DeleteNote(noteId, (int)userId);
                if (delNode != null)
                {
                    return Ok(delNode);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }

        }

        [Authorize]
        [HttpGet]
        [Route("NoteById")]
        public IActionResult GetNoteById(int id)
        {
            
            int userId = int.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var noteInfo = _userNotes.GetNotesById(id, userId);
            if (noteInfo != null)
            {
                return Ok(noteInfo);
            }
            else
            {
                return BadRequest("Note Not Found");
            }

        }

        [HttpPut]
        [Route("archieveNote")]
        [Authorize]
        public IActionResult ArchiveNote(long noteId)
        {
            long userId = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            if (userId != 0)
            {
                var notes = _userNotes.ArchieveNotes(userId, noteId);
                if (notes != null)
                {
                    return Ok(notes);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPut]
        [Route("trashNote")]
        [Authorize]
        public IActionResult TrashNote(long noteId)
        {
            long userId = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            if (userId != 0)
            {
                var notes = _userNotes.TrashNotes(userId, noteId);
                if (notes != null)
                {
                    return Ok(notes);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }

        }


        [Authorize]
        [HttpPost]
        [Route("Pinned")]

        public IActionResult TogglePin(long noteid)
        {
            long userid = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var pin = _userNotes.TogglePin(userid, noteid);
            if (pin != null)
            {
                return Ok(new { success = true, message = "TogglePinned Successsfully", Data = pin });
            }
            else
            {
                return BadRequest(new { success = false, message = "TogglePin not Successful/Something went wrong", Data = pin });
            }
        }

        [HttpPut]
        [Route("addColorToNote")]
        [Authorize]
        public IActionResult AddColor(long noteId, string color)
        {
            long userId = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            if (userId != 0)
            {
                var notes = _userNotes.AddNoteColor(userId, noteId, color);
                if (notes != null)
                {
                    return Ok(notes);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
