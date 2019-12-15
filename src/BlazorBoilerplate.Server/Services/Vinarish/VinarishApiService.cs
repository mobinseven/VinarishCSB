using AutoMapper;
using BlazorBoilerplate.Server.Middleware.Wrappers;
using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using VinarishApi.Models;
namespace BlazorBoilerplate.Server.Services.Vinarish
{
    public interface IVinarishService
    {
        Task<ApiResponse> GetDepartmentsAsync();
        //Task<ApiResponse> GetDepartment(Guid id);
        //Task<ApiResponse> Create(TodoDto todo);
        //Task<ApiResponse> Update(TodoDto todo);
        //Task<ApiResponse> Delete(long id);
    }
    public class VinarishService : IVinarishService
    {
        string apiResponses;

        List<Department> apiDepartments = new List<Department>();
        public async Task<ApiResponse> GetDepartmentsAsync()
        {
            // discover endpoints from metadata
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:44377/");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return new ApiResponse(400, disco.Error);
            }

            // request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "VinarishApiClient",
                ClientSecret = "SecretVinarishApi",

                Scope = "VinarishApi"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return new ApiResponse(400, tokenResponse.Error);
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");

            // call api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync("https://localhost:44377/api/Departments");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
                return new ApiResponse((int)response.StatusCode, response.ReasonPhrase);

            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                apiResponses = content;
                apiDepartments = Newtonsoft.Json.JsonConvert.DeserializeObject<Department[]>(apiResponses).ToList<Department>();

            }
            return new ApiResponse(200, "Retrieved Departments", apiDepartments);
        }
    }
}
