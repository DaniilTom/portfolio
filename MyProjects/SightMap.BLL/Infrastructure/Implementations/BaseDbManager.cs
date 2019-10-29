using System;
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
    public abstract class BaseDbManager<TFullDto, TFilterDto, TModel> : IBaseManager<TFullDto, TFilterDto>
        where TFullDto : BaseDTO
        where TFilterDto : BaseFilterDTO
        where TModel : BaseEntity
    {

        protected IRepository<TModel> _repo;
        protected IMapper _mapper;
        protected ICustomCache<TFullDto> _cache;

        private ILogger _logger;

        protected BaseDbManager(ILogger logger, IRepository<TModel> repo, IMapper mapper, ICustomCache<TFullDto> cache)
        {
            _logger = logger;
            _repo = repo;
            _mapper = mapper;
            _cache = cache;
        }

        public TFullDto Add(TFullDto dto)
        {
            TModel temp = null;
            TFullDto fullDto;
            try
            {
                var src = _mapper.Map<TModel>(dto);
                temp = _repo.Add(src);
                fullDto = _mapper.Map<TFullDto>(temp);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
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

                var src = _mapper.Map<TModel>(dto);
                temp = _repo.Update(src);
                fullDto = _mapper.Map<TFullDto>(temp);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                fullDto = default(TFullDto);
            }

            return fullDto;
        }

        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                result = _repo.Delete(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }

            return result;
        }

        public virtual IEnumerable<TFullDto> GetListObjects(TFilterDto filterDto, bool IsCacheUsed = true)
        {
            IFilter<TModel> filter = ConfigureFilter(filterDto);

            IEnumerable<TFullDto> dtoCollection;

            try
            {
                if (IsCacheUsed)
                {
                    if (!_cache.TryGetCachedValue(filterDto.RequestPath, out dtoCollection))
                    {
                        IEnumerable<TModel>  collection = _repo.GetList(filter.ApplyFilter, filter.Offset, filter.Size);
                        dtoCollection = collection.Select(s => _mapper.Map<TFullDto>(s)).ToList();

                        _cache.SetValueToCache(filterDto.RequestPath, dtoCollection);
                    }
                }
                else
                {
                    IEnumerable<TModel> collection = _repo.GetList(filter.ApplyFilter, filter.Offset, filter.Size);
                    dtoCollection = collection.Select(s => _mapper.Map<TFullDto>(s)).ToList();
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                dtoCollection = null;
            }

            return dtoCollection;
        }

        protected abstract IFilter<TModel> ConfigureFilter(TFilterDto dto);
    }
}
