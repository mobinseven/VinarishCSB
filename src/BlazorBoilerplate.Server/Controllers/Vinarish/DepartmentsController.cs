using BlazorBoilerplate.Server.Middleware.Wrappers;
using BlazorBoilerplate.Server.Services;
using BlazorBoilerplate.Server.Services.Vinarish;
using BlazorBoilerplate.Shared.AuthorizationDefinitions;
using BlazorBoilerplate.Shared.Dto;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using VinarishApi.Models;
namespace BlazorBoilerplate.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ILogger<ToDoController> _logger;
        private readonly IVinarishService _Service;

        public DepartmentController(IVinarishService Service, ILogger<ToDoController> logger)
        {
            _logger = logger;
            _Service = Service;
        }

        // GET: api/Department
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResponse> Get()
        {
            return await _Service.GetDepartmentsAsync();
            //List<Department> apiDepartments = new List<Department>();

            //// discover endpoints from metadata
            //var client = new HttpClient();

            //var disco = await client.GetDiscoveryDocumentAsync("https://localhost:44377/");
            //if (disco.IsError)
            //{
            //    return new ApiResponse(400, disco.Error);
            //}

            //// request token
            //var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            //{
            //    Address = disco.TokenEndpoint,
            //    ClientId = "VinarishApiClient",
            //    ClientSecret = "SecretVinarishApi",

            //    Scope = "VinarishApi"
            //});

            //if (tokenResponse.IsError)
            //{
            //    return new ApiResponse(400, tokenResponse.Error);
            //}

            //// call api
            //var apiClient = new HttpClient();
            //apiClient.SetBearerToken(tokenResponse.AccessToken);

            //var response = await apiClient.GetAsync("https://localhost:44377/api/Departments");
            //if (!response.IsSuccessStatusCode)
            //{
            //    return new ApiResponse((int)response.StatusCode, response.ReasonPhrase);

            //}
            //else
            //{
            //    var content = await response.Content.ReadAsStringAsync();
            //    apiDepartments = Newtonsoft.Json.JsonConvert.DeserializeObject<Department[]>(content).ToList<Department>();

            //}
            //return new ApiResponse(200, "Retrieved Departments", apiDepartments);
        }

        //// GET: api/Department/5
        //[HttpGet("{id}")]
        //[AllowAnonymous]
        //public async Task<ApiResponse> Get(int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return new ApiResponse(400, "Todo Model is Invalid");
        //    }
        //    return await _todoService.Get(id);
        //}

        //// POST: api/Department
        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<ApiResponse> Post([FromBody] Department todo)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return new ApiResponse(400, "Todo Model is Invalid");
        //    }
        //    return await _todoService.Create(todo);
        //}

        //// Put: api/Todo
        //[HttpPut]
        //[AllowAnonymous]
        //public async Task<ApiResponse> Put([FromBody] Department todo)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return new ApiResponse(400, "Todo Model is Invalid");
        //    }
        //    return await _todoService.Update(todo);
        //}                

        //// DELETE: api/Todo/5
        //[HttpDelete("{id}")]
        //[Authorize(Policy = Policies.IsAdmin)]
        //public async Task<ApiResponse> Delete(long id)
        //{
        //    return await _todoService.Delete(id); // Delete from DB
        //}
    }
}
