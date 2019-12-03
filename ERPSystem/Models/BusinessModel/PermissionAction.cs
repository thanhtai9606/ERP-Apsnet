using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPSystem.Models.BusinessModel
{
    public class PermissionActions
    {

        public int PermissionId { set; get; }
        public string PermissionName { set; get; }
        public string Description { set; get; }
        public string BusinessId { set; get; }
        public bool isGranted { set; get; }
    }
}