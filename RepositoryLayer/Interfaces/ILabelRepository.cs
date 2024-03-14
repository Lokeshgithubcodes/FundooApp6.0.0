using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface ILabelRepository
    {
        public bool AddLable(long userId, long noteId, string labelName);

        public UserLabelEntity UpdateLable(long userId, long labelId, string labelname);

        public IEnumerable<UserLabelEntity> GetAllLabels(long userId);

        public UserLabelEntity DeleteLabel(long userId, long labelId);
    }
}
