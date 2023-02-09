using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SOSReqQueueAPIService.Exceptions;
using SOSReqQueueAPIService.Services;
using SOSReqQueueAPIService.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SOSReqQueueAPIService.Controllers
{
    [Route("api/[controller]")]
    public class SOSReqQueueController : Controller
    {
        private readonly ISOSReqQueueService _sOSReqQueueService;

        public SOSReqQueueController(ISOSReqQueueService sOSReqQueueService)
        {

            Console.WriteLine("Contoller init");
            this._sOSReqQueueService = sOSReqQueueService;
        }

        [HttpGet]
        public async Task<ActionResult<object>> Get()
        {
            try
            {
                var response = await this._sOSReqQueueService.Get();
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
                var response = await this._sOSReqQueueService.Get(id);
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

        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> Delete(int id)
        {
            try
            {
                var response = await this._sOSReqQueueService.Delete(id);
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
        public async Task<ActionResult<object>> Post([FromBody] SOSReqQueue sOSReqQueue)
        {
            try
            {
                var response = await this._sOSReqQueueService.Create(sOSReqQueue);
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

