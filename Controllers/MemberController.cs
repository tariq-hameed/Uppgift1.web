using System;
using System.Web.Security;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Uppgift1.web.Models;

namespace Uppgift1.web.Controllers
{
    public class MemberController : SurfaceController
    {
        // GET: Member
        public ActionResult RenderLogin()
        {
            return PartialView("MemberLogin", new LoginModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitLogin(LoginModel model, string returnUrl)
        {
            if(ModelState.IsValid)
            {
                if(Membership.ValidateUser(model.Username, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    UrlHelper urlHelper = new UrlHelper(HttpContext.Request.RequestContext);

                    if(urlHelper.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return Redirect("/");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name and password are invalid.");
                }
            }
            return CurrentUmbracoPage();
        }

        public ActionResult SubmitLogout()
        {
            TempData.Clear();
            Session.Clear();
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
    }
}