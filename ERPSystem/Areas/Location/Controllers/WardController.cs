using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPLibrary.AjaxServerSide;
using ERPLibrary.Helpers;
using ERPSystem.Core.UnitOfWorks;
using ERPSystem.Models;

namespace ERPSystem.Areas.Location.Controllers
{
    public class WardController : Controller
    {

        UnitOfWork unitOfWork = new UnitOfWork(new ERPDatabaseEntities());
        OperationResult operationResult = new OperationResult();

        // GET: Location/Ward
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetEntities()
        {
            var rep = unitOfWork.BusinessEntityRepository.WardRepository.GetAll();

            var eventList = (from e in rep
                             select new
                             {
                                 WardID = e.WardID,
                                 WardName = e.WardName,
                                 EnglishName = e.EnglishName,
                                 Level = e.Level,
                                 DistrictID = e.DistrictID,
                                 DistrictName = e.District.DistrictName
                             });
            // phải trả về eventList.FirstOrDefault(); vì eventList.ToArray(); trả về một list
            var rows = eventList.ToList();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Create(Ward entity)
        {

            operationResult = unitOfWork.BusinessEntityRepository.WardRepository.Add(entity);
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

        public JsonResult GetID(string id)
        {
            var rep = unitOfWork.BusinessEntityRepository.WardRepository.FindBy(x => x.WardID == id);

            var eventList = from e in rep
                            select new
                            {
                                WardID = e.WardID,
                                WardName = e.WardName,
                                EnglishName = e.EnglishName,
                                Level = e.Level,
                                DistrictID = e.DistrictID,
                                DistrictName = e.District.DistrictName
                            };
            // phải trả về eventList.FirstOrDefault(); vì eventList.ToArray(); trả về một list
            var rows = eventList.FirstOrDefault();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Edit(Ward entity)
        {

            operationResult = unitOfWork.BusinessEntityRepository.WardRepository.Update(entity, x => x.WardID == entity.WardID);
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
        public JsonResult Remove(string id)
        {
            var current = unitOfWork.BusinessEntityRepository.WardRepository.FindBy(x => x.WardID == id).FirstOrDefault();
            operationResult = unitOfWork.BusinessEntityRepository.WardRepository.Remove(current);
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
            var a = unitOfWork.BusinessEntityRepository.WardRepository.GetAll().ToList();
            operationResult = unitOfWork.BusinessEntityRepository.WardRepository.RemoveRange(a);
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
        /// GET: /Ward/getDataServerSide   
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
                List<Ward> data = unitOfWork.BusinessEntityRepository.WardRepository.GetResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch);

                int count = unitOfWork.BusinessEntityRepository.WardRepository.Count(param.Search.Value, columnSearch);

                data = unitOfWork.BusinessEntityRepository.WardRepository.SortByColumnWithOrder(param.SortOrder.ToString(), param.Order[0].Dir.ToString(), data);

                DTResult<Ward> result = new DTResult<Ward>
                {
                    draw = param.Draw,
                    data = data,
                    recordsFiltered = count,
                    recordsTotal = count
                };

                return Json(new
                {
                    draw = param.Draw,
                    data = result.data.ToList().Select(e => new
                    {
                        WardID = e.WardID,
                        WardName = e.WardName,
                        EnglishName = e.EnglishName,
                        Level = e.Level,
                        DistrictID = e.District.DistrictID,
                        DistrictName = e.District.DistrictName
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
