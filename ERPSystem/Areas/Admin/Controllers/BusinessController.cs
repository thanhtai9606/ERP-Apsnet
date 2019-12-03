using ERPLibrary.Helpers;
using ERPSystem.Core.UnitOfWorks;
using ERPSystem.Models;
using ERPSystem.Models.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ERPSystem.Areas.Admin.Controllers
{
    public class BusinessController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork(new Models.ERPDatabaseEntities());
        OperationResult operationResult = new OperationResult();

        // GET: Admin/Business
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetEntities()
        {
            var rep = unitOfWork.GrantPermissionAdmin.BusinessRepository.GetAll();

            var eventList = from e in rep
                            select new
                            {
                                BusinessId = e.BusinessId,
                                BusinessName = e.BusinessName,
                                DateModified = e.ModifiedDate.ToString()

                            };
            // phải trả về eventList.FirstOrDefault(); vì eventList.ToArray(); trả về một list
            var rows = eventList.ToList();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetID(string id)
        {
            var rep = unitOfWork.GrantPermissionAdmin.BusinessRepository.FindBy(x => x.BusinessId == id);

            var eventList = from e in rep
                            select new
                            {
                                BusinessId = e.BusinessId,
                                BusinessName = e.BusinessName,
                                DateModified = e.ModifiedDate.ToString()
                            };
            // phải trả về eventList.FirstOrDefault(); vì eventList.ToArray(); trả về một list
            var rows = eventList.FirstOrDefault();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Edit(Business entity)
        {

            entity.ModifiedDate = DateTime.Now;
            operationResult = unitOfWork.GrantPermissionAdmin.BusinessRepository.Update(entity, x => x.BusinessId == entity.BusinessId);
            unitOfWork.Complete();
            if (operationResult.Success)
            {
                return Json(new
                {
                    statusCode = 200,
                    status = operationResult.Message,

                }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new
                {
                    statusCode = 400,
                    status = operationResult.Message,

                }, JsonRequestBehavior.AllowGet);

            }

        }
        public OperationResult GetAllBusiness()
        {
            try
            {
                ReflectionController reflectionController = new ReflectionController();
                List<Type> listControllerType = reflectionController.GetController("ERPSystem.Areas");
                List<string> listControllerOld = unitOfWork.GrantPermissionAdmin.BusinessRepository.GetAll().Select(c => c.BusinessId).ToList();
                List<string> listPermissionOld = unitOfWork.GrantPermissionAdmin.PermissionRepository.GetAll().Select(x => x.PermissionName).ToList();

                foreach (var c in listControllerType)
                {
                    if (!listControllerOld.Contains(c.Name))
                    {
                        Business b = new Business() { BusinessId = c.Name, BusinessName = "Chưa có mô tả", ModifiedDate = DateTime.Now };
                        unitOfWork.GrantPermissionAdmin.BusinessRepository.Add(b);
                    }
                    List<string> listPermission = reflectionController.GetActions(c);
                    foreach (var p in listPermission)
                    {
                        if (!listPermissionOld.Contains(c.Name + "-" + p))
                        {
                            Permission permission = new Permission { PermissionName = c.Name + "-" + p, Description = "Chưa có mô tả này", BusinessId = c.Name, DateModified= DateTime.Now };
                            unitOfWork.GrantPermissionAdmin.PermissionRepository.Add(permission);
                        }
                    }
                }
                unitOfWork.Complete();
                operationResult.Success = true;
                operationResult.Message = "Đã cập nhật thành công!";
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Lỗi " + ex.ToString();
            }
            return operationResult;
        }
        public JsonResult UpdateBusiness()
        {
            operationResult = GetAllBusiness();
            if (operationResult.Success)
            {
                return Json(new
                {
                    statusCode = 200,
                    status = operationResult.Message,

                }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new
                {
                    statusCode = 400,
                    status = operationResult.Message,

                }, JsonRequestBehavior.AllowGet);

            }
        }
        public JsonResult Remove(string id)
        {
            var current = unitOfWork.GrantPermissionAdmin.BusinessRepository.FindBy(x => x.BusinessId == id).FirstOrDefault();
            operationResult = unitOfWork.GrantPermissionAdmin.BusinessRepository.Remove(current);
            unitOfWork.Complete();
            if (operationResult.Success)
            {
                return Json(new
                {
                    statusCode = 200,
                    status = operationResult.Message,

                }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new
                {
                    statusCode = 400,
                    status = operationResult.Message,

                }, JsonRequestBehavior.AllowGet);

            }
        }
    }
}