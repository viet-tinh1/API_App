﻿using BusinessObject.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CategoryDAO
    {
        public static List<Category> GetCategories()
        {
            var listCategories = new List<Category>();
            try
            {
                using(var context = new PRN231_Assignment1Context())
                {
                    listCategories = context.Categories.ToList();
                }
            }catch (Exception e)    
            {
                throw new Exception(e.Message);
            }
            return listCategories;
        }
    }
}
