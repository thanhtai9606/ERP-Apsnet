using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace ERPSystem.Models.BusinessModel
{
    public class ReflectionController
    {
        //Get all List Controller
        public List<Type> GetController(string namespaces)
        {
            List<Type> listController = new List<Type>();
            Assembly assembly = Assembly.GetExecutingAssembly();
            IEnumerable<Type> types = assembly.GetTypes().Where(type => typeof(Controller).IsAssignableFrom(type) && type.Namespace.Contains
             (namespaces)).OrderBy(x => x.Name);
            return types.ToList();
        }
        //Get all Actions in Controller
        public List<string> GetActions(Type controller)
        {
            List<string> listActions = new List<string>();
            IEnumerable<MemberInfo> memberInfo = controller.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly |
                BindingFlags.Public).Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute),
                true).Any()).OrderBy(x => x.Name);
            foreach (MemberInfo method in memberInfo)
            {
                if (method.ReflectedType.IsPublic && !method.IsDefined(typeof(NonActionAttribute)))
                {
                    listActions.Add(method.Name.ToString());
                }
            }
            return listActions;
        }
    }
}