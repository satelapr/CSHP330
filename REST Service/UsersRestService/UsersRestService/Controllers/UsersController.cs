using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UsersRestService.Models;

namespace UsersRestService.Controllers
{
    public class UsersController : ApiController
    {
        List<UserMode> listUserMode = new List<UserMode>();
        public UsersController()
        {
            listUserMode.Add(new UserMode() { UserID = 1, Email = "test1@gmail.com", Password = "Test@123", CreatedDate = DateTime.Now });
            listUserMode.Add(new UserMode() { UserID = 2, Email = "test2@gmail.com", Password = "Test@321", CreatedDate = DateTime.Now });
            listUserMode.Add(new UserMode() { UserID = 3, Email = "test3@gmail.com", Password = "Test@213", CreatedDate = DateTime.Now });

        }
        // GET api/<controller>
        public IEnumerable<UserMode> Get()
        {
            return listUserMode;
        }

        // GET api/<controller>/5
        public UserMode Get(int id)
        {
            return listUserMode.Where(q => q.UserID == id).FirstOrDefault();
        }

        // POST api/<controller>
        public Status Post([FromBody]UserMode oUserMode)
        {
            Status oStatus = new Status();
            try
            {
                int? lastID = listUserMode.LastOrDefault()?.UserID;
                //Adding UserID
                if (lastID.HasValue)
                    oUserMode.UserID = (int)lastID + 1;
                else
                    oUserMode.UserID = 1;

                //Adding Created Date
                oUserMode.CreatedDate = DateTime.Now;

                listUserMode.Add(oUserMode);

                oStatus.StatusCode = 200;
                oStatus.Message = "User successfully added";
                oStatus.Users = oUserMode;
                return oStatus;
            }
            catch(Exception ex)
            {
                oStatus.StatusCode = 200;
                oStatus.Message = "An error has occured while adding the user";
                return oStatus;

            }
        }

        // PUT api/<controller>/5
        public Status Put(int id, [FromBody]UserMode oUserMode)
        {
            Status oStatus = new Status();

            try
            {
                listUserMode.Where(q => q.UserID == id).ForEach(q=> { q.Email = oUserMode.Email; q.Password = oUserMode.Password; });

                oStatus.StatusCode = 200;
                oStatus.Message = "User successfully updated";
                oStatus.Users = listUserMode.Where(q => q.UserID == id).FirstOrDefault();
                return oStatus;
            }
            catch (Exception ex)
            {
                oStatus.StatusCode = 200;
                oStatus.Message = "An error has occured while updating the user";
                return oStatus;

            }
        }

        // DELETE api/<controller>/5
        public Status Delete(int id)
        {
            Status oStatus = new Status();
            try
            {
                UserMode oUserMode = listUserMode.Where(q => q.UserID == id).FirstOrDefault();
                listUserMode.Remove(oUserMode);

                oStatus.StatusCode = 200;
                oStatus.Message = "User successfully Deleted";
                return oStatus;
            }
            catch (Exception ex)
            {
                oStatus.StatusCode = 200;
                oStatus.Message = "An error has occured while deleting the user";
                return oStatus;

            }
        }
    }
}