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
    public class LabelController : ControllerBase
    {
        private readonly ILabelBusiness _labelbusiness;

        public LabelController(ILabelBusiness labelbusiness)
        {
            _labelbusiness = labelbusiness;
        }

        [Authorize]
        [HttpPost]
        [Route("Create")]
        public IActionResult AddLable(long noteId, string labelName)
        {
            var userid = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);

            var result = _labelbusiness.AddLable(userid, noteId, labelName);
            if (result)
            {
                return Ok(new ResponseModel<string>() { Success = true, Message = "Label Added", Data="Label Added" });


            }
            else
            {
                return BadRequest(new ResponseModel<string>() { Success = false, Message = "Label NotAdded" });
            }
        }

        [Authorize]
        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateLable(long labelId, string labelname)
        {
            var userid = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);

            UserLabelEntity result = _labelbusiness.UpdateLable(userid, labelId, labelname);
            if (result != null)
            {
                return Ok(new ResponseModel<UserLabelEntity>() { Success = true, Message = "Label Updated", Data = result });


            }
            else
            {
                return BadRequest(new ResponseModel<UserLabelEntity>() { Success = false, Message = "Label Not updated", Data = result });
            }

        }

        [Authorize]
        [HttpGet]
        [Route("GetAllLabels")]

        public IActionResult getAllLabels()
        {


            var userid = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var result = _labelbusiness.GetAllLabels(userid);
            if (result != null)
            {
                return Ok(new ResponseModel<IEnumerable<UserLabelEntity>>() { Success = true, Message = "Labels Found", Data = result });


            }
            else
            {
                return BadRequest(new ResponseModel<IEnumerable<UserLabelEntity>>() { Success = false, Message = "Labels Not Found" });
            }

        }

        [Authorize]
        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteLabel(long labelId)
        {
            var userid = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            UserLabelEntity result = _labelbusiness.DeleteLabel(userid, labelId);
            if (result != null)
            {
                return Ok(new ResponseModel<UserLabelEntity>() { Success = true, Message = "Label Deleted", Data = result });


            }
            else
            {
                return BadRequest(new ResponseModel<UserLabelEntity>() { Success = false, Message = "Label NotDeleted", Data = result });
            }

        }


    }
}
