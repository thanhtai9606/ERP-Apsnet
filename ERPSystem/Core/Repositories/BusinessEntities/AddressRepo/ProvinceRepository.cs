using ERPSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPSystem.Core.Repositories.BusinessEntities.AddressRepo
{
    public class ProvinceRepository : Repository<Province>
    {

        public ProvinceRepository(ERPDatabaseEntities context) : base(context) { }

        public List<Province> GetResult(string search, string sortOrder, int start, int length, List<string> columnFilters)
        {
            return FilterResult(search, columnFilters).Skip(start).Take(length).ToList();
        }

        public int Count(string search, List<string> columnFilters)
        {
            return FilterResult(search, columnFilters).Count();
        }

        private IQueryable<Province> FilterResult(string search, List<string> columnFilters)
        {
            IQueryable<Province> results = Context.Provinces.ToList().AsQueryable();

            results = results.Where(p => (search == null || (p.ProvinceName != null && p.ProvinceName.ToLower().Contains(search.ToLower()) || p.EnglishName != null && p.EnglishName.ToLower().Contains(search.ToLower())
                || p.ProvinceID != null && p.ProvinceID.ToLower().Contains(search.ToLower()) || p.Level != null && p.Level.ToLower().Contains(search.ToLower())))
                && (columnFilters[0] == null || (p.ProvinceID != null && p.ProvinceID.ToLower().Contains(columnFilters[0].ToLower())))
                && (columnFilters[1] == null || (p.ProvinceName != null && p.ProvinceName.ToLower().Contains(columnFilters[1].ToLower())))
                && (columnFilters[2] == null || (p.EnglishName != null && p.EnglishName.ToLower().Contains(columnFilters[2].ToLower())))
                && (columnFilters[3] == null || (p.Level != null && p.Level.ToLower().Contains(columnFilters[3].ToLower())))
                );

            return results;
        }

        #region
        /// <summary>  
        /// Sort by column with order method.   
        /// </summary>  
        /// <param name="order">Order parameter</param>  
        /// <param name="orderDir">Order direction parameter</param>  
        /// <param name="data">Data parameter</param>  
        /// <returns>Returns - Data</returns>  
        public List<Province> SortByColumnWithOrder(string order, string orderDir, List<Province> data)
        {
            // Initialization.   
            List<Province> lst = new List<Province>();
            try
            {
                // Sorting   
                switch (order)
                {
                    case "0":
                        // Setting.   
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.ProvinceID).ToList() : data.OrderBy(p => p.ProvinceID).ToList();
                        break;
                    case "1":
                        // Setting.   
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.ProvinceName).ToList() : data.OrderBy(p => p.ProvinceName).ToList();
                        break;
                    case "2":
                        // Setting.   
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.EnglishName).ToList() : data.OrderBy(p => p.EnglishName).ToList();
                        break;
                    case "3":
                        // Setting.   
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Level).ToList() : data.OrderBy(p => p.Level).ToList();
                        break;
                    default:
                        // Setting.   
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.ProvinceID).ToList() : data.OrderBy(p => p.ProvinceID).ToList();
                        break;
                }
            }
            catch (Exception ex)
            {
                // info.   
                Console.Write(ex);
            }
            // info.   
            return lst;
        }
        #endregion
    }
}