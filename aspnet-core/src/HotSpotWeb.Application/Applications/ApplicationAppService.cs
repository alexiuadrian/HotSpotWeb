using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.UI;
using AutoMapper.Internal.Mappers;
using HotSpotWeb.Applications.Dtos;
using HotSpotWeb.Configurations;

namespace HotSpotWeb.Applications;

[AbpAuthorize]
public class ApplicationAppService : HotSpotWebAppServiceBase, IApplicationAppService
{
    private readonly IRepository<Application, int> _applicationRepository;
    private readonly IApplicationManager _applicationManager;
    private readonly IConfigurationManager _configurationManager;
    
    public ApplicationAppService(IRepository<Application, int> applicationRepository, IApplicationManager applicationManager,
        IConfigurationManager configurationManager)
    {
        _applicationRepository = applicationRepository;
        _applicationManager = applicationManager;
        _configurationManager = configurationManager;
    }

    public async Task<List<ApplicationDto>> GetListAsync(GetApplicationListInput input)
    {
        var applications = _applicationRepository
            .GetAll()
            .WhereIf(input.Filter != null, a => a.Name.Contains(input.Filter))
            .ToList();

        if (input.Sorting == "ASC")
        {
            applications = applications.OrderBy(a => a.Name).ToList();
        } else if (input.Sorting == "DESC")
        {
            applications = applications.OrderByDescending(a => a.Name).ToList();
        }

        if (input.IncludeUnpublished == false)
        {
            applications = applications.Where(a => a.Status != "Unpublished").ToList();
        }
        
        if (input.IncludeDeleted == false)
        {
            applications = applications.Where(a => a.Status != "Deleted").ToList();
        }

        if (input.MaxResultCount > 100)
        {
            throw new UserFriendlyException("Max result count cannot be greater than 100");
        }
        
        if (input.MaxResultCount != 0)
        {
            applications = applications.Take(input.MaxResultCount).ToList();
        }
        
        var result = ObjectMapper.Map<List<ApplicationDto>>(applications);

        return result;
    }

    public async Task<ApplicationDetailsDto> GetDetailsAsync(EntityDto<int> input)
    {
        var application = await _applicationManager.GetAsync(input.Id);

        if (application == null)
        {
            throw new UserFriendlyException("Application not found");
        }
        
        var result = ObjectMapper.Map<ApplicationDetailsDto>(application);

        return result;
    }

    public async Task CreateAsync(CreateApplicationInput input)
    {
        var configuration = await _configurationManager.GetAsync(input.Configuration.Id);
        var application = Application.Create(input.Name, input.Description, input.Status, input.Version, input.Type,
            input.Url, input.Icon, input.Color, input.VersionControl, input.RepositoryUrl, input.RepositoryUsername,
            input.RepositoryBranch, input.Technology, configuration, AbpSession.GetUserId());
        await _applicationManager.CreateAsync(application);
    }

    public Task<bool> RunApplicationAsync(int id)
    {
        return _applicationManager.StartApplication(id);
    }

    public Task DeleteAsync(int id)
    {
        return _applicationManager.DeleteAsync(id);
    }
}