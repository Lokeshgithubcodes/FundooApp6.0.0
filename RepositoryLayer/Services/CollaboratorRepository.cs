using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class CollaboratorRepository:ICollaboratorRepository
    {
        private FundooContext fundooContext;

        public CollaboratorRepository(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        public bool AddCollaborator(long userId, long noteId, string collaboratorEmail)
        {
            var notes = fundooContext.UserNotes.Where(x => x.UserId == userId && x.NoteId == noteId).FirstOrDefault();
            if (notes != null)
            {

                Collaborator collaborator = new Collaborator();
                collaborator.UserId = userId;
                collaborator.NoteId = noteId;
                collaborator.CollaboratorsEmail = collaboratorEmail;
                fundooContext.Add(collaborator);
                fundooContext.SaveChanges();

                return true;

            }
            else
            {
                return false;
            }

        }

        public Collaborator DeleteCollaborator(long userId, long noteId, long collaboratorId)
        {
            var collab = fundooContext.TCollaborator.Where(x => x.UserId == userId && x.NoteId == noteId && x.CollaboratorsId == collaboratorId).FirstOrDefault();
            if (collab != null)
            {
                fundooContext.TCollaborator.Remove(collab);
                fundooContext.SaveChanges();
                return collab;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<Collaborator> GetCollaboratorsByNoteId(long userId, long noteId)
        {
            var collab = fundooContext.TCollaborator.Where(x => x.UserId == userId && x.NoteId == noteId).ToList();
            if (collab != null)
            {
                return collab;
            }
            return null;
        }

        public IEnumerable<Collaborator> GetCollaborators(long userId)
        {
            var collab = fundooContext.TCollaborator.Where(x => x.UserId == userId).ToList();
            if (collab != null)
            {
                return collab;
            }
            return null;
        }
    }
}
