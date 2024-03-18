using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class UserNotesBusiness:IUserNotesBusiness
    {
        private readonly IUserNotesRepository _notesrepo;

        public UserNotesBusiness(IUserNotesRepository notesrepo)
        {
            this._notesrepo = notesrepo;
        }


        public UserNotesEntity CreateUserNotes(UserNotesModel notes, long UserId)
        {
            return _notesrepo.CreateUserNotes(notes, UserId);
        }

        public UserNotesEntity UpdateNotes(int noteId, long userId, NoteUpdateModel notesModel)
        {
            return _notesrepo.UpdateNotes(noteId, userId, notesModel);
        }

        public List<UserNotesEntity> GetAllNodes()
        {
            return _notesrepo.GetAllNodes();
        }
        public List<UserNotesEntity> GetUserNotesById(long userId)
        {
            return _notesrepo.GetUserNotesById(userId);
        }

        public UserNotesEntity DeleteNote(int id, int userId)
        {
            return _notesrepo.DeleteNote(id, userId);
        }

        public UserNotesEntity GetNotesById(long noteId, long userId)
        {
            return _notesrepo.GetNotesById(noteId, userId);
        }

        public UserNotesEntity ArchieveNotes(long userId, long noteId)
        {
            return _notesrepo.ArchieveNotes(userId, noteId);
        }

        public UserNotesEntity TrashNotes(long userId, long noteId)
        {
            return _notesrepo.TrashNotes(userId, noteId);
        }

        public UserNotesEntity TogglePin(long userid, long noteid)
        {
            return _notesrepo.TogglePin(userid, noteid);
        }

        public UserNotesEntity AddNoteColor(long userId, long noteId, string color)
        {
            return _notesrepo.AddNoteColor(userId, noteId, color);
        }

        public UserNotesEntity AddRemainder(long userId, long noteId, DateTime reminder)
        {
            return _notesrepo.AddRemainder(userId, noteId, reminder);
        }

        public UserNotesEntity AddImage(long userId, long noteId, IFormFile Image)
        {
            return _notesrepo.AddImage(userId, noteId, Image);
        }

        public List<UserNotesEntity> GetNotesBy1stLetter(string letter)
        {
            return _notesrepo.GetNotesBy1stLetter(letter);
        }

        public UserNotesEntity GetNoteByTitle(string title)
        {
            return _notesrepo.GetNoteByTitle(title);
        }

        public UserNotesEntity GetNoteByBody(string body)
        {
            return _notesrepo.GetNoteByBody(body);
        }

        public long GetCount(long userid)
        {
            return _notesrepo.GetCount(userid);
        }
    }
}
