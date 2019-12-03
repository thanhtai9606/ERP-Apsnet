using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPLibrary.Helpers;
using ERPSystem.Core.UnitOfWorks;
using ERPSystem.Models;
using ERPSystem.Models.BusinessModel;
using ERPLibrary.AjaxServerSide;

namespace ERPSystem.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
    public class PermissionController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork(new ERPDatabaseEntities());
        OperationResult operationResult = new OperationResult();
        // GET: Adminstrator/Member
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetEntities()
        {
            var rep = unitOfWork.GrantPermissionAdmin.PermissionRepository.GetAll();

            var eventList = from e in rep
                            select new
                            {
                                PermissionId = e.PermissionId,
                                PermissionName = e.PermissionName,
                                Description = e.Description,
                                DateModified = e.DateModified.ToString(),
                                BusinessId = e.BusinessId
                            };
            // phải trả về eventList.FirstOrDefault(); vì eventList.ToArray(); trả về một list
            var rows = eventList.ToList();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Create(Permission entity)
        {

            operationResult = unitOfWork.GrantPermissionAdmin.PermissionRepository.Add(entity);
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
            var rep = unitOfWork.GrantPermissionAdmin.PermissionRepository.FindBy(x => x.PermissionId == id);

            var eventList = from e in rep
                            select new
                            {
                                PermissionId = e.PermissionId,
                                PermissionName = e.PermissionName,
                                Description = e.Description,
                                BusinessId = e.BusinessId
                            };
            // phải trả về eventList.FirstOrDefault(); vì eventList.ToArray(); trả về một list
            var rows = eventList.FirstOrDefault();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Edit(Permission entity)
        {
            entity.DateModified = DateTime.Now;
            operationResult = unitOfWork.GrantPermissionAdmin.PermissionRepository.Update(entity, x => x.PermissionId == entity.PermissionId);
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
            var current = unitOfWork.GrantPermissionAdmin.PermissionRepository.FindBy(x => x.PermissionId == id).FirstOrDefault();
            operationResult = unitOfWork.GrantPermissionAdmin.PermissionRepository.Remove(current);
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
        public JsonResult Delete()
        {
            var e = unitOfWork.GrantPermissionAdmin.PermissionRepository.GetAll().ToList();
            operationResult = unitOfWork.GrantPermissionAdmin.PermissionRepository.RemoveRange(e);
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

        #region
        /// <summary>  
        /// GET: /Province/getDataServerSide   
        /// </summary>  
        /// <returns>Return data</returns>  
        public JsonResult getDataServerSide(DTParameters param)
        {
            try
            {
                #region
                /// <summary>  
                /// search by value with listString.   
                /// </summary>  
                /// <param name="Search.Value">Search.Value parameter</param>  

                List<String> columnSearch = new List<string>();

                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }
                #endregion
                List<Permission> data = unitOfWork.GrantPermissionAdmin.PermissionRepository.GetResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch);

                int count = unitOfWork.GrantPermissionAdmin.PermissionRepository.Count(param.Search.Value, columnSearch);

                data = unitOfWork.GrantPermissionAdmin.PermissionRepository.SortByColumnWithOrder(param.SortOrder.ToString(), param.Order[0].Dir.ToString(), data);
                #region
                //If we have no foregin Keys related to other tables in list. We can use it
                DTResult<Permission> result = new DTResult<Permission>
                {
                    draw = param.Draw,
                    data = data,
                    recordsFiltered = count,
                    recordsTotal = count
                };
                #endregion
                return Json(new
                {
                    draw = param.Draw,
                    data = result.data.ToList().Select(e => new
                    {
                        PermissionId = e.PermissionId,
                        PermissionName = e.PermissionName,
                        Description = e.Description,
                        BusinessId = e.BusinessId,
                        DateModified = e.DateModified.ToString()
                    }),
                    recordsFiltered = count,
                    recordsTotal = count
                });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        #endregion
    }
}