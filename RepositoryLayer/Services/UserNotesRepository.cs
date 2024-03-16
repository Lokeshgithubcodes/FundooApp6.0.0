using CommonLayer.Models;
using MassTransit.Initializers.Variables;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

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


        public UserNotesEntity AddRemainder(long userId, long noteId, DateTime reminder)
        {
            var note = fundooContext.UserNotes.Where(x => x.UserId == userId && x.NoteId == noteId).FirstOrDefault();
            if (note == null)
            {
                return null;
            }
            else
            {

                if (reminder > DateTime.Now)
                {
                    note.Remainder = reminder;
                    fundooContext.Entry(note).State = EntityState.Modified;
                    fundooContext.SaveChanges();
                    return note;
                }
                else
                {
                    return null;
                }
            }
        }

        public UserNotesEntity AddImage(long userId, long noteId, IFormFile Image)
        {
            try
            {


                var note = fundooContext.UserNotes.Where(x => x.UserId == userId && x.NoteId == noteId).FirstOrDefault();

                if (note == null)
                {
                    return null;
                }
                else
                {
                    Account account = new Account(
                                                   "dwvr6sy4h",
                                                    "917726717342863",
                                                       "sQW87yt8T4uswwRknaBhOIIcXGE");

                    Cloudinary cloudinary = new Cloudinary(account);


                    var uploadParameters = new ImageUploadParams()
                    {
                        File = new FileDescription(Image.FileName, Image.OpenReadStream()),
                        PublicId = note.Title
                    };
                    var uploadResult = cloudinary.Upload(uploadParameters);
                    string ImagePath = uploadResult.Url.ToString();


                    note.Image = ImagePath;
                    fundooContext.Entry(note).State = EntityState.Modified;
                    fundooContext.SaveChanges();

                    return note;

                }
            }
            catch (Exception )
            {
                throw;
            }

        }

        public List<UserNotesEntity> GetNotesBy1stLetter(string letter)
        {
            var let=fundooContext.UserNotes.Where(x=>x.Title.StartsWith(letter)).ToList();
            if (let != null)
            {
                return let;
            }
            else
            {
                return null;
            }
        }

    }
}
