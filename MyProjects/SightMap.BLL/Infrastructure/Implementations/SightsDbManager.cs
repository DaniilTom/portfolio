using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using SightMap.BLL.CustomCache;
using SightMap.BLL.DTO;
using SightMap.BLL.Filters;
using SightMap.BLL.Infrastructure.Interfaces;
using SightMap.DAL.Models;
using SightMap.DAL.Repositories;

namespace SightMap.BLL.Infrastructure.Implementations
{
    public class SightsDbManager : BaseDbManager<SightDTO, SightFilterDTO, Sight>
    {
        private IBaseManager<SightTypeDTO, SightTypeFilterDTO> _typeManager;

        public SightsDbManager(ILogger<SightsDbManager> _logger,
                               IRepository<Sight> _repo,
                               IMapper _mapper,
                               ICustomCache<SightDTO> _cache,
                               IBaseManager<SightTypeDTO, SightTypeFilterDTO> typeManager) : base(_logger, _repo, _mapper, _cache)
        {
            _typeManager = typeManager;
        }

        public override IEnumerable<SightDTO> GetListObjects(SightFilterDTO filterDto, bool IsCacheUsed = true)
        {
            IEnumerable<SightDTO> result;

            if (IsCacheUsed)
            {
                if (!_cache.TryGetCachedValue(filterDto.RequestPath, out result))
                {
                    result = base.GetListObjects(filterDto, false);
                    foreach (var sight in result)
                    {
                        sight.Type = _typeManager.GetListObjects(new SightTypeFilterDTO { Id = sight.Type.Id }, false).FirstOrDefault();
                    }

                    _cache.SetValueToCache(filterDto.RequestPath, result);
                }
            }
            else
            {
                result = base.GetListObjects(filterDto, false);
                foreach (var sight in result)
                {
                    sight.Type = _typeManager.GetListObjects(new SightTypeFilterDTO { Id = sight.Type.Id }, false).FirstOrDefault();
                }
            }

            return result;
        }

        protected override IFilter<Sight> ConfigureFilter(SightFilterDTO dto) => new SightFilter(dto);
    }
}
