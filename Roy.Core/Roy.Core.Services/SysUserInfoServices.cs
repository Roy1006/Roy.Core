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
        
        public SysUserInfoServices(ISysUserInfoReposity userDal)
        {
            this.userDal = userDal;
            this.baseDal = userDal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public async Task<UserInfoViewModel> GetUserInfo(LoginViewModel vm)
        {
            var result = await userDal.GetUserInfo(vm);

            return result;
        }
    }
}
