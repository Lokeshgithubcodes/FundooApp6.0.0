using CommonLayer.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class UserNotesRepository:IUserNotesRepository
    {
        private readonly FundooContext fundooContext;

        public UserNotesRepository(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        public UserNotesEntity CreateUserNotes(UserNotesModel notes, long UserId)
        {
            if (UserId != 0)
            {
               
                UserEntity user = fundooContext.UserTable.FirstOrDefault(u => u.UserId == UserId);
                if (user != null)
                {
                    UserNotesEntity notesEntity = new UserNotesEntity
                    {
                        Title = notes.Title,
                        Description = notes.Description,
                        Color = notes.Color,
                        Remainder = notes.Remainder,
                        IsArchive = notes.IsArchive,
                        IsPinned = notes.IsPinned,
                        IsTrash = notes.IsTrash,
                        CreatedAt = DateTime.Now,
                        ModifiedAt = DateTime.Now,
                        UserId = UserId
                    };

                    fundooContext.UserNotes.Add(notesEntity);
                    fundooContext.SaveChanges();
                    return notesEntity;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }

        public UserNotesEntity UpdateNotes(int noteId, long userId, NoteUpdateModel notesModel)
        {
            var userNotes = fundooContext.UserNotes.FirstOrDefault(x => x.NoteId == noteId && x.UserId == userId);
            if (userNotes != null)
            {
                userNotes.Title=notesModel.Title ?? userNotes.Title;
                userNotes.Description=notesModel.Description ?? userNotes.Description;
                userNotes.ModifiedAt=DateTime.Now;
                fundooContext.SaveChanges();
                return userNotes;
            }
            else
            {
                return null;
            }
        }

        public List<UserNotesEntity> GetAllNodes()
        {
            var nodes = new List<UserNotesEntity>();
            nodes = fundooContext.UserNotes.ToList();
            return nodes;
        }

        public List<UserNotesEntity> GetUserNotesById(long userId)
        {
            try
            {

                var nodes = new List<UserNotesEntity>();
                nodes = fundooContext.UserNotes.Where(x => x.UserId == userId).ToList();
                return nodes;
            }
            catch (Exception)
            {

                throw;

            }
        }

        public UserNotesEntity DeleteNote(int id, int userId)
        {
            var delNote = fundooContext.UserNotes.FirstOrDefault(x => x.NoteId == id && x.UserId == userId);
            if (delNote != null)
            {
                fundooContext.UserNotes.Remove(delNote);
                fundooContext.SaveChanges();
                return delNote;
            }
            else
            {
                return null;
            }
        }

        public UserNotesEntity GetNotesById(long noteId, long userId)
        {
          
            UserNotesEntity noteinfo = fundooContext.UserNotes.FirstOrDefault(x => x.NoteId == noteId && x.UserId == userId);
            
            if(noteinfo != null)
            {
                return noteinfo;
            }
            else
            {
                return null;
            }
        }

        public UserNotesEntity ArchieveNotes(long userId, long noteId)
        {
            var notes = fundooContext.UserNotes.FirstOrDefault(x => x.UserId == userId && x.NoteId == noteId);
            if (notes != null)
            {
                notes.IsArchive = !notes.IsArchive;
                fundooContext.SaveChanges();
                return notes;
            }
            else
            {
                return null;
            }
        }

        public UserNotesEntity TrashNotes(long userId, long noteId)
        {
            var notes = fundooContext.UserNotes.FirstOrDefault(x => x.UserId == userId && x.NoteId == noteId);
            if (notes != null)
            {
                notes.IsTrash = !notes.IsTrash;
                fundooContext.SaveChanges();
                return notes;
            }
            else
            {
                return null;
            }
        }


        public UserNotesEntity TogglePin(long userid, long noteid)
        {
            var pin = fundooContext.UserNotes.FirstOrDefault(x => x.UserId == userid && x.NoteId == noteid);
            if (pin != null)
            {
                if (pin.IsPinned == true)
                {
                    pin.IsPinned = false;
                    fundooContext.Entry(pin).State = EntityState.Modified;
                    fundooContext.SaveChanges();
                    return pin;
                }
                else
                {
                    pin.IsPinned = true;
                    fundooContext.Entry(pin).State = EntityState.Modified;
                    fundooContext.SaveChanges();
                    return pin;
                }
            }
            else
            {
                return null;
            }
        }

        public UserNotesEntity AddNoteColor(long userId, long noteId, string color)
        {
            var notes = fundooContext.UserNotes.FirstOrDefault(x => x.UserId == userId && x.NoteId == noteId);
            if (notes != null)
            {
                notes.Color = color;
                fundooContext.SaveChanges();
                return notes;
            }
            else
            {
                return null;
            }
        }


    }
}
