using Roy.Core.Model;
using Roy.Core.Model.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Roy.Core.IServices
{
    public interface IModuleService : IBaseServices<Module>
    {
        Task<List<ModuleViewModel>> GetUserModules(string userId);
    }
}
