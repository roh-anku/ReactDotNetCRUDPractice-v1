using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactCRUDSupport_v1.Database;
using ReactCRUDSupport_v1.Models.Domain;
using ReactCRUDSupport_v1.Models.DTOs.User;
using ReactCRUDSupport_v1.Models.DTOs.Values;
using ReactCRUDSupport_v1.Repositories;

namespace ReactCRUDSupport_v1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ValuesDbContext _valuesDbContext;
        private readonly IMapper _mapper;
        private readonly IValuesRepository _valuesRepository;
        public ValuesController(ValuesDbContext valuesDbContext, IMapper mapper, IValuesRepository valuesRepository)
        {
            _valuesDbContext = valuesDbContext;
            _mapper = mapper;
            _valuesRepository = valuesRepository;
        }

        [HttpPost]
        [Route("AddOne")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> AddOne([FromBody] AddValuesRequestDto addValuesRequestDto)
        {
            ResponseDto response = new();

            AddDomain? addDomain = _mapper.Map<AddDomain>(addValuesRequestDto);

            if (addValuesRequestDto.Value != 0)
            {
                addDomain = await _valuesRepository.AddValue(addDomain);
                //await _valuesDbContext.AddOne.AddAsync(addDomain);
                //await _valuesDbContext.SaveChangesAsync();

                if (addDomain == null)
                {
                    response.Result = false;
                    response.Message = "Invalid value provided";
                    response.Data = null;

                    return NotFound(response);
                }
                response.Result = true;
                response.Message = "Successfully Added!";
                response.Data = addDomain;

                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPut]
        [Route("UpdateTotal/{id}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateTotal([FromRoute] Guid id, [FromBody] UpdateTotalRequestDto updateTotalRequestDto)
        {
            ResponseDto response = new();

            AddDomain? updateDomain = _mapper.Map<AddDomain>(updateTotalRequestDto);

            updateDomain = await _valuesRepository.UpdateValue(id, updateDomain);

            if (updateDomain == null)
            {

                response.Result = false;
                response.Message = "No Value found";
                response.Data = null;
                return NotFound(response);

            }
            response.Result = true;
            response.Message = "Successfull!";
            response.Data = updateDomain;
            return Ok(response);
        }

        [HttpGet]
        [Route("GetTotal")]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetTotal()
        {
            ResponseDto response = new();

            AddDomain? domain = await _valuesRepository.GetTotal();

            if (domain == null)
            {
                response.Result = false;
                response.Message = "No Value found";
                response.Data = null;
                return NotFound(response);
            }
            response.Result = true;
            response.Message = "Successfull!";
            response.Data = domain;
            return Ok(response);
        }
    }
}
