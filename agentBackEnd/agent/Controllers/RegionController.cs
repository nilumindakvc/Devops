using agent.DTOs;
using agent.entityClasses;
using agent.TableInteraction.TableSpecificInerfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace agent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegion _regionTable;
        private readonly IMapper _mapper;

        public RegionController(IRegion regionTable, IMapper mapper, ICountry countryTable)
        {
            _regionTable = regionTable;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<Region>> GetAllRegions()
        {
            List<Region> regions = _regionTable.getAll();
            List<RegionDTO> regionDTOs = _mapper.Map<List<RegionDTO>>(regions);
            return Ok(regions);
        }

        [HttpGet("countries/{regionId:int}")]
        public ActionResult<List<Country>?> GetCountriesbyRegion(int regionId)
        {
            var countries = _regionTable.getCountriesbyRegion(regionId);
            return Ok(countries);
        }

        [HttpPost]
        public ActionResult<String> PostRegion([FromBody] RegionDTO model)
        {
            Region newRegion = new Region();
            newRegion = _mapper.Map<Region>(model);
            var result = _regionTable.addRecord(newRegion);
            return Ok("Region created successfully with ID: " + result.RegionId);
        }
    }
}