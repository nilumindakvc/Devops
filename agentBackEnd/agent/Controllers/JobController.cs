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
    public class JobController : ControllerBase
    {
        private readonly IJob _jobTable;
        private readonly IMapper _mapper;
        private readonly IJobCategory _category;
        private readonly ICountry _countryTable;
        private readonly IAgency _agencyTable;

        public JobController(IJob jobTable, IMapper mapper, IJobCategory category, ICountry countryTable, IAgency agencyTable)
        {
            _jobTable = jobTable;
            _mapper = mapper;
            _category = category;
            _countryTable = countryTable;
            _agencyTable = agencyTable;
        }

        [HttpGet("categories")]
        public ActionResult<List<CategoryOutDTO>> GetAllCategories()
        {
            var categoryList = new List<JobCategory>();
            categoryList = _category.getAll();

            var returningList = _mapper.Map<List<CategoryOutDTO>>(categoryList);
            return returningList;
        }

        [HttpPost("category")]
        public ActionResult<CategoryOutDTO> CreateCategory([FromBody] CategoryInDTO model)
        {
            JobCategory newJobCategory = new JobCategory();

            newJobCategory = _mapper.Map<JobCategory>(model);

            var newrecord = _category.addRecord(newJobCategory);

            return _mapper.Map<CategoryOutDTO>(newrecord);
        }

        [HttpPost]
        public ActionResult<JobInDTO> CreateJob([FromBody] JobInDTO model)
        {
            Job newJob = new Job();
            newJob = _mapper.Map<Job>(model);
            _jobTable.addRecord(newJob);

            return model;
        }

        [HttpGet]
        public ActionResult<List<JobOutDTO>> GetAllJobs()
        {
            var jobList = _jobTable.getAll();
            var result = _mapper.Map<List<JobOutDTO>>(jobList);
            foreach (JobOutDTO job in result)
            {
                job.CountryName = _countryTable.getRecordByProperty(a => a.CountryId == job.CountryId).CountryName;
                job.AgencyName = _agencyTable.getRecordByProperty(a => a.AgencyId == job.AgencyId).AgencyName;
                job.CategoryName = _category.getRecordByProperty(a => a.CategoryId == job.CategoryId).CategoryName;
            }
            return result;
        }

        [HttpDelete("{jobId:int}")]
        public ActionResult DeleteJobByJobId(int jobId)
        {
            var jobToDelete = _jobTable.getRecordByProperty(j => j.JobId == jobId);
            if (jobToDelete == null)
            {
                return NotFound();
            }
            _jobTable.deleteRecord(jobToDelete);

            return Ok(jobToDelete);
        }

        [HttpGet("jobsbyAgency/{agencyId:int}")]
        public ActionResult<List<JobOutDTO>> GetAllgobsOfAgency_byAgencyId(int agencyId)
        {
            var jobsByAgency = _jobTable.getAllRecordsByProperty(a => a.AgencyId == agencyId);

            var result = _mapper.Map<List<JobOutDTO>>(jobsByAgency);

            foreach (JobOutDTO job in result)
            {
                job.CountryName = _countryTable.getRecordByProperty(a => a.CountryId == job.CountryId).CountryName;
                job.AgencyName = _agencyTable.getRecordByProperty(a => a.AgencyId == job.AgencyId).AgencyName;
                job.CategoryName = _category.getRecordByProperty(a => a.CategoryId == job.CategoryId).CategoryName;
            }
            return result;
        }

        [HttpPost("collection")]
        public ActionResult<List<JobInDTO>> AddJobList([FromBody] List<JobInDTO> model)
        {
            List<Job> newJobList = new List<Job>();
            newJobList = _mapper.Map<List<Job>>(model);
            _jobTable.addRecords(newJobList);
            return model;
        }
    }
}