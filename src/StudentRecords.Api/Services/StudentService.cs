using Microsoft.EntityFrameworkCore;
using StudentRecords.DB;
using StudentRecords.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentRecords.Api.Services
{
    public class StudentService
    {
        private readonly StudentsDbContext _studentsDbContext;
        public StudentService(StudentsDbContext studentsDbContext)
        {
            _studentsDbContext = studentsDbContext;
        }

        public async Task<IEnumerable<Student>> Search(string keyword)
        {
            return await _studentsDbContext.Students
                            .AsNoTracking()
                            .Where(i => i.Name.StartsWith(keyword, StringComparison.CurrentCultureIgnoreCase)
                                     || i.Grade.StartsWith(keyword, StringComparison.CurrentCultureIgnoreCase))
                            .ToListAsync();
        }

        public async Task<Student> Add(Student entity)
        {
            entity.CreatedOn = DateTime.UtcNow;
            _studentsDbContext.Students.Add(entity);
            await _studentsDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Student> Edit(Student entity)
        {
            entity.ModifiedOn = DateTime.UtcNow;
            _studentsDbContext.Students.Attach(entity);
            _studentsDbContext.Entry(entity).State = EntityState.Modified;
            await _studentsDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(int id)
        {
            var student = await _studentsDbContext.Students.SingleOrDefaultAsync(i => i.Id == id);
            _studentsDbContext.Students.Remove(student);
            await _studentsDbContext.SaveChangesAsync();
        }

        public async Task<Student> GetStudent(int id)
        {
            return await _studentsDbContext.Students.SingleOrDefaultAsync(i => i.Id == id);
        }

        public async Task<StudentFile> GetFile(int fileId)
        {
            return await _studentsDbContext.StudentsFiles
                            .AsNoTracking()
                            .SingleOrDefaultAsync(i => i.Id == fileId);
        }
    }
}
