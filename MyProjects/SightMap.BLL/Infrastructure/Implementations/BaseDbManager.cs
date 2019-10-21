using System;
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
    public abstract class BaseDbManager<TFullDto, TShortDto, TFilterDto, TSource> : IDbManager<TFullDto, TShortDto, TFilterDto>
        where TFullDto : TShortDto
        where TFilterDto : BaseFilterDTO
        where TSource : BaseEntity
    {

        protected IRepository<TSource> repo;
        private ILogger logger;
        protected IMapper mapper;

        protected BaseDbManager(ILogger _logger, IRepository<TSource> _repo, IMapper _mapper)
        {
            logger = _logger;
            repo = _repo;
            mapper = _mapper;
        }

        public TFullDto Add(TFullDto dto)
        {
            TSource temp = null;
            TFullDto fullDto;
            try
            {
                var src = mapper.Map<TSource>(dto);
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
            TSource temp;
            TFullDto fullDto;

            try
            {
                var src = mapper.Map<TSource>(dto);
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

        public IEnumerable<TShortDto> GetListObjects(TFilterDto filterDto)
        {
            IFilter<TSource> filter = ConfigureFilter(filterDto);

            IEnumerable<TSource> collection;
            IEnumerable<TShortDto> dtoCollection;

            try
            {
                collection = repo.GetList(filter.IsStatisfy, filter.Offset, filter.Size);
                dtoCollection = collection.Select(s => mapper.Map<TShortDto>(s)).AsEnumerable();

            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                dtoCollection = null;
            }

            return dtoCollection;
        }

        public TFullDto GetObject(int id)
        {
            TFullDto fullDto;

            try
            {
                fullDto = mapper.Map<TFullDto>(repo.GetById(id));
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                fullDto = default(TFullDto);
            }

            return fullDto;
        }

        protected abstract IFilter<TSource> ConfigureFilter(TFilterDto dto);
    }
}
