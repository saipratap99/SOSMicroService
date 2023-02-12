using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SOSRequestsAPIService.Exceptions;
using SOSRequestsAPIService.Models;
using SOSRequestsAPIService.Services;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SOSRequestsAPIService.Controllers
{
    [Route("api/[controller]")]
    public class SOSRequestController : Controller
    {
        private readonly ISOSRequestService _sOSRequestService;

        public SOSRequestController(ISOSRequestService sOSRequestService)
        {
            this._sOSRequestService = sOSRequestService;
        }

        [HttpGet]
        public async Task<ActionResult<object>> Get()
        {
            try
            {
                var response = await this._sOSRequestService.Get();
                return Ok(response);
            }
            catch (Exception e)
            {

                if (e.GetType() == typeof(BusinessException))
                    return BadRequest(e.Message);
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> Get(int id)
        {
            try
            {
                var response = await this._sOSRequestService.Get(id);
                return Ok(response);
            }
            catch (Exception e)
            {

                if (e.GetType() == typeof(BusinessException))
                    return BadRequest(e.Message);
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<object>> Post([FromBody] SOSRequest sOSRequest)
        {
            try
            {
                var response = await this._sOSRequestService.Create(sOSRequest);
                return Ok(response);
            }
            catch (Exception e)
            {

                if (e.GetType() == typeof(BusinessException))
                    return BadRequest(e.Message);
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut]
        [Route("[Action]")]
        public async Task<ActionResult<object>> AssignPolice(int sOSRequestId, int policeId)
        {
            try
            {
                var response = await this._sOSRequestService.AssignPolice(sOSRequestId, policeId);
                Console.WriteLine("Resp", response);
                return Ok(response);
            }
            catch (Exception e)
            {

                if (e.GetType() == typeof(BusinessException))
                    return BadRequest(e.Message);
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}

