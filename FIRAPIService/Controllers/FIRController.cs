using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FIRAPIService.Exceptions;
using FIRAPIService.Models;
using FIRAPIService.Services;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FIRAPIService.Controllers
{
    [Route("api/[controller]")]
    public class FIRController : Controller
    {
        private readonly IFIRService _fIRService;

        public FIRController(IFIRService fIRService)
        {
            this._fIRService = fIRService;
        }

        [HttpGet]
        public async Task<ActionResult<object>> Get()
        {
            try
            {
                var response = await this._fIRService.Get();
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
                var response = await this._fIRService.Get(id);
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
        public async Task<ActionResult<object>> Post([FromBody] FIR fir)
        {
            try
            {
                var response = await this._fIRService.Create(fir);
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

