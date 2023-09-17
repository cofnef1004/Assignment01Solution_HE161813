using BusinessObject.Models;
using DataAccess.DAO;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
	public class CategoryRepository : ICategoryRepository
	{
		public List<Category> GetCategories()
		{
			return CategoryDAO.GetAllCategories();
		}

        public Category GetCategoryById(int id) =>  CategoryDAO.GetCategoryById(id);
    }
}
