using System.Threading.Tasks;
using Roy.Core.Model;

namespace Roy.Core.IServices
{	
	/// <summary>
	/// UserRoleServices
	/// </summary>	
    public interface IUserRoleServices :IBaseServices<UserRole>
	{
        Task<UserRole> SaveUserRole(int uid, int rid);
    }
}

