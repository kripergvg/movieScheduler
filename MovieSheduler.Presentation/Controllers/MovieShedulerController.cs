using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieSheduler.Presentation.Core;

namespace MovieSheduler.Presentation.Controllers
{
    public abstract class MovieShedulerController : Controller
    {
        protected void SetMessage(string message, MessageType messageType)
        {
            TempData.Add();
        }
    }
}