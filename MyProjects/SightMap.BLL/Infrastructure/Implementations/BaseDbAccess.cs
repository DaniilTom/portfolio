﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SightMap.BLL.DTO;
using SightMap.BLL.Filters;
using SightMap.BLL.Infrastructure.Interfaces;
using SightMap.BLL.Mappers;
using SightMap.DAL;
using SightMap.DAL.Models;
using SightMap.DAL.Repositories;

namespace SightMap.BLL.Infrastructure.Implementations.Test
{
    public abstract class BaseDbAccess<TFullDto, TShortDto, Source> : IDataAccess<TFullDto, TShortDto, Source>
                                                                            where TFullDto : TShortDto
                                                                            where Source : Base
    {

        protected IRepository<Source> repo;
        private ILogger logger;

        protected BaseDbAccess(ILogger _logger, IRepository<Source> _repo)
        {
            logger = _logger;
            repo = _repo;
        }

        public TFullDto Add(TFullDto dto)
        {
            Source temp = null;
            TFullDto fullDto = default(TFullDto);
            try
            {
                temp = repo.Add(DtoToSource(dto));
                fullDto = SourceToDto(temp);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return fullDto;
        }

        public TFullDto Edit(TFullDto dto)
        {
            Source temp = null;
            TFullDto fullDto = default(TFullDto);

            try
            {
                temp = repo.Update(DtoToSource(dto));
                fullDto = SourceToDto(temp);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return fullDto;
        }

        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                result = repo.Delete(id);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return result;
        }

        public IEnumerable<TShortDto> GetListObjects(IFilter<Source> filter)
        {
            IEnumerable<Source> collection = null;

            try
            {
                collection = repo.GetList(filter.IsStatisfy);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return collection.Skip(filter.Offset)
                             .Take(filter.Size)
                             .Select(s => SourceToShortDto(s));
        }

        public TFullDto GetObject(int id)
        {
            TFullDto fullDto = default(TFullDto);

            try
            {
                fullDto = SourceToDto(repo.GetById(id));
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }
            logger.LogWarning("In GetFullObject(int id) method.");

            return fullDto;
        }

        protected abstract Source DtoToSource(TFullDto dto);
        protected abstract TFullDto SourceToDto(Source item);
        protected abstract TShortDto SourceToShortDto(Source item);

    }

    public class SightsDbAccess : BaseDbAccess<SightDTO, ShortSightDTO, Sight>
    {
        //private ILogger<SightsDbAccess> logger;

        public SightsDbAccess(ILogger<SightsDbAccess> _logger, IRepository<Sight> _repo) : base(_logger, _repo) { }

        protected override Sight DtoToSource(SightDTO dto) => dto?.ToSource();

        protected override SightDTO SourceToDto(Sight item) => item?.ToDTO();

        protected override ShortSightDTO SourceToShortDto(Sight item) => item?.ToShortDTO();
    }

    public class SightTypesDbAccess : BaseDbAccess<SightTypeDTO, SightTypeDTO, SightType>
    {
        //private ILogger<SightTypesDbAccess> logger;

        public SightTypesDbAccess(ILogger<SightsDbAccess> _logger, IRepository<SightType> _repo) : base(_logger, _repo) { }

        protected override SightType DtoToSource(SightTypeDTO dto) => dto?.ToSource();

        protected override SightTypeDTO SourceToDto(SightType item) => item?.ToDTO();

        protected override SightTypeDTO SourceToShortDto(SightType item) => item?.ToShortDTO();
    }
}
