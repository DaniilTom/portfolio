using SightMap.BLL.DTO;
using SightMap.DAL.Models;
using System;

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

        public override bool IsStatisfy(Sight item)
        {
            if(Id != 0)
            {
                if (Id == item.Id)
                    return true;
                else
                    return false;
            }
            
            if(!string.IsNullOrEmpty(Name))
            {
                // проверка имени
                if (Name != item.Name)
                    return false;
            }
            
            if(SightTypeId != 0)
            {
                // проверка SightTypeId
                if (SightTypeId != item.SightTypeId)
                    return false;
            }
            
            if(!(CreateUpDate is null))
            {
                // проверка CreateDate по верхнему порогу
                if (CreateUpDate < item.CreateDate)
                    return false;
            }
            
            if(!(CreateDownDate is null))
            {
                // проверка CreateDate по нижнему порогу
                if (CreateDownDate > item.CreateDate)
                    return false;
            }
            
            if (!(UpdateUpDate is null))
            {
                // проверка UpdateDate по верхнему порогу
                if (UpdateUpDate < item.UpdateDate)
                    return false;
            }
            
            if (!(UpdateDownDate is null))
            {
                // проверка UpdateDate по нижнему порогу
                if (UpdateDownDate < item.UpdateDate)
                    return false;
            }

            return true;
        }
    }
}
