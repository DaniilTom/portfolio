using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using SightMap.BLL.CustomCache;
using SightMap.BLL.DTO;
using SightMap.BLL.Filters;
using SightMap.DAL.Models;
using SightMap.DAL.Repositories;

namespace SightMap.BLL.Infrastructure.Implementations
{
    public class ReviewsDbManager : BaseDbManager<ReviewDTO, ReviewFilterDTO, Review>
    {
        public ReviewsDbManager(ILogger<ReviewsDbManager> _logger,
                                IRepository<Review> _repo,
                                IMapper _mapper,
                                ICustomCache<ReviewDTO> _cache) : base(_logger, _repo, _mapper, _cache) { }

        public override IEnumerable<ReviewDTO> GetListObjects(ReviewFilterDTO filterDto, bool IsCacheUsed = true)
        {
            var source = base.GetListObjects(filterDto).ToList();
            var result = new List<ReviewDTO>();

            Fill(result, source);

            return result;
        }

        protected override IFilter<Review> ConfigureFilter(ReviewFilterDTO dto) => new ReviewFilter(dto);

        private void Fill(List<ReviewDTO> result, List<ReviewDTO> source)
        {
            if (source.Count == 0) // условие выхода
                return;

            if (result.Count == 0) // инициализация
            {
                var minParentId = source.Min(s => s.ParentId);
                for (int i = 0; i < source.Count; i++)
                {
                    var tempItem = source[i];
                    if (tempItem.ParentId == minParentId)
                    {
                        result.Add(tempItem);
                        source.Remove(tempItem);
                        i--; // из-за смещения элементов коллекции
                    }
                }
            }

            for (int i = 0; i < result.Count; i++) // для каждого родителя
            {
                var parent = result[i];

                for (int l = 0; l < source.Count; l++)
                {
                    var tempItem = source[l];
                    if (tempItem.ParentId == parent.Id)
                    {
                        parent.Children.Add(tempItem);
                        source.Remove(tempItem);
                        l--; // из-за смещения элементов коллекции
                    }
                }

                Fill(parent.Children, source);
            }
        }
    }
}
