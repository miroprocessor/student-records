using Microsoft.AspNetCore.Mvc;
using StudentRecords.Api.Services;
using StudentRecords.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentRecords.Api.Controllers
{
    [ApiController]
    [Route("students")]
    public class StudentsController : ControllerBase
    {
        private readonly StudentService _studentsService;

        public StudentsController(StudentService studentsService)
        {
            _studentsService = studentsService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> Get()
        {
            var result = await _studentsService.GetStudents();
            return CreatedAtAction(nameof(Get), result);
        }

        [HttpGet]
        [Route("search/{keyword}")]
        public async Task<ActionResult<IEnumerable<Student>>> Search(string keyword)
        {
            var result = await _studentsService.Search(keyword);
            return CreatedAtAction(nameof(Search), result);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<IEnumerable<Student>>> Get(int id)
        {
            var result = await _studentsService.GetStudent(id);
            return CreatedAtAction(nameof(Get), result);
        }

        [HttpPost]
        public async Task<ActionResult<Student>> Post(Student student)
        {
            return CreatedAtAction(nameof(Post), await _studentsService.Add(student));
        }

        [HttpPut]
        [Route("edit")]
        public async Task<ActionResult<Student>> Put(Student student)
        {
            return CreatedAtAction(nameof(Post), await _studentsService.Edit(student));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _studentsService.Delete(id);
            return CreatedAtAction(nameof(Post), Ok());
        }

        [HttpGet]
        [Route("files/{fileId:int}")]
        public async Task<ActionResult<byte[]>> GetFile(int fileId)
        {
            var studentFile = await _studentsService.GetFile(fileId);
            if (studentFile == null)
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(Post), studentFile.Contents);
        }
    }
}
