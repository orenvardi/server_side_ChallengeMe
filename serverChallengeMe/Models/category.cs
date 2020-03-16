using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using serverChallengeMe.Models.DAL;
namespace serverChallengeMe.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public Category() { }

        public Category(int categoryID, string categoryName)
        {
            CategoryID = categoryID;
            CategoryName = categoryName;
        }

        //public DataTable getCategory()
        //{
        //    DBservices dBservices = new DBservices();
        //    return dBservices.getCategory();
        //}

        //public int postCategory(Category category)
        //{
        //    DBservices dbs = new DBservices();
        //    return dbs.postCategory(category);
        //}

        //public int deleteCategory(int id)
        //{
        //    DBservices dbs = new DBservices();
        //    return dbs.deleteCategory(id);
        //}
    }
}