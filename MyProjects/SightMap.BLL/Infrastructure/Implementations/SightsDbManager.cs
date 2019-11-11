using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using SightMap.BLL.Constants;
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
        private IBaseManager<AlbumDTO, AlbumFilterDTO> _albumManager;

        public SightsDbManager(ILogger<SightsDbManager> _logger,
                               IRepository<Sight> _repo,
                               IMapper _mapper,
                               ICustomCache _cache,
                               IBaseManager<SightTypeDTO, SightTypeFilterDTO> typeManager,
                               IBaseManager<AlbumDTO, AlbumFilterDTO> albumManager) : base(_logger, _repo, _mapper, _cache)
        {
            _typeManager = typeManager;
            _albumManager = albumManager;
        }

        public override SightDTO Add(SightDTO dto)
        {
            dto.CreateDate = DateTime.Now;
            SightDTO temp = base.Add(dto);

            if (temp != null)
                temp.Type = _typeManager.GetListObjects(new SightTypeFilterDTO { Id = dto.Type.Id }).FirstOrDefault();

            return temp;
        }

        public override SightDTO Edit(SightDTO dto)
        {
            dto.UpdateDate = DateTime.Now;
            SightDTO temp = base.Edit(dto);

            if(temp != null)            
                temp.Type = _typeManager.GetListObjects(new SightTypeFilterDTO { Id = dto.Type.Id }).FirstOrDefault();

            return base.Edit(dto);
        }

        public override IEnumerable<SightDTO> GetListObjects(SightFilterDTO filterDto, bool IsCacheUsed = true)
        {
            IEnumerable<SightDTO> result;

            if (IsCacheUsed)
            {
                result = base.GetListObjects(filterDto);
                foreach (var sight in result)
                {
                    sight.Type = _typeManager.GetListObjects(new SightTypeFilterDTO { Id = sight.Type.Id }).FirstOrDefault();
                    sight.Album = _albumManager.GetListObjects(new AlbumFilterDTO { ItemId = sight.Id }).ToList();
                }
            }
            else
            {
                result = base.GetListObjects(filterDto, false);
                foreach (var sight in result)
                {
                    sight.Type = _typeManager.GetListObjects(new SightTypeFilterDTO { Id = sight.Type.Id }, false).FirstOrDefault();
                    sight.Album = _albumManager.GetListObjects(new AlbumFilterDTO { ItemId = sight.Id }, false).ToList();
                }
            }

            return result;
        }

        protected override IFilter<Sight> ConfigureFilter(SightFilterDTO dto) => new SightFilter(dto);
    }
}
