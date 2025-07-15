using Application.Shared;
using Application.Teacher.Create;
using Application.Teacher.Delete;
using Application.Teacher.Get;
using Application.Teacher.Update;
using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace APiScore.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public TeacherController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(CreateRequest request)
        {
            var response = await new CreateCommand(_unitOfWork).Handle(request);
            if (response.Status == 201)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("GetById")]
        public async Task<ActionResult> GetById(int id)
        {
            var response = await new GetQuery(_unitOfWork).Handle(id);
            if (response.Status == 200)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            var response = await new GetQuery(_unitOfWork).Handle();
            if (response.Status == 200)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPut("Update")]
        public async Task<ActionResult> Update(UpdateRequest request)
        {
            var response = await new UpdateCommand(_unitOfWork).Handle(request);
            if (response.Status == 200)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        
        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await new DeleteCommand(_unitOfWork).Handle(id);
            if (response.Status == 204)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        
    }
}
