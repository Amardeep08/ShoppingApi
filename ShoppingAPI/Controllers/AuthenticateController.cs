using ShoppingAPI.Data;
using ShoppingAPI.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace ShoppingAPI.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class AuthenticateController : ApiController
    {
        // POST: api/Authenticate
        public HttpResponseMessage Post([FromBody]tbl_user user)
        {
            if (UserSecurity.Login(user.username,user.password))
            {
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(user.username), null);
                using (angularShoppingEntities dbEntities = new angularShoppingEntities())
                {
                    tbl_user loginUser= dbEntities.tbl_user.Where(x => x.username.Equals(user.username, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                    return Request.CreateResponse(HttpStatusCode.OK,loginUser);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
        }

       
    }
}