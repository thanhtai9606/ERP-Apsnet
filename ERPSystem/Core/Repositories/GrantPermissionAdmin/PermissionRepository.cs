using ERPSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPSystem.Core.Repositories.GrantPermissionAdmin
{
    public class PermissionRepository : Repository<Permission>
    {
        public PermissionRepository(ERPDatabaseEntities context) : base(context) { }
        public List<Permission> GetResult(string search, string sortOrder, int start, int length, List<string> columnFilters)
        {
            return FilterResult(search, columnFilters).Skip(start).Take(length).ToList();
        }

        public int Count(string search, List<string> columnFilters)
        {
            return FilterResult(search, columnFilters).Count();
        }

        private IQueryable<Permission> FilterResult(string search, List<string> columnFilters)
        {
             IQueryable<Permission> results = Context.Permissions.ToList().AsQueryable();
          
    
        results = results.Where(p => (search == null || (p.PermissionName != null && p.PermissionName.ToLower().Contains(search.ToLower()) || p.Description != null && p.Description.ToLower().Contains(search.ToLower())
            || p.PermissionId.ToString() != null && p.PermissionId.ToString().ToLower().Contains(search.ToLower()) || p.Description != null && p.Description.ToLower().Contains(search.ToLower())))
            && (columnFilters[0] == null || (p.PermissionId.ToString() != null && p.PermissionId.ToString().ToLower().Contains(columnFilters[0].ToLower())))
            && (columnFilters[1] == null || (p.PermissionName != null && p.PermissionName.ToLower().Contains(columnFilters[1].ToLower())))
            && (columnFilters[2] == null || (p.Description != null && p.Description.ToLower().Contains(columnFilters[2].ToLower())))
             && (columnFilters[3] == null || (p.BusinessId != null && p.BusinessId.ToLower().Contains(columnFilters[3].ToLower())))
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
        public List<Permission> SortByColumnWithOrder(string order, string orderDir, List<Permission> data)
        {
            // Initialization.   
            List<Permission> lst = new List<Permission>();
            try
            {
                // Sorting   
                switch (order)
                {
                    case "0":
                        // Setting.   
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.PermissionId).ToList() : data.OrderBy(p => p.PermissionId).ToList();
                        break;
                    case "1":
                        // Setting.   
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.PermissionName).ToList() : data.OrderBy(p => p.PermissionName).ToList();
                        break;
                    case "2":
                        // Setting.   
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Description).ToList() : data.OrderBy(p => p.Description).ToList();
                        break;
                    case "3":
                        // Setting.   
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.BusinessId).ToList() : data.OrderBy(p => p.BusinessId).ToList();
                        break;
                    default:
                        // Setting.   
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.PermissionId).ToList() : data.OrderBy(p => p.PermissionId).ToList();
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