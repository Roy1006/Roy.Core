using Roy.Core.IRepository;
using Roy.Core.IServices;
using Roy.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Roy.Core.Services.Base;


namespace Roy.Core.Services
{
    public class ModuleServices : BaseServices<Module>, IModuleService
    {
        IModuleReposity dal;
        public ModuleServices(IModuleReposity dal)        
        {
            this.dal = dal;
            base.baseDal = dal;
        }
    }
}
