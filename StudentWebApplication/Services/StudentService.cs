
using StudentWebApplication.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace StudentWebApplication.Services
{
    public class StudentService
    {
        private readonly HttpClient _httpClient;

        public StudentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Student>> GetAllAsync() =>
            await _httpClient.GetFromJsonAsync<List<Student>>("https://localhost:7186/api/student");

        public async Task<Student> GetByIdAsync(int id) =>
            await _httpClient.GetFromJsonAsync<Student>($"https://localhost:7186/api/student/{id}");

        public async Task AddAsync(Student student) =>
            await _httpClient.PostAsJsonAsync("https://localhost:7186/api/student", student);

        public async Task UpdateAsync(Student student) =>
            await _httpClient.PutAsJsonAsync($"https://localhost:7186/api/student/{student.Id}", student);

        public async Task DeleteAsync(int id) =>
            await _httpClient.DeleteAsync($"https://localhost:7186/api/student/{id}");
    }
}
