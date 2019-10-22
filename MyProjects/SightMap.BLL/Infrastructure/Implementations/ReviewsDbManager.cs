using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using SightMap.BLL.DTO;
using SightMap.BLL.Filters;
using SightMap.DAL.Models;
using SightMap.DAL.Repositories;

namespace SightMap.BLL.Infrastructure.Implementations
{
    public class ReviewsDbManager : BaseDbManager<ReviewDTO, ReviewFilterDTO, Review>
    {
        public ReviewsDbManager(ILogger<ReviewsDbManager> _logger, IRepository<Review> _repo, IMapper _mapper) : base(_logger, _repo, _mapper) { }

        public override IEnumerable<ReviewDTO> GetListObjects(ReviewFilterDTO filterDto)
        {
            var temp = base.GetListObjects(filterDto).ToList();
            var result = new List<ReviewDTO>();

            foreach (var r in temp)
            {
                if (r.ParentId == 0)
                {
                    result.Add(r);
                    temp.Remove(r);
                }
            }

            Fill(result, temp);


            return result;
        }

        protected override IFilter<Review> ConfigureFilter(ReviewFilterDTO dto) => new ReviewFilter(dto);

        public void Fill(List<ReviewDTO> review, List<ReviewDTO> source)
        {
            if (source.Count == 0)
                return;

            if()

            foreach (var r in review)
            {
                foreach (var src in source)
                {
                    if (src.ParentId == r.Id)
                    {
                        r.Children.Add(src);
                        source.Remove(src);
                    }
                }

                Fill(r.Children, source);
            }
        }
    }
}
