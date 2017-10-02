using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ShoppingAPI.Security
{
    public class BasicAuthentication : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //base.OnAuthorization(actionContext);
            if(actionContext.Request.Headers.Authorization==null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
                string decodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));
                string[] usernamePasswordArray = decodedAuthenticationToken.Split(':');
                if(UserSecurity.Login(usernamePasswordArray[0], usernamePasswordArray[1]))
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(usernamePasswordArray[0]), null);
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}