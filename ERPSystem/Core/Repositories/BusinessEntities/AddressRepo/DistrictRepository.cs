using ERPSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPSystem.Core.Repositories.BusinessEntities.AddressRepo
{
    public class DistrictRepository : Repository<District>
    {
        public DistrictRepository(ERPDatabaseEntities context) : base(context) { }
        public List<District> GetResult(string search, string sortOrder, int start, int length, List<string> columnFilters)
        {
            return FilterResult(search, columnFilters).Skip(start).Take(length).ToList();
        }

        public int Count(string search, List<string> columnFilters)
        {
            return FilterResult(search, columnFilters).Count();
        }

        private IQueryable<District> FilterResult(string search, List<string> columnFilters)
        {
            IQueryable<District> results = Context.Districts.ToList().AsQueryable();

            results.Where(p => (search == null || (p.DistrictName != null && p.DistrictName.ToLower().Contains(search.ToLower()) || p.EnglishName != null && p.EnglishName.ToLower().Contains(search.ToLower())
                || p.DistrictID != null && p.DistrictID.ToLower().Contains(search.ToLower()) || p.Level != null && p.Level.ToLower().Contains(search.ToLower())))
                && (columnFilters[0] == null || (p.DistrictID != null && p.DistrictID.ToLower().Contains(columnFilters[0].ToLower())))
                && (columnFilters[1] == null || (p.DistrictName != null && p.DistrictName.ToLower().Contains(columnFilters[1].ToLower())))
                && (columnFilters[2] == null || (p.EnglishName != null && p.EnglishName.ToLower().Contains(columnFilters[2].ToLower())))
                && (columnFilters[3] == null || (p.Level != null && p.Level.ToLower().Contains(columnFilters[3].ToLower())))
                 && (columnFilters[4] == null || (p.ProvinceID != null && p.ProvinceID.ToLower().Contains(columnFilters[4].ToLower())))
                && (columnFilters[5] == null || (p.Province.ProvinceName != null && p.Province.ProvinceName.ToLower().Contains(columnFilters[5].ToLower())))
                );

            return results;
        }

        //Return only the results we want
        public List<District> GetDistricts(string searchTerm, int pageSize, int pageNum)
        {
            return GetDistrictsQuery(searchTerm)
                .Skip(pageSize * (pageNum - 1))
                .Take(pageSize)
                .ToList();
        }

        //And the total count of records
        public int GetDistrictsCount(string searchTerm, int pageSize, int pageNum)
        {
            return GetDistrictsQuery(searchTerm)
                .Count();
        }
        //Our search term
        private IQueryable<District> GetDistrictsQuery(string searchTerm)
        {
            searchTerm = searchTerm.ToLower();

            return Context.Districts
                .Where(
                    a => a.DistrictName.Contains(searchTerm)
                ).ToList().AsQueryable();
        }

        #region
        /// <summary>  
        /// Sort by column with order method.   
        /// </summary>  
        /// <param name="order">Order parameter</param>  
        /// <param name="orderDir">Order direction parameter</param>  
        /// <param name="data">Data parameter</param>  
        /// <returns>Returns - Data</returns>  
        public List<District> SortByColumnWithOrder(string order, string orderDir, List<District> data)
        {
            // Initialization.   
            List<District> lst = new List<District>();
            try
            {
                // Sorting   
                switch (order)
                {
                    case "0":
                        // Setting.   
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.DistrictID).ToList() : data.OrderBy(p => p.DistrictID).ToList();
                        break;
                    case "1":
                        // Setting.   
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.DistrictName).ToList() : data.OrderBy(p => p.DistrictName).ToList();
                        break;
                    case "2":
                        // Setting.   
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.EnglishName).ToList() : data.OrderBy(p => p.EnglishName).ToList();
                        break;
                    case "3":
                        // Setting.   
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Level).ToList() : data.OrderBy(p => p.Level).ToList();
                        break;
                    case "4":
                        // Setting.   
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.ProvinceID).ToList() : data.OrderBy(p => p.ProvinceID).ToList();
                        break;
                    case "5":
                        // Setting.   
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Province.ProvinceName).ToList() : data.OrderBy(p => p.Province.ProvinceName).ToList();
                        break;
                    default:
                        // Setting.   
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.DistrictID).ToList() : data.OrderBy(p => p.DistrictID).ToList();
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