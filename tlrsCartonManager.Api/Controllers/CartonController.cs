using AutoMapper;
using tlrsCartonManager.DAL.Dtos;
using tlrsCartonManager.DAL.Reporsitory.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Helper;
using tlrsCartonManager.Api.Extensions;
using tlrsCartonManager.DAL.Models.ResponseModels;
using tlrsCartonManager.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using tlrsCartonManager.Api.Error;
using System.Net;

namespace tlrsCartonManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CartonController : Controller
    {
        private readonly ICartonStorageManagerRepository _cartonRepository;

        public CartonController(ICartonStorageManagerRepository cartonRepository)
        {
            _cartonRepository = cartonRepository;
        }

        [HttpGet("getCarton")]
        public async Task<ActionResult<CartonStorageSearchDto>> SearchCarton(string columnValue, int pageIndex, int pageSize)
        {
            var cartonList = await _cartonRepository.SearchCarton(columnValue, pageIndex, pageSize);
            return Ok(cartonList);
        }

        [HttpGet("getCartonBy/{cartonId}")]
        public async Task<ActionResult<CartonStorageDto>> GetSingleSearch(int cartonId)
        {
            var carton = await _cartonRepository.GetCartonById(cartonId);
            if(carton != null)
                return Ok(carton);
            else              
                return new JsonErrorResult(new { Message = "Carton Not Found" }, HttpStatusCode.NotFound);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCartonType(CartonStorageDto carton)
        {
            await _cartonRepository.UpdateCarton(carton);
            return new JsonErrorResult(new { Message = "Carton  Updated" }, HttpStatusCode.OK);
        }
        //to do request tab
    }
}
