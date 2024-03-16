using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IUserNotesBusiness
    {
        public UserNotesEntity CreateUserNotes(UserNotesModel notes, long UserId);

        public UserNotesEntity UpdateNotes(int noteId, long userId, NoteUpdateModel notesModel);

        public List<UserNotesEntity> GetAllNodes();

        public List<UserNotesEntity> GetUserNotesById(long userId);

        public UserNotesEntity DeleteNote(int id, int userId);

        public UserNotesEntity GetNotesById(long noteId, long userId);

        public UserNotesEntity ArchieveNotes(long userId, long noteId);

        public UserNotesEntity TrashNotes(long userId, long noteId);

        public UserNotesEntity TogglePin(long userid, long noteid);

        public UserNotesEntity AddNoteColor(long userId, long noteId, string color);

        public UserNotesEntity AddRemainder(long userId, long noteId, DateTime reminder);

        public UserNotesEntity AddImage(long userId, long noteId, IFormFile Image);

        public List<UserNotesEntity> GetNotesBy1stLetter(string letter);
    }
}
