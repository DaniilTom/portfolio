using AutoMapper;
using Microsoft.Extensions.Logging;
using SightMap.BLL.CustomCache;
using SightMap.BLL.DTO;
using SightMap.BLL.Filters;
using SightMap.BLL.Infrastructure.Interfaces;
using SightMap.BLL.PluploadManager;
using SightMap.DAL.Models;
using SightMap.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SightMap.BLL.Infrastructure.Implementations
{
    public class AlbumManager : BaseManager<AlbumDTO, AlbumFilterDTO, Album>, IAlbumEditor<AlbumDTO, AlbumFilterDTO>
    {
        private IPluploadManager _uploadManager;

        public AlbumManager(ILogger<AlbumManager> _logger,
                                  IRepository<Album> _repo,
                                  IMapper _mapper,
                                  ICustomCache _cache,
                                  IPluploadManager uploadManager) : base(_logger, _repo, _mapper, _cache)
        {
            _uploadManager = uploadManager;
        }

        public IEnumerable<AlbumDTO> Edit(IEnumerable<AlbumDTO> album, string refId)
        {
            List<AlbumDTO> result = new List<AlbumDTO>();

            if (album.Count() > 0)
            {
                try
                {
                    result = new List<AlbumDTO>();
                    foreach (var page in album)
                    {
                        switch (page.State)
                        {
                            case State.Add:
                                AlbumDTO temp = null;
                                try
                                {
                                    string newFileName = _uploadManager.MoveToMain(refId, page.ItemId.ToString(), page.ImageName);
                                    page.ImageName = newFileName;
                                    if (string.IsNullOrEmpty(page.Title))
                                        page.Title = "Default";
                                    temp = base.Add(page);
                                    result.Add(temp);
                                }
                                catch (Exception exp)
                                {
                                    //base.Delete(temp.Id);
                                    throw;
                                }
                                break;
                            case State.Edit:
                                base.Edit(page);
                                break;
                            case State.Delete:
                                base.Delete(page.Id);
                                _uploadManager.DeleteFromMain(page.ItemId.ToString(), page.ImageName);
                                break;
                        }
                    }
                    _uploadManager.DeleteTempDirectory(refId);
                }
                catch (Exception exp)
                {
                    _uploadManager.DeleteTempDirectory(refId);
                    result = null;
                }
            }

            return result?.ToArray();
        }

        public override IEnumerable<AlbumDTO> GetListObjects(AlbumFilterDTO filter, bool isCachedUsed = true)
        {
            IEnumerable<AlbumDTO> result = base.GetListObjects(filter, isCachedUsed);

            if (result != null)
            {
                string webRootPath = _uploadManager.GetRelativeMainPath(filter.ItemId.ToString());
                result.AsParallel().ForAll(p => p.ImagePath = webRootPath + "\\" + p.ImageName);
            }

            return result;
        }

        protected override IFilter<Album> ConfigureFilter(AlbumFilterDTO dto) => new AlbumFilter(dto);
    }
}
