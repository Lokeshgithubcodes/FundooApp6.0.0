using CommonLayer.Models;
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
    public class ProductRepository:IProductRepository
    {
        private FundooContext fundooContext;

        public ProductRepository(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        public Products productRegistration(PRegisterModel pregistration)
        {
            Products products = new Products();
            products.product_id = pregistration.product_id;
            products.product_name = pregistration.product_name;
            products.price = pregistration.price;
            products.description = pregistration.description;
            fundooContext.TProducts.Add(products);
            fundooContext.SaveChanges();
            return products;

        }

        public Products GetByName(string productname)
        {
            var productsDetails = fundooContext.TProducts.FirstOrDefault(x => x.product_name == productname);
            if (productsDetails != null)
            {
                return productsDetails;
            }else
            {
                return null;
            }
            

        }
    }
}
