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
        IUserRoleReposity userRoleDal;
        IRoleReposity roleDal;
        IPermissionReposity pDal;
        IDepartmentReposity deDal;
        
        public SysUserInfoServices(ISysUserInfoReposity userDal, IUserRoleReposity userRoleDal, IRoleReposity roleDal,IPermissionReposity pDal, IDepartmentReposity deDal)
        {
            this.userDal = userDal;
            this.userRoleDal = userRoleDal;
            this.roleDal = roleDal;
            this.pDal = pDal;
            this.deDal = deDal;
            this.baseDal = userDal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public async Task<UserInfoViewModel> GetUserInfo(string UserId)
        {
            var result = await userDal.GetUserInfo(UserId);

            return result;
        }
    }
}
