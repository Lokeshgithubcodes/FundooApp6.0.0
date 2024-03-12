using CommonLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IProductRepository
    {
        public Products productRegistration(PRegisterModel pregistration);
        public Products GetByName(string productname);
    }
}
