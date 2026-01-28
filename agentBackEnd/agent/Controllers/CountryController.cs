using agent.DTOs;
using agent.entityClasses;
using agent.TableInteraction.TableSpecificInerfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace agent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountry _countryTable;
        private readonly IMapper _mapper;

        public CountryController(ICountry countryTable, IMapper mapper)
        {
            _countryTable = countryTable;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<CountryDTO>> GetAllCountries()
        {
            List<CountryDTO> countriesDTO = new List<CountryDTO>();

            var countries = _countryTable.getAll();
            countriesDTO = _mapper.Map<List<CountryDTO>>(countries);
            return Ok(countriesDTO);
        }

        [HttpGet("{countryid:int}")]
        public ActionResult<CountryDTO> GetCountryById(int countryid)
        {
            var country = _countryTable.getRecordByProperty(a => a.CountryId == countryid);
            return Ok(country);
        }

        [HttpPost]
        public ActionResult<String> CreateCountry([FromBody] CountryDTO model)
        {
            if (ModelState.IsValid)
            {
                Country newCountry = new Country();
                newCountry = _mapper.Map<Country>(model);
                var result = _countryTable.addRecord(newCountry);
                return Ok("Country created successfully with id " + result.CountryId);
            }
            else
            {
                return BadRequest("invalid data inputs");
            }
        }
    }
}