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
    public class LabelBusiness:ILabelBusiness
    {

        private readonly ILabelRepository _labelrepo;

        public LabelBusiness(ILabelRepository labelrepo)
        {
            _labelrepo = labelrepo;
        }
        public bool AddLable(long userId, long noteId, string labelName)
        {
            return _labelrepo.AddLable(userId, noteId, labelName);
        }

        public UserLabelEntity UpdateLable(long userId, long labelId, string labelname)
        {
            return _labelrepo.UpdateLable(userId, labelId, labelname);
        }

        public IEnumerable<UserLabelEntity> GetAllLabels(long userId)
        {
            return _labelrepo.GetAllLabels(userId);
        }

        public UserLabelEntity DeleteLabel(long userId, long labelId)
        {
            return _labelrepo.DeleteLabel(userId, labelId);
        }
    }
}
