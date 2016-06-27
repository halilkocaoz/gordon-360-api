﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CCT_App.Models;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;

namespace CCT_App.Controllers.Api
{
    [RoutePrefix("api/memberships")]
    public class MembershipsController : ApiController
    {

        private CCTEntities database = new CCTEntities();

        public MembershipsController(CCTEntities dbContext)
        {
            database = dbContext;
        }

        // GET api/<controller>
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            var all = database.Memberships.ToList();
            return Ok(all);
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result =  database.Memberships.Find(id);

            if( result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("", Name="Memberships")]
        public IHttpActionResult Post([FromBody] Membership membership)
        {
            if(!ModelState.IsValid || membership == null)
            {
                return BadRequest();
            }

            var valid_activity_codes = database.ACTIVE_CLUBS_PER_SESS_ID(membership.SESSION_CDE).ToList();

            bool offered = false;
            string potential_activity = membership.ACT_CDE.Trim();

            foreach (ACTIVE_CLUBS_PER_SESS_ID_Result activity in valid_activity_codes)
            {
                if(potential_activity.Equals(activity.ACT_CDE.Trim()))
                {
                    offered = true;
                }
            }

            if (!offered)
            {
                return NotFound();
            }

            database.Memberships.Add(membership);
            database.SaveChanges();

            return Created("memberships", membership);

        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("")]
        public IHttpActionResult Put(int id, [FromBody]Membership membership)
        {
            if(!ModelState.IsValid || membership == null || id != membership.MEMBERSHIP_ID)
            {
                return BadRequest();
            }
            var original = database.Memberships.Find(id);

            if(original == null)
            {
                return NotFound();
            }

            database.Memberships.Attach(membership);
            database.Entry(membership).State = EntityState.Modified;
            database.SaveChanges();

            return Created("DefaultApi", membership);
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            var toDelete = database.Memberships.Find(id);
            if (toDelete == null)
            {
                return NotFound();
            }

            database.Memberships.Remove(toDelete);
            database.SaveChanges();
            return Ok();
        }
    }
}