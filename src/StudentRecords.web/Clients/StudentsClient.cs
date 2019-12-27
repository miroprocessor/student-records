using StudentRecords.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentRecords.web.Clients
{
    public class StudentsClient : ClientBase
    {
        public StudentsClient()
           : base()
        { }

        public async Task<IEnumerable<Student>> GetStudents(string keyword)
        {
            var response = await base.HttpClient.GetAsync($"Students/search/{keyword}");
            return Deserialize<IEnumerable<Student>>(response);
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            var response = await base.HttpClient.GetAsync($"Students");
            return Deserialize<IEnumerable<Student>>(response);
        }

        public async Task<Student> GetStudent(int id)
        {
            var response = await base.HttpClient.GetAsync($"Students/{id}");
            return Deserialize<Student>(response);
        }

        public async Task<Student> AddStudent(Student Student)
        {
            var response = await base.HttpClient.PostAsync("Students", Serialize(Student));
            return Deserialize<Student>(response);
        }

        public async Task<Student> UpdateStudent(Student Student)
        {
            var response = await base.HttpClient.PutAsync("Students", Serialize(Student));
            return Deserialize<Student>(response);
        }

        public async Task<Student> DeleteStudent(int StudentId)
        {
            var response = await base.HttpClient.DeleteAsync("Students/" + StudentId);
            return Deserialize<Student>(response);
        }
    }
}
