using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using SightMap.BLL.Infrastructure.Interfaces;
using SightMap.DAL;

namespace SightMap.BLL.Infrastructure.Implementations
{
    public class BaseDbAccess<TFullDto, TShortDto, Filter> : IDataAccess<TFullDto, TShortDto, Filter>
    {
        private DataDbContext db;
        private ILogger<SightTypesDbAccess> logger;

        public BaseDbAccess(ILogger<SightTypesDbAccess> _logger, DataDbContext _db)
        {
            db = _db;
            logger = _logger;
        }

        public virtual TFullDto Add(TFullDto dto)
        {
            throw new System.NotImplementedException();
        }

        public virtual bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public virtual TFullDto Edit(TFullDto dto)
        {
            throw new System.NotImplementedException();
        }

        public virtual IEnumerable<TShortDto> GetListObjects(Filter filter)
        {
            throw new System.NotImplementedException();
        }

        public virtual TFullDto GetObject(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
