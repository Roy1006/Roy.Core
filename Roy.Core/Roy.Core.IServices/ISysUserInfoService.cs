using Roy.Core.Model;
using Roy.Core.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Roy.Core.IServices
{
    public interface ISysUserInfoService : IBaseServices<User>
    {
        Task<List<UserInfoViewModel>> GetUserInfo(LoginViewModel vm);
    }
}
