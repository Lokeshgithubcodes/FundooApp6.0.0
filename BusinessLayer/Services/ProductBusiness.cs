using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class ProductBusiness:IProductBusiness
    {
        private readonly IProductRepository productRepo;

        public ProductBusiness(IProductRepository productRepo)
        {
            this.productRepo = productRepo;
        }

        public Products productRegistration(PRegisterModel pregistration)
        {
            return productRepo.productRegistration(pregistration);
        }

        public Products GetByName(string productname)
        {
            return productRepo.GetByName(productname);
        }
    }
}
