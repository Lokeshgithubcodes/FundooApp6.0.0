using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Models
{
    public class UserNotesModel
    {
        public string Title {  get; set; }
        public string Description { get; set; }
        public string Color {  get; set; }
        public DateTime Remainder { get; set; }
        public bool IsArchive {  get; set; }
        public bool IsPinned {  get; set; }

        public bool IsTrash {  get; set; }
    }
}
