using ERPLibrary.Helpers;
using ERPSystem.Core.UnitOfWorks;
using ERPSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPSystem.Areas.Admin.Controllers
{
    public class LanguageController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork(new Models.ERPDatabaseEntities());
        OperationResult operationResult = new OperationResult();
        // GET: Admin/Language
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetEntities()
        {
            return Json(unitOfWork.CultureRepository.GlobalValueRepository.GetAll(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Create(GlobalValue entity)
        {

            operationResult = unitOfWork.CultureRepository.GlobalValueRepository.Add(entity);
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
        public JsonResult GetID(int id)
        {
            var rep = unitOfWork.CultureRepository.GlobalValueRepository.FindBy(x => x.ControlID == id);

            var eventList = from e in rep
                            select new
                            {
                                ControlID = e.ControlID,
                                ControlName = e.ControlName,
                                Description = e.Description,
                                eng = e.Eng,
                                vn = e.Vn
                            };
            // phải trả về eventList.FirstOrDefault(); vì eventList.ToArray(); trả về một list
            var rows = eventList.FirstOrDefault();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Edit(GlobalValue entity)
        {

            operationResult = unitOfWork.CultureRepository.GlobalValueRepository.Update(entity, x => x.ControlID == entity.ControlID);
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
        public JsonResult Remove(int id)
        {
            var current = unitOfWork.CultureRepository.GlobalValueRepository.FindBy(x => x.ControlID == id).FirstOrDefault();
            operationResult = unitOfWork.CultureRepository.GlobalValueRepository.Remove(current);
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
        public JsonResult GetTypeLanguage(string lang)
        {
            IEnumerable<object> data = null;
            if (lang == "Eng")
            {
               var a = unitOfWork.CultureRepository.GlobalValueRepository.GetAll().Select(x => new { x.ControlID, x.ControlName, x.Eng, x.Description });               
            }
            else
                if (lang == "Vn")
                data = unitOfWork.CultureRepository.GlobalValueRepository.GetAll().Select(x => new { x.ControlID, x.ControlName, x.Vn, x.Description });
            return Json(data.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}