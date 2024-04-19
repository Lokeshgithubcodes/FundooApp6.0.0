
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entity
{
    public class UserLabelEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LabelId { get; set; }
        public string LabelName { get; set; }

        [ForeignKey("UserTable")]
        public long UserId { get; set; }

        [JsonIgnore]
        public virtual UserEntity UserTable { get; set; }


        [ForeignKey("UserNotes")]
        public long NoteId { get; set; }
       
        [JsonIgnore]
        public virtual UserNotesEntity UserNotes { get; set; }
    }
}
