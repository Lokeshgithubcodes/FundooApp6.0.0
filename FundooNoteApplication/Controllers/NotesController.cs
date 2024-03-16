using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System.Text;
using Microsoft.Extensions.Caching.StackExchangeRedis;


namespace FundooNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly IUserNotesBusiness _userNotes;
        private readonly IDistributedCache _distributedCache;
        private readonly FundooContext _fundooContext;

        public NotesController(IUserNotesBusiness userNotes, IDistributedCache distributedCache, FundooContext _fundooContext)
        {
           this. _userNotes = userNotes;
            this. _distributedCache = distributedCache;
            this._fundooContext = _fundooContext;
        }

        [HttpPost]
        [Route("CreateNotes")]

       // [Authorize]

        public IActionResult UserNoteCreation(UserNotesModel userNotes)
        {
            try
            {
                // long userId = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
                byte[] userbyte = HttpContext.Session.Get("UserId");
                long userId=BitConverter.ToInt64(userbyte, 0);
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

        [Authorize]
        [HttpPost]
        [Route("AddReminder")]
        public IActionResult AddReminder(long noteId, DateTime reminder)
        {
            var userid = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var result = _userNotes.AddRemainder(userid, noteId, reminder);
            if (result != null)
            {
                return Ok(new ResponseModel<UserNotesEntity>() { Success = true, Message = "reminder added..", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<UserNotesEntity>() { Success = false, Message = "Some thing went wrong..!", Data = result });
            }
        }

        [Authorize]
        [HttpPost]
        [Route("AddImage")]
        public IActionResult ImageAdd(int noteId, IFormFile imageUrl)
        {
            var userid = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var result = _userNotes.AddImage(userid, noteId, imageUrl);
            if (result != null)
            {
                return Ok(new { Success = true, Message = "Image addedsucessfully..", Data = result });
            }
            else
            {
                return BadRequest(new { Success = false, Message = "Some thing went wrong..!", Data = result });
            }

        }

        [Authorize]
        [HttpGet]
        [Route("Redis")]
        public async Task<IActionResult> GetAllNotesUsiingRedisCache()
        {
            var cachKey = "NotesList";
            string serializedNotedList;
            
            var NotesList = new List<UserNotesEntity>();
            byte[] redisNotesList = await _distributedCache.GetAsync(cachKey);
            if (redisNotesList != null)
            {
                serializedNotedList = Encoding.UTF8.GetString(redisNotesList);
                NotesList = JsonConvert.DeserializeObject<List<UserNotesEntity>>(serializedNotedList);

            }
            else
            {
                NotesList = _fundooContext.UserNotes.ToList();
                serializedNotedList = JsonConvert.SerializeObject(NotesList);
                redisNotesList = Encoding.UTF8.GetBytes(serializedNotedList);
                var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddMinutes(10)).SetSlidingExpiration(TimeSpan.FromMinutes(2));
               await _distributedCache.SetAsync(cachKey, redisNotesList, options);
            }
            return Ok(NotesList);
        }

        [Authorize]
        [HttpGet]
        [Route("GetBy1stLetter")]

        public IActionResult GetBy1stLetter(string name)
        {
            var res=_userNotes.GetNotesBy1stLetter(name);
            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest("not found");
            }
        }

    }
}
