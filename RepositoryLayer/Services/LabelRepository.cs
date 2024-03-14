using MassTransit.Initializers.Variables;
using Microsoft.EntityFrameworkCore;
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
    public class LabelRepository:ILabelRepository
    {
        private readonly FundooContext fundooContext;

        public LabelRepository(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        public bool AddLable(long userId, long noteId, string labelName)
        {
            var note = fundooContext.UserNotes.Where(x => x.UserId == userId && x.NoteId == noteId).FirstOrDefault();
            if (note != null)
            {
                UserLabelEntity label = new UserLabelEntity();
                label.NoteId = noteId;
                label.LabelName = labelName;
                label.UserId = userId;
                fundooContext.Add(label);
                fundooContext.SaveChanges();
                return true;

            }
            else
            {
                return false;
            }
        }

        public UserLabelEntity UpdateLable(long userId, long labelId, string labelname)
        {
            var label = fundooContext.UserLabel.Where(x => x.UserId == userId && x.LabelId == labelId).FirstOrDefault();
            if (label != null)
            {
                label.LabelName = labelname;
                fundooContext.Entry(label).State = EntityState.Modified;
                fundooContext.SaveChanges();
                return label;
            }
            else
            {
                return null;
            }
            

        }

        public IEnumerable<UserLabelEntity> GetAllLabels(long userId)
        {
            var label = fundooContext.UserLabel.Where(x => x.UserId == userId).ToList();
            if (label != null)
            {
                return label;
            }
            else
            {
                return null;
            }

        }

        public UserLabelEntity DeleteLabel(long userId, long labelId)
        {
            var label = fundooContext.UserLabel.Where(x => x.UserId == userId && x.LabelId == labelId).FirstOrDefault();
            if (label != null)
            {
                fundooContext.Remove(label);
                fundooContext.SaveChanges();
                return label;
            }
            else
            {
                return null;
            }
            

        }
    }
}
