using ERPLibrary.Helpers;
using ERPSystem.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPSystem.Areas.Admin.Controllers
{
    public class AuthenticationController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork(new Models.ERPDatabaseEntities());
        OperationResult operationResult = new OperationResult();

        public JsonResult GetEntities()
        {
            var rep = unitOfWork.HRRepository.EmployeeRepository.GetAll();

            var eventList = from e in rep
                            select new
                            {
                                BusinessEntityID = e.BusinessEntityID,
                                NationalIDNumber = e.NationalIDNumber,
                                EmpCode = e.EmpCode,
                                Position =e.Position,
                                Level = e.Level,
                                MaritalStatus = e.MaritalStatus,
                                Gender = e.Gender,
                                HireDate = e.HireDate.ToShortDateString(),
                                BirthDate = e.BirthDate.ToShortDateString(),
                                JobTitle = e.JobTitle,
                                Avatar = e.Avatar
                              
                            };
            // phải trả về eventList.FirstOrDefault(); vì eventList.ToArray(); trả về một list
            var rows = eventList.ToList();
            return Json(rows , JsonRequestBehavior.AllowGet);          
        }

        public OperationResult Validate(string Username, string Password)
        {
            try
            {
                var user = unitOfWork.HRRepository.EmployeeRepository.FindBy(x=>x.EmpCode == Username && x.Person.Password.PasswordHash == Password).FirstOrDefault();
              
                if (user != null)
                {
                    Session["LoginID"] = user.BusinessEntityID;
                    Session["FullName"] = user.Person.FirstName;
                    Session["Username"] = user.EmpCode;
                    Session["Avatar"] = user.Avatar;
                    operationResult.Success = true;
                    operationResult.Message = "Đăng nhập thành công!";
                }
                else
                {
                    operationResult.Success = false;
                    operationResult.Message = "Tên đăng nhập hoặc mật khẩu không đúng!";
                }


            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Somethings wrong here " + ex.ToString();
            }

            return operationResult;

        }
        public JsonResult ValidateLogin(string Username, string Password)
        {
            operationResult = Validate(Username, Password);
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
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session["LoginID"] = null;
            Session["FullName"] = null;
            Session["Username"] = null;
            Session["Avatar"] = null;
            return RedirectToAction("Login");
        }
        public ActionResult NotificationAuthorize()
        {
            return View();
        }

    }
}