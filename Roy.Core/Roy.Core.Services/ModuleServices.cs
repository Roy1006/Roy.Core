using Roy.Core.IRepository;
using Roy.Core.IServices;
using Roy.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Roy.Core.Services.Base;
using System.Threading.Tasks;

namespace Roy.Core.Services
{
    public class ModuleServices : BaseServices<Module>, IModuleService
    {
        IModuleReposity moduleDal;
        public ModuleServices(IModuleReposity dal)        
        {
            this.moduleDal = dal;
            base.baseDal = dal;
        }

        public async Task<List<Module>> GetUserModules(string userId)
        {
            var result = await moduleDal.GetUserModules(userId);

            return result;
        }
    }
}
