using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Models
{
    public class NoteUpdateModel
    {
        public string Title {  get; set; }
        public string Description { get; set; }
        public long NoteId {  get; set; }
    }
}
