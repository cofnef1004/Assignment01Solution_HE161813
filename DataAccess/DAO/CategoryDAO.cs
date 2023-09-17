using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
	public class CategoryDAO
	{
		public static List<Category> GetAllCategories()
		{
			using (var _context = new Prn231As1Context())
			{
				return _context.Categories.ToList();
			}
		}

        public static Category GetCategoryById(int cateId)
        {
            using (var _context = new Prn231As1Context())
            {
                return _context.Categories.SingleOrDefault(p => p.CategoryId == cateId);
            }
        }
    }
}
