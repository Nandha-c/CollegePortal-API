using CollegePortal.Db;
using CollegePortal.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegePortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public StudentController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<List<Student>> GetAllStudent()
        {
            var students = await _dbContext.Students.Select(x => x).ToListAsync();
            return students;
        }

        [HttpGet("{id}")]
        public async Task<Student> GetStudentDetailsByID(int id)
        {
            var students = await _dbContext.Students.Where(x => x.StudentID == id).FirstOrDefaultAsync();
            return students;
        }

        [HttpPost]
        public Task PostStudentDetail(StudentDto student)
        {
            var students = PostStudentDetails(student);
            _dbContext.Students.Add(students);
            _dbContext.SaveChanges();

            return Task.CompletedTask;
        }

        private static  Student PostStudentDetails(StudentDto student)
        {
            return new Student()
            {
                StudentName = student.StudentName,
                Course = student.Course,
                Specialization = student.Specialization,
                Percentage = student.Percentage,
                DepartmentID = student.DepartmentID,

            };
        }
        [HttpPut]
        public Task UpdateStudentDetail(StudentDto student)
        {
              var students= _dbContext.Students.Where(x => x.StudentID == student.StudentID).FirstOrDefault();
            students.StudentName=student.StudentName;
            students.Course=student.Course;
            students.Specialization=student.Specialization;
            students.Percentage=student.Percentage;
            students.DepartmentID=student.DepartmentID;
            _dbContext.Update(students);
            _dbContext.SaveChanges();

            return Task.CompletedTask;

        }

        [HttpDelete("{id}")]
        public Task DeleteStudentDetails(int id)
        {
            var students= _dbContext.Students.FirstOrDefault(x => x.StudentID == id);
            if(students==null)
            {
                throw new Exception("There is no Data founded ");
            }
            _dbContext.Remove(students);
            _dbContext.SaveChanges();
            return Task.CompletedTask;
        }
    }
}


