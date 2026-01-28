using agent.DTOs;
using agent.entityClasses;
using agent.TableInteraction.TableSpecificInerfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace agent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgencyController : ControllerBase
    {
        private readonly IAgency _agencyTable;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IAgencyCountry _agencyCountry;

        public AgencyController(IAgency agencyTable, IMapper mapper, IWebHostEnvironment env, IAgencyCountry agencyCountry)
        {
            _agencyTable = agencyTable;
            _mapper = mapper;
            _env = env;
            _agencyCountry = agencyCountry;
        }

        [HttpGet]
        public ActionResult<List<AgencyOutDTO>> GetAllAgencies()
        {
            List<Agency> agencies = new List<Agency>();
            agencies = _agencyTable.getAll();

            var agencyDTOs = new List<AgencyOutDTO>();
            agencyDTOs = _mapper.Map<List<AgencyOutDTO>>(agencies);
            return agencyDTOs;
        }

        [HttpPost]
        [Route("createAgency")]
        public ActionResult<AgencyOutDTO> CreateAgency([FromBody] AgencyInDTO request)
        {
            var newAgency = new Agency();

            newAgency = _mapper.Map<Agency>(request);
            var result = _agencyTable.addRecord(newAgency);

            return _mapper.Map<AgencyOutDTO>(result);
        }

        [HttpGet("{license}")]
        public ActionResult<AgencyInDTO> GetAgencyByLicense(string license)
        {
            var agency = _agencyTable.getRecordByProperty(a => a.LicenseNumber == license);
            if (agency == null)
            {
                return NotFound();
            }
            var agencyDTO = _mapper.Map<AgencyInDTO>(agency);
            return agencyDTO;
        }

        [HttpGet("Logo/{license}")]
        public ActionResult<string> GetLogobyLicense(string license)
        {
            var agency = _agencyTable.getRecordByProperty(a => a.LicenseNumber == license);
            if (agency == null)
            {
                return NotFound();
            }

            var logoPath = agency.LogoPath;

            if (logoPath != "" && logoPath != null)
            {
                byte[] imageBytes = System.IO.File.ReadAllBytes(logoPath);
                string base64Image = Convert.ToBase64String(imageBytes);

                return base64Image;
            }
            return NotFound();
        }

        [HttpPut("{license}")]
        public ActionResult<AgencyInDTO> UpdateAgencybyLicense(string license, [FromBody] AgencyInDTO model)
        {
            var agency = _agencyTable.getRecordByProperty(a => a.LicenseNumber == license);
            if (agency == null)
            {
                return NotFound();
            }

            agency.AgencyName = model.AgencyName;
            agency.LicenseNumber = model.LicenseNumber;
            agency.Email = model.Email;
            agency.Phone = model.Phone;
            agency.Address = model.Address;
            agency.City = model.City;
            agency.Country = model.Country;
            agency.Website = model.Website;
            agency.Description = model.Description;

            _agencyTable.updateRecord(agency);

            return model;
        }

        [HttpDelete("{id:int}")]
        public ActionResult<bool> DeleteAgency(int id)
        {
            var agency = _agencyTable.getRecordByProperty(a => a.AgencyId == id);
            if (agency == null)
            {
                return NotFound();
            }
            var result = _agencyTable.deleteRecord(agency);
            return NoContent();
        }

        [HttpPost("CreateAgencyCountry")]
        public ActionResult CreateAgencyCountry([FromBody] AgencyCountryDTO model)
        {
            AgencyCountry AgencyCountry = _mapper.Map<AgencyCountry>(model);
            var result = _agencyCountry.addRecord(AgencyCountry);
            return Ok(result);
        }

        [HttpGet("AgencyCountries")]
        public ActionResult<List<AgencyCountryDTO>> GetAllAgencyCountries()
        {
            var agencyCountriesList = _agencyCountry.getAll();
            var result = _mapper.Map<List<AgencyCountryDTO>>(agencyCountriesList);
            return Ok(result);
        }

        [HttpPost("Uploads/{license}")]
        public async Task<ActionResult> Uploads(string license, [FromForm] UploadModel file)
        {
            var agency = _agencyTable.getRecordByProperty(a => a.LicenseNumber == license);

            if (agency == null)
            {
                return NotFound("no agency registered under that license number");
            }

            if (file.Logo == null || file.Doc1 == null || file.Doc2 == null)
            {
                return BadRequest("All files are required.");
            }

            var webRoot = _env.WebRootPath;

            var logoPath = await SaveToFolder(file.Logo, Path.Combine(webRoot, "logo"));
            var doc1Path = await SaveToFolder(file.Doc1, Path.Combine(webRoot, "doc1"));
            var doc2Path = await SaveToFolder(file.Doc2, Path.Combine(webRoot, "doc2"));

            agency.LogoPath = logoPath;
            agency.Doc1Path = doc1Path;
            agency.Doc2Path = doc2Path;

            _agencyTable.updateRecord(agency);

            return Ok(new
            {
                logo = "/logo/" + Path.GetFileName(logoPath),
                doc1 = "/doc1/" + Path.GetFileName(doc1Path),
                doc2 = "/doc2/" + Path.GetFileName(doc2Path)
            });

            async Task<string> SaveToFolder(IFormFile file, string folderPath)
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var fullPath = Path.Combine(folderPath, uniqueFileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return fullPath;
            }
        }
    }
}