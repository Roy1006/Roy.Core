
using System;
using System.IO;
using System.Linq;

namespace Roy.Core.Repository
{
    public class BaseDBConfig
    {
        //public static string ConnectionString = File.ReadAllText(@"D:\my-file\dbCountPsw1.txt").Trim();

        //正常格式是

         public static string ConnectionString = "Data Source=localhost;User Id=root;Password=royzz;database=roydata;";
    }
}
