/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using ICSLib.Authen.Models.Genders;
using ICSLib.BaseModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ICSLib.Authen.Services.Interfaces
{
    public interface IGenderService
    {
        Task<ApiResult<GenderViewModel>> GetById(int id);
        Task<ApiResult<List<GenderViewModel>>> GetAll();
        Task<ApiResult<PagedResult<GenderViewModel>>> GetListPaging(GetGenderPagingRequest request);
        Task<ApiResult<GenderViewModel>> Create(GenderCreateRequest request);
        Task<ApiResult<GenderViewModel>> Update(GenderEditRequest request);
        Task<ApiResult<int>> Delete(int id);
    }
}
