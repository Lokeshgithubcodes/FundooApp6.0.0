using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;

namespace FundooNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        private ICollaboratorBusiness _business;

        public CollaboratorController(ICollaboratorBusiness business)
        {
            _business = business;
        }

        [Authorize]
        [HttpPost]
        [Route("Add")]

        public IActionResult AddCollaborator(long noteId, string collaboratorEmail)
        {
            var userid = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);

            var result = _business.AddCollaborator(userid, noteId, collaboratorEmail);
            if (result)
            {
                return Ok(new ResponseModel<string> { Success = true, Message = "Collaborator added" });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "Ccollaborator not added" });
            }
        }


        [Authorize]
        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteCollaborator(long noteId, long collaboratorId)
        {
            var userid = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var result = _business.DeleteCollaborator(userid, noteId, collaboratorId);
            if (result != null)
            {
                return Ok(new ResponseModel<Collaborator>() { Success = true, Message = "Collaborator deleted ", Data = result });
            }
            else
            {
                return NotFound(new ResponseModel<Collaborator>() { Success = false, Message = "Collaborator not deleted", Data = null });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("Get")]
        public IActionResult GetCollaboratorsByNoteId(long noteId)
        {
            var userid = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var result = _business.GetCollaboratorsByNoteId(userid, noteId);
            if (result != null)
            {
                return Ok(new ResponseModel<IEnumerable<Collaborator>>() { Success = true, Message = "Collaborator found... ", Data = result });
            }
            else
            {
                return NotFound(new ResponseModel<IEnumerable<Collaborator>>() { Success = false, Message = "Collaborator not Not found", Data = null });
            }

        }

        [Authorize]
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetCollaborators()
        {
            var userid = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var result = _business.GetCollaborators(userid);
            if (result != null)
            {
                return Ok(new ResponseModel<IEnumerable<Collaborator>>() { Success = true, Message = "Collaborator found... ", Data = result });
            }
            else
            {
                return NotFound(new ResponseModel<IEnumerable<Collaborator>>() { Success = false, Message = "Collaborator not Not found", Data = null });
            }

        }
    }
}
