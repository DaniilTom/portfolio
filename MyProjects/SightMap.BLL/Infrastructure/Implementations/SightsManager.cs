using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using SightMap.BLL.Constants;
using SightMap.BLL.CustomCache;
using SightMap.BLL.DTO;
using SightMap.BLL.Filters;
using SightMap.BLL.Infrastructure.Interfaces;
using SightMap.BLL.PluploadManager;
using SightMap.DAL.Models;
using SightMap.DAL.Repositories;

namespace SightMap.BLL.Infrastructure.Implementations
{
    public class SightsManager : BaseManager<SightDTO, SightFilterDTO, Sight>
    {
        private IBaseManager<SightTypeDTO, SightTypeFilterDTO> _typeManager;
        private IAlbumEditor<AlbumDTO, AlbumFilterDTO> _albumManager;

        public SightsManager(ILogger<SightsManager> _logger,
                               IRepository<Sight> _repo,
                               IMapper _mapper,
                               ICustomCache _cache,
                               IBaseManager<SightTypeDTO, SightTypeFilterDTO> typeManager,
                               IAlbumEditor<AlbumDTO, AlbumFilterDTO> albumManager) : base(_logger, _repo, _mapper, _cache)
        {
            _typeManager = typeManager;
            _albumManager = albumManager;
        }

        public override SightDTO Add(SightDTO dto)
        {
            dto.CreateDate = DateTime.Now;
            SightDTO tempSight = base.Add(dto);

            if (tempSight != null)
            {
                dto.Album.AsParallel().ForAll(a => a.ItemId = tempSight.Id);
                _albumManager.Edit(dto.Album, dto.RefId);
            }
            

            tempSight = GetListObjects(new SightFilterDTO { Id = tempSight.Id }, false).FirstOrDefault();

            return tempSight;
        }

        public override SightDTO Edit(SightDTO dto)
        {
            dto.UpdateDate = DateTime.Now;
            SightDTO temp = base.Edit(dto);

            if (temp != null)
                temp.Type = _typeManager.GetListObjects(new SightTypeFilterDTO { Id = dto.Type.Id }).FirstOrDefault();

            return base.Edit(dto);
        }

        public override IEnumerable<SightDTO> GetListObjects(SightFilterDTO filterDto, bool isCacheUsed = true)
        {
            IEnumerable<SightDTO> result = base.GetListObjects(filterDto, isCacheUsed);
            foreach (var sight in result)
            {
                sight.Type = _typeManager.GetListObjects(new SightTypeFilterDTO { Id = sight.Type.Id }, isCacheUsed).FirstOrDefault();
                sight.Album = (_albumManager as IBaseManager<AlbumDTO, AlbumFilterDTO>)
                    .GetListObjects(new AlbumFilterDTO { ItemId = sight.Id }, isCacheUsed).ToList();
            }

            return result;
        }

        protected override IFilter<Sight> ConfigureFilter(SightFilterDTO dto) => new SightFilter(dto);
    }
}
