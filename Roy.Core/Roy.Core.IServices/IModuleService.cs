using Roy.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Roy.Core.IServices
{
    public interface IModuleService : IBaseServices<Module>
    {
        Task<List<Module>> GetUserModules(string userId);
    }
}
