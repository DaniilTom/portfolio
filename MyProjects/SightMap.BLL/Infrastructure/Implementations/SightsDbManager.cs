using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Logging;
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
                                IBaseManager<SightTypeDTO, SightTypeFilterDTO> typeManager) : base(_logger, _repo, _mapper)
        {
            _typeManager = typeManager;
        }

        public override IEnumerable<SightDTO> GetListObjects(SightFilterDTO filterDto)
        {
            var result = base.GetListObjects(filterDto);

            foreach(var sight in result)
            {
                sight.Type = _typeManager.GetListObjects(new SightTypeFilterDTO { Id = sight.Type.Id }).FirstOrDefault();
            }

            return result;
        }

        protected override IFilter<Sight> ConfigureFilter(SightFilterDTO dto) => new SightFilter(dto);
    }
}
