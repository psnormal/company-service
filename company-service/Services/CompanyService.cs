﻿using company_service.DTO;
using company_service.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;

namespace company_service.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ApplicationDbContext _context;

        public CompanyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateCompany(CompanyCreateUpdateDto model)
        {
            var newCompany = new Company
            {
                CompanyName = model.CompanyName,
                CompanyDescription = model.CompanyDescription,
                CompanyAddress = model.CompanyAddress,
                CompanyContacts = model.CompanyContacts
            };

            await _context.Сompanies.AddAsync(newCompany);
            await _context.SaveChangesAsync();

            return newCompany.CompanyId;
        }

        public CompanyCreateUpdateDto GetCompanyInfo(int id)
        {
            var companyInfo = _context.Сompanies.FirstOrDefault(c => c.CompanyId == id);

            if (companyInfo == null)
            {
                throw new ValidationException("This company does not exist");
            }

            CompanyCreateUpdateDto result = new CompanyCreateUpdateDto
            {
                CompanyName = companyInfo.CompanyName,
                CompanyDescription = companyInfo.CompanyDescription,
                CompanyAddress = companyInfo.CompanyAddress,
                CompanyContacts = companyInfo.CompanyContacts
            };
            return result;
        }

        public CompanyNameDto GetCompanyName(int id)
        {
            var companyInfo = _context.Сompanies.FirstOrDefault(c => c.CompanyId == id);

            if (companyInfo == null)
            {
                throw new ValidationException("This company does not exist");
            }

            CompanyNameDto result = new CompanyNameDto
            {
                CompanyName = companyInfo.CompanyName
            };
            return result;
        }

        public async Task<ApplicationsDto> GetCompanyAllApplications(string token, int id)
        {
            var companyInfo = _context.Сompanies.FirstOrDefault(c => c.CompanyId == id);

            if (companyInfo == null)
            {
                throw new ValidationException("This company does not exist");
            }

            var intershipPositions = _context.IntershipPositions.Where(p => p.CompanyId == id).ToList();
            
            var client = new HttpClient();
            var baseUrl = "https://hits-application-service.onrender.com/api/applications/position/";
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
            client.DefaultRequestHeaders.Add("Authorization", token);

            //ApplicationsDto result = new ApplicationsDto();
            List<ApplicationDto> applications = new List<ApplicationDto>();

            foreach (var position in intershipPositions)
            {
                var urlId = position.IntershipPositionId.ToString();
                var url = baseUrl + urlId;
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    //ApplicationsDto application = await response.Content.ReadFromJsonAsync<ApplicationsDto>();
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var application = JsonConvert.DeserializeObject<List<ApplicationDto>>(jsonString);
                    foreach (var a in application)
                    {
                        applications.Add(a);
                    }
                }
                else
                {
                    //Console.WriteLine(response.StatusCode);
                    throw new ValidationException("This info does not exist");
                }
            }

            ApplicationsDto result = new ApplicationsDto(applications);
            return result;
        }

        public AllCompaniesDto GetAllCompanies()
        {
            List<Company> companies = _context.Сompanies.ToList();
            List<CompanyInfoDto> companyInfoDtos = new List<CompanyInfoDto>();
            foreach(var company in companies)
            {
                CompanyInfoDto newCompany = new CompanyInfoDto
                {
                    CompanyName = company.CompanyName,
                    CompanyId = company.CompanyId
                };
                companyInfoDtos.Add(newCompany);
            }

            AllCompaniesDto allCompanies = new AllCompaniesDto(companyInfoDtos);
            return allCompanies;
        }

        public async Task EditCompany(int id, CompanyCreateUpdateDto model)
        {
            var companyInfo = _context.Сompanies.FirstOrDefault(c => c.CompanyId == id);

            if (companyInfo == null)
            {
                throw new ValidationException("This company does not exist");
            }

            companyInfo.CompanyName = model.CompanyName;
            companyInfo.CompanyDescription = model.CompanyDescription;
            companyInfo.CompanyAddress = model.CompanyAddress;
            companyInfo.CompanyContacts = model.CompanyContacts;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteCompany(int id)
        {
            var companyInfo = _context.Сompanies.FirstOrDefault(c => c.CompanyId == id);

            if (companyInfo == null)
            {
                throw new ValidationException("This company does not exist");
            }

            _context.Сompanies.Remove(companyInfo);
            await _context.SaveChangesAsync();
        }
    }
}
