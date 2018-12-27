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
        ISysUserInfoReposity dal;
        public SysUserInfoServices(ISysUserInfoReposity dal)
        {
            this.dal = dal;
            base.baseDal = dal;
        }

        public bool Login(LoginViewModel vModel)
        {
            var user = Query(m => m.UserId == vModel.LoginUserId && m.Password == vModel.LoginPwd);

            return user.Result.Count == 0 ? false : true;
        }
    }
}
