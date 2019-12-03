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
    public class DistrictController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork(new ERPDatabaseEntities());
        OperationResult operationResult = new OperationResult();

        // GET: Location/District
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetEntities()
        {

            var rep = unitOfWork.BusinessEntityRepository.DistrictRepository.GetAll();

            var eventList = (from e in rep
                             select new
                             {
                                 DistrictID = e.DistrictID,
                                 DistrictName = e.DistrictName,
                                 EnglishName = e.EnglishName,
                                 Level = e.Level,
                                 ProvinceID = e.ProvinceID,
                                 ProvinceName = e.Province.ProvinceName
                             }).ToList();
            // phải trả về eventList.FirstOrDefault(); vì eventList.ToArray(); trả về một list
            var rows = eventList.ToList();
           // var rows = eventList.Where(p => (searchTerm == null || p.DistrictName != null && p.DistrictName.ToLower().Contains(searchTerm.ToLower()))).ToList();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Create(District entity)
        {

            operationResult = unitOfWork.BusinessEntityRepository.DistrictRepository.Add(entity);
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
            var rep = unitOfWork.BusinessEntityRepository.DistrictRepository.FindBy(x => x.DistrictID == id);

            var eventList = from e in rep
                            select new
                            {
                                DistrictID = e.DistrictID,
                                DistrictName = e.DistrictName,
                                EnglishName = e.EnglishName,
                                Level = e.Level,
                                ProvinceID = e.ProvinceID,
                                ProvinceName = e.Province.ProvinceName
                            };
            // phải trả về eventList.FirstOrDefault(); vì eventList.ToArray(); trả về một list
            var rows = eventList.FirstOrDefault();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Edit(District entity)
        {
            var a = entity;
            operationResult = unitOfWork.BusinessEntityRepository.DistrictRepository.Update(entity, x => x.DistrictID == entity.DistrictID);
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
            var current = unitOfWork.BusinessEntityRepository.DistrictRepository.FindBy(x => x.DistrictID == id).FirstOrDefault();
            operationResult = unitOfWork.BusinessEntityRepository.DistrictRepository.Remove(current);
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
            var a = unitOfWork.BusinessEntityRepository.DistrictRepository.GetAll().ToList();
            operationResult = unitOfWork.BusinessEntityRepository.DistrictRepository.RemoveRange(a);
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
        /// GET: /District/getDataServerSide   
        /// </summary>  
        /// <returns>Return data</returns>  
        public JsonResult getDataServerSide(DTParameters param)
        {
            try
            {

                List<String> columnSearch = new List<string>();

                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }

                List<District> data = unitOfWork.BusinessEntityRepository.DistrictRepository.GetResult(param.Search.Value, param.SortOrder, param.Start, param.Length, columnSearch);

                int count = unitOfWork.BusinessEntityRepository.DistrictRepository.Count(param.Search.Value, columnSearch);

                data = unitOfWork.BusinessEntityRepository.DistrictRepository.SortByColumnWithOrder(param.SortOrder.ToString(), param.Order[0].Dir.ToString(), data);

                #region
                //If we have no foregin Keys related to other tables in list. We can use it
                DTResult<District> result = new DTResult<District>
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
                        DistrictID = e.DistrictID,
                        DistrictName = e.DistrictName,
                        EnglishName = e.EnglishName,
                        Level = e.Level,
                        ProvinceID = e.Province.ProvinceID,
                        ProvinceName = e.Province.ProvinceName
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

        public JsonResult getDistrictServerSide(string searchTerm, int pageSize, int pageNum, int page)
        {
            //Get the paged results and the total count of the results for this query. 

            List<District> districts = unitOfWork.BusinessEntityRepository.DistrictRepository.GetDistricts(searchTerm, pageSize, pageNum);
            int Count = unitOfWork.BusinessEntityRepository.DistrictRepository.GetDistrictsCount(searchTerm, pageSize, pageNum);

            //Translate the attendees into a format the select2 dropdown expects
            Select2PagedResult Data = AttendeesToSelect2Format(districts, Count);

            //Return the data as a jsonp result
            return Json(Data, JsonRequestBehavior.AllowGet);

        }

        private Select2PagedResult AttendeesToSelect2Format(List<District> attendees, int totalAttendees)
        {
            Select2PagedResult jsonAttendees = new Select2PagedResult();
            jsonAttendees.Results = new List<Select2Result>();

            //Loop through our attendees and translate it into a text value and an id for the select list
            foreach (District a in attendees)
            {
                jsonAttendees.Results.Add(new Select2Result { id = a.DistrictID.ToString(), text = a.DistrictName });
            }
            //Set the total count of the results from the query.
            jsonAttendees.Total = totalAttendees;

            return jsonAttendees;
        }
    }

    //Extra classes to format the results the way the select2 dropdown wants them
    public class Select2PagedResult
    {
        public int Total { get; set; }
        public List<Select2Result> Results { get; set; }
    }

    public class Select2Result
    {
        public string id { get; set; }
        public string text { get; set; }
    }
}
