using ERPSystem.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPSystem.Models.BusinessModel
{
    public class AuthorizeBusiness : ActionFilterAttribute, IDisposable
    {
        UnitOfWork unitOfWork = new UnitOfWork(new ERPDatabaseEntities());
        public void Dispose()
        {
            if (unitOfWork != null)
            {
                unitOfWork.Dispose();
                unitOfWork = null;
            }
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["LoginID"] == null)
            {
                filterContext.Result = new RedirectResult("/Admin/Authentication/Login");
                return;
            }
            int loginID = int.Parse((HttpContext.Current.Session["LoginID"].ToString()));
            string actionName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "Controller-" + filterContext.ActionDescriptor.ActionName;
            var admin = unitOfWork.HRRepository.EmployeeRepository.FindBy(x => x.BusinessEntityID == loginID).FirstOrDefault();
            if (admin != null)
                return;
            var listPermission = from p in unitOfWork.GrantPermissionAdmin.PermissionRepository.GetAll()
                                 join g in unitOfWork.GrantPermissionAdmin.GrantPermissionRepository.GetAll() on p.PermissionId equals g.PermissionId
                                 where g.BusinessEntityID == loginID
                                 select p.PermissionName;
            
            if (!listPermission.Contains(actionName))
            {
                filterContext.Result = new RedirectResult("/Admin/Authentication/NotificationAuthorize");
                return;
            }

        }
    }
}