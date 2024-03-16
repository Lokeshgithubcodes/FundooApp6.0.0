using BusinessLayer.Interfaces;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CollaboratorBusiness:ICollaboratorBusiness
    {
        private readonly ICollaboratorRepository _collaboratorRepo;

        public CollaboratorBusiness(ICollaboratorRepository collaboratorRepo)
        {
            this._collaboratorRepo = collaboratorRepo;
        }

        public bool AddCollaborator(long userId, long noteId, string collaboratorEmail)
        {
            return _collaboratorRepo.AddCollaborator(userId, noteId, collaboratorEmail);
        }

        public Collaborator DeleteCollaborator(long userId, long noteId, long collaboratorId)
        {
            return _collaboratorRepo.DeleteCollaborator(userId, noteId, collaboratorId);
        }

        public IEnumerable<Collaborator> GetCollaboratorsByNoteId(long userId, long noteId)
        {
            return _collaboratorRepo.GetCollaboratorsByNoteId(userId, noteId);
        }

        public IEnumerable<Collaborator> GetCollaborators(long userId)
        {
            return _collaboratorRepo.GetCollaborators(userId);
        }
    }

}
