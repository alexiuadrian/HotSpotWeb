using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using AutoMapper.Internal.Mappers;
using HotSpotWeb.Applications.Dtos;

namespace HotSpotWeb.Applications;

public class ApplicationAppService : IApplicationAppService
{
    private readonly IRepository<Application, int> _applicationRepository;
    
    public ApplicationAppService(IRepository<Application, int> applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }

    public async Task<ListResultDto<ApplicationListDto>> GetListAsync(GetApplicationListInput input)
    {
        var applications = _applicationRepository
            .GetAll()
            .WhereIf(input.Filter != null, a => a.Name.Contains(input.Filter))
            .OrderBy(a => a.Name)
            .ToList();
        
        return new ListResultDto<ApplicationListDto>(applications.MapTo<List<ApplicationListDto>>());
    }
}