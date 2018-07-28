using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace HomeWorkWeek1.ActionFilter
{
    public class LogActionFilterAttribute : ActionFilterAttribute
    {
     
       
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {

            System.Diagnostics.Debug.Print(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "rn");
          
                    
            }

            public override void OnActionExecuted(ActionExecutedContext filterContext)
            {
                Thread.Sleep(3000);
            System.Diagnostics.Debug.Print(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "rn");
        }

            public override void OnResultExecuting(ResultExecutingContext filterContext)
            {
                Thread.Sleep(3000);
            System.Diagnostics.Debug.Print(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "rn");
        }

            public override void OnResultExecuted(ResultExecutedContext filterContext)
            {
                Thread.Sleep(3000);
            System.Diagnostics.Debug.Print(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "rn");
        }
        }
  


}