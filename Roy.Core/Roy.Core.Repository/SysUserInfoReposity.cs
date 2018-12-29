using Roy.Core.IRepository;
using Roy.Core.Model;
using Roy.Core.Model.ViewModel;
using Roy.Core.Repository.Base;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Roy.Core.Repository
{
    public class SysUserInfoReposity : BaseRepository<User>, ISysUserInfoReposity
    {
        public async Task<UserInfoViewModel> GetUserInfo(LoginViewModel loginModel)
        {
            UserInfoViewModel userInfo = new UserInfoViewModel();
            try
            {
                userInfo = this.Db.Queryable<User, UserRole, Role, Department>((u, ur, r, d) => new object[]
                  {
                     JoinType.Inner,u.UserId==ur.UserId,
                     JoinType.Inner,ur.RoleId==r.RoleId,
                     JoinType.Left,u.DeptId==d.DeptId
                  })
                  .WhereIF(!string.IsNullOrWhiteSpace(loginModel.LoginUserId),e=>e.UserId==loginModel.LoginUserId)
                  .WhereIF(!string.IsNullOrWhiteSpace(loginModel.LoginPwd), e => e.UserId == loginModel.LoginPwd)
                  .Select((u, ur, r, d) => new UserInfoViewModel
                  {
                      UserId = u.UserId,
                      UserName = u.UserName,
                      RoleId = ur.RoleId,
                      RoleLevel = r.RoleLevel,
                      RoleName = r.RoleName,
                      DeptId = d.DeptId,
                      DeptName = d.DeptName
                  }).First();
            }
            catch (Exception ex)
            {

            }

            return await Task.Run(() => userInfo);
        }
    }
}
