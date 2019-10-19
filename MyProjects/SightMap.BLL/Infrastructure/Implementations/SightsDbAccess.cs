using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SightMap.BLL.DTO;
using SightMap.BLL.Filters;
using SightMap.BLL.Infrastructure.Interfaces;
using SightMap.BLL.Mappers;
using SightMap.DAL;
using SightMap.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SightMap.BLL.Infrastructure.Implementations
{
    public class SightsDbAccess : IDataAccess<SightDTO, ShortSightDTO, SightFilter>
    {
        private DataDbContext db;
        private ILogger<SightsDbAccess> logger;

        public SightsDbAccess(ILogger<SightsDbAccess> _logger, DataDbContext _db)
        {
            db = _db;
            logger = _logger;
        }

        public SightDTO Add(SightDTO dto)
        {
            Sight temp = null;
            try
            {
               temp = dto.ToSight();
               temp.Type = db.SightTypes.FirstOrDefault(t => t.Name == dto.TypeName);

                db.Sights.Add(temp);
                db.SaveChanges();
            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
            }

            return temp.ToSightDTO();
        }

        public SightDTO Edit(SightDTO dto)
        {
            Sight temp = null;

            try
            {
                // в случае возникновения исключения, temp не будет затронут
                Sight proxyTemp = db.Sights.First(s => s.Id == dto.Id);

                proxyTemp.Name = dto.Name;
                proxyTemp.ShortDescription = dto.ShortDescription;
                proxyTemp.FullDescription = dto.FullDescription;
                proxyTemp.PhotoPath = dto.PhotoPath;
                proxyTemp.Type = db.SightTypes.First(t => t.Name == dto.TypeName); ;

                db.SaveChanges();

                temp = proxyTemp;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return temp.ToSightDTO();
        }

        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                db.Sights.Remove( db.Sights.First(s => s.Id == id) );
                db.SaveChanges();

                result = true;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return result;
        }

        public IEnumerable<ShortSightDTO> GetListObjects(SightFilter filter)
        {
            IEnumerable<Sight> sightCollection = db.Sights;

            try
            {
                if (!(filter.Name is null) || !(filter.Type is null))
                {
                    Regex nameR = new Regex($@"\B{filter.Name ?? ""}\B");
                    Regex typeR = new Regex($@"\B{filter.Type ?? ""}\B");
                    sightCollection = sightCollection.Where(s => nameR.IsMatch(s.Name) && typeR.IsMatch(s.Type.Name));
                }
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return sightCollection.Skip(filter.Offset)
                                  .Take(filter.Size)
                                  .Select(s => s.ToShortSightDTO());
        }

        public SightDTO GetObject(int id)
        {
            SightDTO sightDto = null;

            try
            {
                sightDto = db.Sights.Include(s => s.Type).FirstOrDefault(s => s.Id == id)?.ToSightDTO();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }
            logger.LogWarning("In GetFullObject(int id) method.");
            return sightDto;
        }
    }

    class ConcreteRealization
    {
        public override Add()
        {
            /*...Common for all...*/
            SomeMeth();
            /*...Continue common for all...*/
        }

        private SomeType SomeMeth()
        {

        }
    }
}
