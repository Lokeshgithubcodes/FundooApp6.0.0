using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ICollaboratorBusiness
    {
        public bool AddCollaborator(long userId, long noteId, string collaboratorEmail);

        public Collaborator DeleteCollaborator(long userId, long noteId, long collaboratorId);

        public IEnumerable<Collaborator> GetCollaboratorsByNoteId(long userId, long noteId);

        public IEnumerable<Collaborator> GetCollaborators(long userId);
    }
}
