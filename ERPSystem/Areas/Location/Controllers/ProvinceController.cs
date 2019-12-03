using ERPLibrary.AjaxServerSide;
using ERPLibrary.Helpers;
using ERPSystem.Core.UnitOfWorks;
using ERPSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPSystem.Areas.Location.Controllers
{
    public class ProvinceController : Controller
    {

        UnitOfWork unitOfWork = new UnitOfWork(new ERPDatabaseEntities());
        OperationResult operationResult = new OperationResult();

        // GET: Location/Province
        public ActionResult Index()
        {

            return View();
        }

        public JsonResult GetEntities()
        {
            var rep = unitOfWork.BusinessEntityRepository.ProvinceRepository.GetAll();

            var eventList = from e in rep
                            select new
                            {
                                ProvinceID = e.ProvinceID,
                                ProvinceName = e.ProvinceName,
                                EnglishName = e.EnglishName,
                                Level = e.Level
                            };
            // phải trả về eventList.FirstOrDefault(); vì eventList.ToArray(); trả về một list
            var rows = eventList.ToList();
            return Json(rep.Select(x => new { x.ProvinceID, x.ProvinceName, x.EnglishName, x.Level }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Create(Province entity)
        {

            operationResult = unitOfWork.BusinessEntityRepository.ProvinceRepository.Add(entity);
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
            var rep = unitOfWork.BusinessEntityRepository.ProvinceRepository.FindBy(x => x.ProvinceID == id);

            var eventList = from e in rep
                            select new
                            {
                                ProvinceID = e.ProvinceID,
                                ProvinceName = e.ProvinceName,
                                EnglishName = e.EnglishName,
                                Level = e.Level
                            };
            // phải trả về eventList.FirstOrDefault(); vì eventList.ToArray(); trả về một list
            var rows = eventList.FirstOrDefault();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Edit(Province entity)
        {

            operationResult = unitOfWork.BusinessEntityRepository.ProvinceRepository.Update(entity, x => x.ProvinceID == entity.ProvinceID);
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
            var current = unitOfWork.BusinessEntityRepository.ProvinceRepository.FindBy(x => x.ProvinceID == id).FirstOrDefault();
            operationResult = unitOfWork.BusinessEntityRepository.ProvinceRepository.Remove(current);
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
            var a = unitOfWork.BusinessEntityRepository.ProvinceRepository.GetAll().ToList();
            operationResult = unitOfWork.BusinessEntityRepository.ProvinceRepository.RemoveRange(a);
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
                List<Province> data = unitOfWork.BusinessEntityRepository.ProvinceRepository.GetResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch);

                int count = unitOfWork.BusinessEntityRepository.ProvinceRepository.Count(param.Search.Value, columnSearch);

                data = unitOfWork.BusinessEntityRepository.ProvinceRepository.SortByColumnWithOrder(param.SortOrder.ToString(), param.Order[0].Dir.ToString(), data);
                #region
                //If we have no foregin Keys related to other tables in list. We can use it
                DTResult<Province> result = new DTResult<Province>
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
                        ProvinceID = e.ProvinceID,
                        ProvinceName = e.ProvinceName,
                        EnglishName = e.EnglishName,
                        Level = e.Level
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