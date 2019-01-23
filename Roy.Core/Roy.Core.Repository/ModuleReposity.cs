using Roy.Core.IRepository;
using Roy.Core.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Roy.Core.Repository.Base;
using System.Threading.Tasks;

namespace Roy.Core.Repository
{
    public class ModuleReposity : BaseRepository<Module>, IModuleReposity
    {
        public async Task<List<Module>> GetUserModules(string userId)
        {
            List<Module> modules = new List<Module>();

            try
            {
                modules = this.Db.Queryable<Module, Permission, UserRole, User>((m, p, ur, u) => new object[]
                   {
                     JoinType.Inner,m.ModuleId == p.RolepElement,
                     JoinType.Inner,p.RoleId == ur.RoleId ,
                     JoinType.Inner,ur.UserId == u.UserId
                   })
                   .Where((m, p, ur, u) => u.UserId == userId && p.IsUse == "Y")
                   .OrderBy(m => m.ModuleId,OrderByType.Asc).OrderBy(m=>m.SortFlag,OrderByType.Asc).ToList();
            }
            catch (Exception ex)
            {

            }

            return await Task.Run(() => modules);
        }
    }
}
