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

namespace HotSpotWeb.Applications;

[AbpAuthorize]
public class ApplicationAppService : HotSpotWebAppServiceBase, IApplicationAppService
{
    private readonly IRepository<Application, int> _applicationRepository;
    private readonly IApplicationManager _applicationManager;
    
    public ApplicationAppService(IRepository<Application, int> applicationRepository, IApplicationManager applicationManager)
    {
        _applicationRepository = applicationRepository;
        _applicationManager = applicationManager;
    }

    public async Task<ListResultDto<ApplicationListDto>> GetListAsync(GetApplicationListInput input)
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
        
        var result = ObjectMapper.Map<List<ApplicationListDto>>(applications);
        
        return new ListResultDto<ApplicationListDto>(result);
    }

    public Task<ApplicationListDto> GetDetailsAsync(EntityDto<int> input)
    {
        var application = _applicationRepository.FirstOrDefault(a => a.Id == input.Id);

        if (application == null)
        {
            throw new UserFriendlyException("Application not found");
        }
        
        var result = ObjectMapper.Map<ApplicationListDto>(application);
        
        return Task.FromResult(result);
    }

    public async Task CreateAsync(CreateApplicationInput input)
    {
        var application = Application.Create(input.Name, input.Description, input.Status, input.Version, input.Type,
            input.Url, input.Icon, input.Color, input.VersionControl, input.RepositoryUrl, input.RepositoryUsername,
            input.RepositoryBranch, input.Technology, AbpSession.GetUserId());
        await _applicationManager.CreateAsync(application);
    }
}