using agent.DTOs;
using agent.entityClasses;
using agent.TableInteraction.TableSpecificInerfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace agent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _userTable;
        private readonly IAgencyReviews _agencyReviews;
        private readonly IMapper _mapper;

        public UserController(IUser userTable, IAgencyReviews agencyReviews, IMapper mapper)
        {
            _userTable = userTable;
            _agencyReviews = agencyReviews;
            _mapper = mapper;
        }

        [HttpPost("users")]
        public ActionResult CreateUsers([FromBody] List<UserDTO> users)
        {
            List<User> usersList = _mapper.Map<List<User>>(users);

            for (int i = 0; i < usersList.Count; i++)
            {
                usersList[i].PasswordHash = users[i].Password;
            }

            var result = _userTable.addRecords(usersList);
            return Ok(result);
        }

        [HttpPost("user")]
        public ActionResult CreateUser([FromBody] UserDTO user)
        {
            User newUser = _mapper.Map<User>(user);
            newUser.PasswordHash = user.Password;
            var result = _userTable.addRecord(newUser);
            return Ok(result);
        }

        [HttpPost("login")]
        public ActionResult GetUserbyEmailAndPassword([FromBody] LogInDTO request)
        {
            var requestedUser = _userTable.getRecordByProperty(a => a.Email == request.Email && a.PasswordHash == request.Password);
            if (requestedUser == null)
            {
                return NotFound();
            }
            var result = _mapper.Map<UserOutDTO>(requestedUser);
            return Ok(result);
        }

        [HttpPost("review")]
        public ActionResult PostReview([FromBody] ReviewDTO review)
        {
            AgencyReview newReview = _mapper.Map<AgencyReview>(review);
            var result = _agencyReviews.addRecord(newReview);
            return Ok(result);
        }

        [HttpPost("reveiws")]
        public ActionResult PostReviews([FromBody] List<ReviewDTO> reviews)
        {
            List<AgencyReview> newReviews = _mapper.Map<List<AgencyReview>>(reviews);
            var result = _agencyReviews.addRecords(newReviews);
            return Ok(result);
        }

        [HttpGet("reveiws")]
        public ActionResult<List<ReviewDTO>> GetAllReviews()
        {
            var allReveiws = _agencyReviews.getAll();
            var result = _mapper.Map<List<ReviewDTO>>(allReveiws);
            return Ok(result);
        }

        [HttpDelete("review/{userid:int}/{serviceNumber}")]
        public ActionResult DeleteReview(int userid, string serviceNumber)
        {
            var reviewToDelete = _agencyReviews.getRecordByProperty(r => r.UserId == userid && r.ServiceNumber == serviceNumber);
            if (reviewToDelete == null)
            {
                return NotFound();
            }
            _agencyReviews.deleteRecord(reviewToDelete);
            return Ok(reviewToDelete);
        }
    }
}