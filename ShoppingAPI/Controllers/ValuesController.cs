using ShoppingAPI.Data;
using ShoppingAPI.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace ShoppingAPI.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    //[RequireHttps]
    //[BasicAuthentication]
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<tbl_user> Get()
        {
            //string username = Thread.CurrentPrincipal.Identity.Name;
            using (angularShoppingEntities dbEntities = new angularShoppingEntities())
            {
                return dbEntities.tbl_user.ToList();
            }
        }

        // GET api/values/5
        public HttpResponseMessage Get(int id)
        {
            using (angularShoppingEntities dbEntities = new angularShoppingEntities())
            {
                var entity= dbEntities.tbl_user.Where(x=>x.id==id).FirstOrDefault();
                if (entity!=null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "user with id: "+id+" not found");
                }
            }
        }

        // POST api/values
        public HttpResponseMessage Post([FromBody]tbl_user value)
        {
            try
            {
                using (angularShoppingEntities dbEntities = new angularShoppingEntities())
                {
                    var entity = dbEntities.tbl_user.Where(x => x.username.Equals(value.username,StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                    if (entity != null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.Conflict, "user name already exist.");
                    }
                    else
                    {
                        dbEntities.tbl_user.Add(value);
                        dbEntities.SaveChanges();
                        var message = Request.CreateResponse(HttpStatusCode.Created, value);
                        message.Headers.Location = new Uri(Request.RequestUri + value.id.ToString());
                        return message;

                    }
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex);
            }
        }

        // PUT api/values/5
        public HttpResponseMessage Put(int id, [FromBody]tbl_user value)
        {
            try
            {
                using (angularShoppingEntities dbEntities = new angularShoppingEntities())
                {
                    var entity = dbEntities.tbl_user.Where(x => x.id == id).FirstOrDefault();
                    if (entity != null)
                    {
                        entity.firstName = value.firstName;
                        entity.lastName = value.lastName;
                        entity.username = value.username;
                        entity.emailId = value.emailId; 
                        dbEntities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Operation Successfull");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "user with id: " + id + " not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    

        // DELETE api/values/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (angularShoppingEntities dbEntities = new angularShoppingEntities())
                {
                    var entity = dbEntities.tbl_user.Where(x => x.id == id).FirstOrDefault();
                    if (entity != null)
                    {
                        dbEntities.tbl_user.Remove(entity);
                        dbEntities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK,"Operation Successfull");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "user with id: " + id + " not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
