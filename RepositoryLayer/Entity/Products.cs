using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*Fields: product_id, product_name, description, price, category_id, quantity_available
Create table and write apis for inserting data into table and search data using product_name.*/
namespace RepositoryLayer.Entity
{
    public class Products
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int product_id {  get; set; }
        public string product_name { get; set;}
        public string description { get; set;}
        public double price { get; set;}

    }
}
