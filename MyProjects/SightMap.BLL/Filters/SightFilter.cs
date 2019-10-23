using SightMap.BLL.DTO;
using SightMap.DAL.Models;
using System;

namespace SightMap.BLL.Filters
{
    public class SightFilter : BaseFilter<Sight>
    {
        public string Name { get; set; }

        public int SightTypeId { get => ((SightFilterDTO)filterData).SightTypeId; }

        public DateTime? UpDate { get => ((SightFilterDTO)filterData).UpDate; }

        public DateTime? DownDate { get => ((SightFilterDTO)filterData).DownDate; }

        public SightFilter(SightFilterDTO dto) : base(dto)
        {
            Name = string.Empty;
        }

        public override bool IsStatisfy(Sight item)
        {
            return (item.SightTypeId == SightTypeId) 
                //|| CreateDateComparing(item)
                //|| UpdateDateComparing(item)
                || base.IsStatisfy(item);
        }

        //private bool CreateDateComparing(Sight item)
        //{
        //    var resultUp = DateTime.Compare(item.CreateDate, UpDate);
        //    var resultDown = DateTime.Compare(item.CreateDate, DownDate);

        //    if ((resultUp <= 0) && (resultDown >= 0))
        //        return true;

        //    return false;
        //}

        //private bool UpdateDateComparing(Sight item)
        //{
        //    var resultUp = DateTime.Compare(item.UpdateDate, UpDate);
        //    var resultDown = DateTime.Compare(item.UpdateDate, DownDate);

        //    if ((resultUp <= 0) && (resultDown >= 0))
        //        return true;

        //    return false;
        //}
    }
}
