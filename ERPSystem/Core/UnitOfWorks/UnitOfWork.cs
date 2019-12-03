using ERPSystem.Core.Repositories.BusinessEntities;
using ERPSystem.Core.Repositories.Globalization;
using ERPSystem.Core.Repositories.GrantPermissionAdmin;
using ERPSystem.Core.Repositories.HR;
using ERPSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPSystem.Core.UnitOfWorks
{
    public class UnitOfWork
    {
        private ERPDatabaseEntities _context;
        public BusinessEntityRepository BusinessEntityRepository { set; get; }
        public CultureRepository CultureRepository { set; get; }
        public GrantPermissionAdmin GrantPermissionAdmin { set; get; }
        public HRRepository HRRepository { set; get; }
        public UnitOfWork(ERPDatabaseEntities context)
        {
            this._context = context;
            BusinessEntityRepository = new BusinessEntityRepository(context);
            CultureRepository = new CultureRepository(context);
            GrantPermissionAdmin = new GrantPermissionAdmin(context);
            HRRepository = new HRRepository(context);
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }

        /// <summary>
        /// Disposes the current object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes all external resources.
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }
    }
}