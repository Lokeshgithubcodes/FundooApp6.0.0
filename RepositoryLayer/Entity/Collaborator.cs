using RepositoryLayer.Migrations;
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
    public class Collaborator
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CollaboratorsId { get; set; }
        public string CollaboratorsEmail { get; set; }

        [ForeignKey("UserTable")]
        public long UserId { get; set; }

        [JsonIgnore]
        public virtual UserEntity UserTable { get; set; }


        [ForeignKey("UserNotes")]// name downbellow fields
        public long NoteId { get; set; }

        [JsonIgnore]
        public virtual UserLabelEntity UserNotes { get; set; }
    }
}
