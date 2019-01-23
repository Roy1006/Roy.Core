using Roy.Core.IRepository;
using Roy.Core.IServices;
using Roy.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Roy.Core.Services.Base;
using System.Threading.Tasks;
using Roy.Core.Model.ViewModel;
using System.Linq;

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

        public async Task<List<ModuleViewModel>> GetUserModules(string userId)
        {
            var result = await moduleDal.GetUserModules(userId);

            List<ModuleViewModel> modules = new List<ModuleViewModel>();

            foreach (var item in result)
            {
                ModuleViewModel info = new ModuleViewModel();

                info.ModuleId = item.ModuleId;
                info.ModuleName = item.ModuleName;
                info.ParentId = item.ParentId;
                info.SortFlag = item.SortFlag;
                info.TargetUrl = item.TargetUrl;
                info.ChildrenList = new List<ModuleViewModel>();

                modules.Add(info);
            }

            var parents = modules.Where(e=>e.ParentId == "0").OrderBy(e=>e.SortFlag).ToList();


            List<ModuleViewModel> modules1 = new List<ModuleViewModel>();
            foreach (var item in parents)
            {
                ModuleViewModel info = new ModuleViewModel();
                info.ModuleId = item.ModuleId;
                info.ModuleName = item.ModuleName;
                info.ParentId = item.ParentId;
                info.SortFlag = item.SortFlag;
                info.TargetUrl = item.TargetUrl;
                info.ChildrenList = modules.Where(e => e.ParentId == info.ModuleId).OrderBy(e => e.SortFlag).ToList();

                info.ChildrenList = GetChildrens(info, modules);
                modules1.Add(info);
            }

            return modules1;
        }

        private List<ModuleViewModel> GetChildrens(ModuleViewModel childrenInfo, List<ModuleViewModel> modules1)
        {
            List<ModuleViewModel> modules = new List<ModuleViewModel>();

            foreach (var item in childrenInfo.ChildrenList)
            {
                ModuleViewModel info = new ModuleViewModel();
                info.ModuleId = item.ModuleId;
                info.ModuleName = item.ModuleName;
                info.ParentId = item.ParentId;
                info.SortFlag = item.SortFlag;
                info.TargetUrl = item.TargetUrl;
                info.ChildrenList = modules1.Where(e => e.ParentId == info.ModuleId).OrderBy(e => e.SortFlag).ToList();

                GetChildrens(info, modules1);

                modules.Add(info);
            }

            return modules;

        }
    }
}
