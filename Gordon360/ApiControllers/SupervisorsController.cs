﻿using System.Web.Http;
using System.Web.Http.Description;
using Gordon360.Models;
using Gordon360.Repositories;
using Gordon360.Services;
using Gordon360.AuthorizationFilters;

namespace Gordon360.Controllers.Api
{
    [RoutePrefix("api/supervisors")]
    [Authorize]
    public class SupervisorsController : ApiController
    {
        private ISupervisorService _supervisorService;



        public SupervisorsController()
        {
            var _unitOfWork = new UnitOfWork();
            _supervisorService = new SupervisorService(_unitOfWork);
        }
        public SupervisorsController(ISupervisorService supervisorService)
        {
            _supervisorService = supervisorService;
        }


        /// <summary>Get all supervisors</summary>
        /// <returns>All supervisors and their corresponding information</returns>
        /// <remarks>Queries the database for all supervisors</remarks>
        // GET: api/Supervisors
        [HttpGet]
        [Route("")]
        [AuthorizationLevel(authorizationLevel = Constants.GOD_LEVEL)]
        public IHttpActionResult Get()
        {
            var all = _supervisorService.GetAll();
            return Ok(all);
        }

        /// <summary>Get a single supervisor</summary>
        /// <param name="id">The ID of desired supervisor</param>
        /// <returns>The supervisor object that has an ID matching the one specified in the URL</returns>
        /// <remarks>Queries the database for a specific supervisor based on their Gordon ID</remarks>
        // GET: api/Supervisors/5
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _supervisorService.Get(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>Update an existing supervisor</summary>
        /// <param name="id">The id for an existing supervisor</param>
        /// <param name="supervisor">The supervisor object to be changed</param>
        /// <returns>The changed supervisor object</returns>
        /// <remarks>Queries the database to update one supervisor</remarks>
        // PUT: api/Supervisors/5
        [HttpPut]
        [Route("{id}")]
        [AuthorizationLevel(authorizationLevel = Constants.GOD_LEVEL)]
        public IHttpActionResult Put(int id, [FromBody] SUPERVISOR supervisor)
        {
            if (!ModelState.IsValid || supervisor == null || id != supervisor.SUP_ID)
            {
                return BadRequest();
            }

            var result = _supervisorService.Update(id, supervisor);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>Add a new supervisor</summary>
        /// <param name="supervisor">The name of the new supervisor</param>
        /// <returns>The new supervisor object</returns>
        /// <remarks>Queries the database to add a new supervisor into the table</remarks>
        // POST: api/Supervisors
        [ResponseType(typeof(IHttpActionResult))]
        [HttpPost]
        [Route("")]
        [AuthorizationLevel(authorizationLevel = Constants.GOD_LEVEL)]
        public IHttpActionResult Post(SUPERVISOR supervisor)
        {
            if (!ModelState.IsValid || supervisor == null)
            {
                return BadRequest();
            }

            var result = _supervisorService.Add(supervisor);

            if (result == null )
            {
                return NotFound();
            }

            return Created("DefaultApi",  supervisor);
        }

        /// <summary>Delete a supervisor</summary>
        /// <param name="id">The ID of supervisor to be deleted</param>
        /// <returns>The supervisor object that was deleted</returns>
        /// <remarks>Queries the database to remove the row of the specified supervisor</remarks>
        // DELETE: api/Supervisors/5
        [HttpDelete]
        [Route("{id}")]
        [AuthorizationLevel(authorizationLevel = Constants.GOD_LEVEL)]
        public IHttpActionResult Delete(int id)
        {

            var result = _supervisorService.Delete(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

    }
}
