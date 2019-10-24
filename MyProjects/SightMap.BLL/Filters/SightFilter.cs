using SightMap.BLL.DTO;
using SightMap.DAL.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace SightMap.BLL.Filters
{
    public class SightFilter : BaseFilter<Sight>
    {
        public string Name { get; }
        public int SightTypeId { get; }

        public DateTime? CreateUpDate { get; }
        public DateTime? CreateDownDate { get; }
        public DateTime? UpdateUpDate { get; }
        public DateTime? UpdateDownDate { get; }

        public SightFilter(SightFilterDTO filterDto) : base(filterDto)
        {
            Name = filterDto.Name;
            SightTypeId = filterDto.SightTypeId;
            CreateUpDate = filterDto.CreateUpDate;
            CreateDownDate = filterDto.CreateDownDate;
            UpdateUpDate = filterDto.UpdateUpDate;
            UpdateDownDate = filterDto.UpdateDownDate;
        }

        #region Почти работает
        public override Expression<Func<Sight, bool>> GetExpression()
        {
            //var collection = Enumerable.Empty<Sight>().AsQueryable();
            
            Expression<Func<Sight, bool>> resultExp = base.GetExpression();

            if (Id != 0)
            {
                //collection = collection
                //    .Where(s => s.Id == Id)
                //    .AsQueryable();
                Expression<Func<Sight, bool>> idExp = s => s.Id == Id;
                return idExp;
            }

            if (!string.IsNullOrEmpty(Name))
            {
                // collection = collection.Where(s => EF.Functions.Like(s.Name, "%" + Name + "%"););
                // проверка имени
                Expression<Func<Sight, bool>> nameExp = s => EF.Functions.Like(s.Name, "%" + Name + "%");
                resultExp = AndExp(resultExp, nameExp);
            }

            if (SightTypeId != 0)
            {
                // проверка SightTypeId
                Expression<Func<Sight, bool>> sightTypeIdExp = s => s.SightTypeId == SightTypeId;
                resultExp = AndExp(resultExp, sightTypeIdExp);
            }

            if (!(CreateUpDate is null))
            {
                // проверка CreateDate по верхнему порогу
                Expression<Func<Sight, bool>> createUpDateExp = s => s.CreateDate <= CreateUpDate;
                resultExp = AndExp(resultExp, createUpDateExp);
            }

            if (!(CreateDownDate is null))
            {
                // проверка CreateDate по нижнему порогу
                Expression<Func<Sight, bool>> createDownDateExp = s => s.CreateDate >= CreateDownDate;
                resultExp = AndExp(resultExp, createDownDateExp);
            }

            if (!(UpdateUpDate is null))
            {
                // проверка UpdateDate по верхнему порогу
                Expression<Func<Sight, bool>> updateUpDateExp = s => s.UpdateDate <= UpdateUpDate;
                resultExp = AndExp(resultExp, updateUpDateExp);
            }

            if (!(UpdateDownDate is null))
            {
                // проверка UpdateDate по нижнему порогу
                Expression<Func<Sight, bool>> updateDownDateExp = s => s.UpdateDate >= UpdateDownDate;
                resultExp = AndExp(resultExp, updateDownDateExp);
            }
            return resultExp;
        }
        #endregion
    }
}
