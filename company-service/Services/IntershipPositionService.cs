﻿using company_service.DTO;
using company_service.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace company_service.Services
{
    public class IntershipPositionService : IIntershipPositionService
    {
        private readonly ApplicationDbContext _context;

        public IntershipPositionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateIntershipPosition(IntershipPositionCreateUpdateDto model)
        {
            var companyInfo = _context.Сompanies.FirstOrDefault(c => c.CompanyId == model.CompanyId);

            if (companyInfo == null)
            {
                throw new ValidationException("This company does not exist");
            }

            var newPosition = new IntershipPosition
            {
                CompanyId = model.CompanyId,
                IntershipPositionName = model.IntershipPositionName,
                IntershipPositionDescription = model.IntershipPositionDescription,
                IntershipPositionCount = model.IntershipPositionCount
            };

            await _context.IntershipPositions.AddAsync(newPosition);
            await _context.SaveChangesAsync();

            return newPosition.IntershipPositionId;
        }

        public IntershipPositionPageDto GetIntershipPositionPageInfo(Guid id)
        {
            var positionInfo = _context.IntershipPositions.FirstOrDefault(c => c.IntershipPositionId == id);

            if (positionInfo == null)
            {
                throw new ValidationException("This intership position does not exist");
            }

            var company = _context.Сompanies.FirstOrDefault(c => c.CompanyId == positionInfo.CompanyId);

            IntershipPositionPageDto result = new IntershipPositionPageDto
            {
                IntershipPositionId = id,
                CompanyId = positionInfo.CompanyId,
                IntershipPositionName = positionInfo.IntershipPositionName,
                IntershipPositionDescription = positionInfo.IntershipPositionDescription,
                IntershipPositionCount = positionInfo.IntershipPositionCount,
                CompanyName = company.CompanyName
            };
            return result;
        }

        public async Task<IntershipPositionsDto> GetAllIntershipPositions(string token)
        {
            List<IntershipPosition> allPositions = _context.IntershipPositions.ToList();
            List<IntershipPositionInfoDto> intershipPositionInfoDtos = new List<IntershipPositionInfoDto>();
            foreach (var position in allPositions)
            {
                var company = _context.Сompanies.FirstOrDefault(c => c.CompanyId == position.CompanyId);
                var newPos = new IntershipPositionInfoDto
                {
                    IntershipPositionId = position.IntershipPositionId,
                    CompanyId = position.CompanyId,
                    IntershipPositionName = position.IntershipPositionName,
                    IntershipPositionCount = position.IntershipPositionCount,
                    CompanyName = company.CompanyName,
                    IntershipApplicationsCount = await GetCountOfApplications(token, position.IntershipPositionId)
                };
                intershipPositionInfoDtos.Add(newPos);
            }
            IntershipPositionsDto result = new IntershipPositionsDto(intershipPositionInfoDtos);
            return result;
        }

        public async Task<IntershipPositionsDto> GetAllIntershipPositionsByCompany(string token, int id)
        {
            var companyInfo = _context.Сompanies.FirstOrDefault(c => c.CompanyId == id);

            if (companyInfo == null)
            {
                throw new ValidationException("This company does not exist");
            }

            List<IntershipPosition> allPositions = _context.IntershipPositions.Where(c => c.CompanyId == id).ToList();
            List<IntershipPositionInfoDto> intershipPositionInfoDtos = new List<IntershipPositionInfoDto>();
            foreach (var position in allPositions)
            {
                var company = _context.Сompanies.FirstOrDefault(c => c.CompanyId == position.CompanyId);
                var newPos = new IntershipPositionInfoDto
                {
                    IntershipPositionId = position.IntershipPositionId,
                    CompanyId = position.CompanyId,
                    IntershipPositionName = position.IntershipPositionName,
                    IntershipPositionCount = position.IntershipPositionCount,
                    CompanyName = company.CompanyName,
                    IntershipApplicationsCount = await GetCountOfApplications(token, position.IntershipPositionId)
                };
                intershipPositionInfoDtos.Add(newPos);
            }
            IntershipPositionsDto result = new IntershipPositionsDto(intershipPositionInfoDtos);
            return result;
        }

        public async Task EditIntershipPosition(Guid id, IntershipPositionCreateUpdateDto model)
        {
            var positionInfo = _context.IntershipPositions.FirstOrDefault(c => c.IntershipPositionId == id);

            if (positionInfo == null)
            {
                throw new ValidationException("This intership position does not exist");
            }

            positionInfo.IntershipPositionName = model.IntershipPositionName;
            positionInfo.IntershipPositionDescription = model.IntershipPositionDescription;
            positionInfo.IntershipPositionCount = model.IntershipPositionCount;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteIntershipPosition(Guid id)
        {
            var positionInfo = _context.IntershipPositions.FirstOrDefault(c => c.IntershipPositionId == id);

            if (positionInfo == null)
            {
                throw new ValidationException("This intership position does not exist");
            }

            _context.IntershipPositions.Remove(positionInfo);
            await _context.SaveChangesAsync();
        }

        private async Task<int> GetCountOfApplications(string token, Guid id)
        {
            var client = new HttpClient();
            var baseUrl = "https://hits-application-service.onrender.com/api/applications/position/";
            var urlId = id.ToString();
            var url = baseUrl + urlId;
            client.DefaultRequestHeaders.Add("Authorization", token);

            List<ApplicationDto> applications = new List<ApplicationDto>();

            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var application = JsonConvert.DeserializeObject<List<ApplicationDto>>(jsonString);
                foreach (var a in application)
                {
                    applications.Add(a);
                }
            }
            else
            {
                throw new ValidationException("This info does not exist");
            }
            return applications.Count;
        }
    }
}
