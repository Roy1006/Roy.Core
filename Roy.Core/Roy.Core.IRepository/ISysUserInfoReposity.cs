﻿using System;
using System.Collections.Generic;
using System.Text;
using Roy.Core.Model;
using Roy.Core.IRepository.Base;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Roy.Core.Model.ViewModel;

namespace Roy.Core.IRepository
{
    public interface ISysUserInfoReposity : IBaseRepository<User>
    {
        Task<UserInfoViewModel> GetUserInfo(LoginViewModel loginModel);
    }
}
