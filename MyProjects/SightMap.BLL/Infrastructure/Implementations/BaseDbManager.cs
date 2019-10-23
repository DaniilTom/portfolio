﻿using System;
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
    public abstract class BaseDbManager<TFullDto, TFilterDto, TModel> : IBaseManager<TFullDto, TFilterDto>
        where TFullDto : BaseDTO
        where TFilterDto : BaseFilterDTO
        where TModel : BaseEntity
    {

        protected IRepository<TModel> repo;
        private ILogger logger;
        protected IMapper mapper;

        protected BaseDbManager(ILogger _logger, IRepository<TModel> _repo, IMapper _mapper)
        {
            logger = _logger;
            repo = _repo;
            mapper = _mapper;
        }

        public TFullDto Add(TFullDto dto)
        {
            TModel temp = null;
            TFullDto fullDto;
            try
            {
                var src = mapper.Map<TModel>(dto);
                temp = repo.Add(src);
                fullDto = mapper.Map<TFullDto>(temp);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                fullDto = default(TFullDto);
            }

            return fullDto;
        }

        public TFullDto Edit(TFullDto dto)
        {
            TModel temp;
            TFullDto fullDto;

            try
            {
                if (dto.Id <= 0)
                    throw new ArgumentOutOfRangeException(Constants.ErrorIdWrong);

                var src = mapper.Map<TModel>(dto);
                temp = repo.Update(src);
                fullDto = mapper.Map<TFullDto>(temp);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                fullDto = default(TFullDto);
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
                logger.LogError(e, e.Message);
            }

            return result;
        }

        public virtual IEnumerable<TFullDto> GetListObjects(TFilterDto filterDto)
        {
            IFilter<TModel> filter = ConfigureFilter(filterDto);

            IEnumerable<TModel> collection;
            IEnumerable<TFullDto> dtoCollection;

            try
            {
                collection = repo.GetList(filter.IsStatisfy, filter.Offset, filter.Size);
                dtoCollection = collection.Select(s => mapper.Map<TFullDto>(s)).ToList();

            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                dtoCollection = null;
            }

            return dtoCollection;
        }

        protected abstract IFilter<TModel> ConfigureFilter(TFilterDto dto);
    }
}
