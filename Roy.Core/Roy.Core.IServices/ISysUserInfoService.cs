using Roy.Core.Model;
using Roy.Core.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Roy.Core.IServices
{
    public interface ISysUserInfoService : IBaseServices<User>
    {
        bool Login(LoginViewModel vModel);
    }
}
