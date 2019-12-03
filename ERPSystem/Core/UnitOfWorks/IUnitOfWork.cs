using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPSystem.Core.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        int Complete();
    }
}