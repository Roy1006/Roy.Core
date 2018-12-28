using Roy.Core.IRepository;
using Roy.Core.IServices;
using Roy.Core.Model;
using Roy.Core.Model.ViewModel;
using Roy.Core.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Roy.Core.Services
{
    public class SysUserInfoServices : BaseServices<User>, ISysUserInfoService
    {
        ISysUserInfoReposity userDal;
        IUserRoleServices dal;
        
        public SysUserInfoServices(ISysUserInfoReposity dal)
        {
            //this.dal = dal;
            //base.baseDal = dal;
        }

        public Task<List<UserInfoViewModel>> GetUserInfo(LoginViewModel vm)
        {
            throw new NotImplementedException();
        }
    }
}
