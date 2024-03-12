using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;

namespace FundooNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductBusiness productBusiness;

        public ProductController(IProductBusiness productBusiness)
        {
            this.productBusiness = productBusiness;
        }

        [HttpPost]
        [Route("Register")]

        public IActionResult Register(PRegisterModel model)
        {
            var result = productBusiness.productRegistration(model);
            if (result != null)
            {
                return Ok(result);
            } else {
                return BadRequest();
            }
        }

        [HttpGet("Name")]

        public IActionResult GetProductByName(string name)
        {
            var result=productBusiness.GetByName(name);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
