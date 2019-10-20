//using Microsoft.Extensions.Logging;
//using SightMap.BLL.DTO;
//using SightMap.BLL.Filters;
//using SightMap.BLL.Infrastructure.Interfaces;
//using SightMap.BLL.Mappers;
//using SightMap.DAL;
//using SightMap.DAL.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text.RegularExpressions;

//namespace SightMap.BLL.Infrastructure.Implementations
//{
//    public class SightTypesDbAccess : IDataAccess<SightTypeDTO, SightTypeDTO, SightType>
//    {
//        private DataDbContext db;
//        private ILogger<SightTypesDbAccess> logger;

//        public SightTypesDbAccess(ILogger<SightTypesDbAccess> _logger, DataDbContext _db)
//        {
//            db = _db;
//            logger = _logger;
//        }

//        public SightTypeDTO Add(SightTypeDTO dto)
//        {
//            SightType temp = null;
//            try
//            {
//                temp = dto.ToSource();

//                db.SightTypes.Add(temp);
//                db.SaveChanges();
//            }
//            catch (Exception e)
//            {
//                logger.LogError(e.Message);
//            }

//            return temp.ToDTO();
//        }

//        public SightTypeDTO Edit(SightTypeDTO dto)
//        {
//            SightType temp = null;

//            try
//            {
//                // в случае возникновения исключения, temp не будет затронут
//                SightType proxyTemp = db.SightTypes.FirstOrDefault(s => s.Id == dto.Id);

//                proxyTemp.Name = dto.Name;

//                db.SaveChanges();

//                temp = proxyTemp;
//            }
//            catch (Exception e)
//            {
//                logger.LogError(e.Message);
//            }

//            return temp.ToDTO();
//        }

//        public bool Delete(int id)
//        {
//            bool result = false;
//            try
//            {
//                db.SightTypes.Remove(db.SightTypes.First(s => s.Id == id));
//                db.SaveChanges();

//                result = true;
//            }
//            catch (Exception e)
//            {
//                logger.LogError(e.Message);
//            }

//            return result;
//        }

//        public IEnumerable<SightTypeDTO> GetListObjects(IFilter<SightType> filter)
//        {
//            IEnumerable<SightType> sightCollection = db.SightTypes;

//            try
//            {
//                //if (!string.IsNullOrWhiteSpace(filter.Name))
//                {
//                    //Regex nameR = new Regex($@"\B{filter.Name ?? ""}\B");
//                    sightCollection = sightCollection.Where(s => filter.IsStatisfy(s));
//                }
//            }

//            catch (Exception e)
//            {
//                logger.LogError(e.Message);
//            }

//            return sightCollection.Skip(filter.Offset)
//                                  .Take(filter.Size)
//                                  .Select(s => s.ToShortDTO());
//        }

//        public SightTypeDTO GetObject(int id)
//        {
//            SightTypeDTO sightDto = null;

//            try
//            {
//                sightDto = db.SightTypes.FirstOrDefault(s => s.Id == id)?.ToDTO();
//            }
//            catch (Exception e)
//            {
//                logger.LogError(e.Message);
//                throw e;
//            }
//            return sightDto;
//        }
//    }
//}
