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

        public override IQueryable<Sight> ApplyFilter(IQueryable<Sight> set)
        {
            if (Id != 0)
            {
                set = set.Where(s => s.Id == Id);
                return set;
            }

            if (!string.IsNullOrEmpty(Name))
            {
                // проверка имени
                set = set.Where(s => EF.Functions.Like(s.Name, "%" + Name + "%"));
            }

            if (SightTypeId != 0)
            {
                // проверка SightTypeId
                set = set.Where(s => s.SightTypeId == SightTypeId);
            }

            if (!(CreateUpDate is null))
            {
                // проверка CreateDate по верхнему порогу
                set = set.Where(s => s.CreateDate <= CreateUpDate);
            }

            if (!(CreateDownDate is null))
            {
                // проверка CreateDate по нижнему порогу
                set = set.Where(s => s.CreateDate >= CreateDownDate);
            }

            if (!(UpdateUpDate is null))
            {
                // проверка UpdateDate по верхнему порогу
                set = set.Where(s => s.UpdateDate <= UpdateUpDate);
            }

            if (!(UpdateDownDate is null))
            {
                // проверка UpdateDate по нижнему порогу
                set = set.Where(s => s.UpdateDate >= UpdateDownDate);
            }

            #region Монадическое выражение
            //set.ContainsInName(Name)
            //       .IsType(SightTypeId)
            //       .CreatedBeforeInclude(CreateUpDate)
            //       .UpdatedAfter(UpdateDownDate);
            #endregion

            return set;
        }
    }

    public static class Monads
    {
        public static IQueryable<Sight> ContainsInName(this IQueryable<Sight> set, string str)
        {
            if (string.IsNullOrEmpty(str))
                return set;

            return set.Where(t => EF.Functions.Like(t.Name, "%" + str + "%"));
        }

        public static IQueryable<Sight> IsType(this IQueryable<Sight> set, int id)
        {
            if (id == default(int))
                return set;

            return set.Where(s => s.SightTypeId == id);
        }

        public static IQueryable<Sight> CreatedBeforeInclude(this IQueryable<Sight> set, DateTime? date)
        {
            if (date is null)
                return set;

            return set.Where(s => s.CreateDate <= date);
        }

        public static IQueryable<Sight> UpdatedAfter(this IQueryable<Sight> set, DateTime? date)
        {
            if (date is null)
                return set;

            return set.Where(s => s.UpdateDate > date);
        }
    }
}
