using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Models
{
    public class LoginModel
    {
    //    [Required]
    //    [RegularExpression(@"^[a-z]{1}[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-]+$")]
    //    [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //[Required]
        //[RegularExpression(@"^[A-Z]{1,3}\w[a-z0-9!@]{7,}$")]
        //[DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
