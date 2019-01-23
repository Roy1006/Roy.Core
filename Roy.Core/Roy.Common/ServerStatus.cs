using System;

namespace Roy.Common
{
    public class ServerStatus
    {
        public int Code { get; set; }
        public string Desc { get; set; }

        public ServerStatus()
        {

        }

        public override string ToString()
        {
            return string.Format("ServerSatus {{Code: {0},Desc: \"{1}\" }}", this.Code, this.Desc);
        }

        public static readonly ServerStatus Success = new ServerStatus() { Code = 200, Desc = "OK" };
        public static readonly ServerStatus LoginFail = new ServerStatus() { Code = 300, Desc = "用户名或密码错误！！！" };
    }

    public class ReturnObject<T>
    {
        public int Count { get; set; } = 0;

        public T Data { get; set; }

        public ServerStatus Status { get; set; }

        public ReturnObject(T _data, int _count, ServerStatus _status)
        {
            Data = _data;
            Count = _count;
            Status = _status;
        }

        public ReturnObject(T _data, ServerStatus _status)
        {
            Data = _data;
            Status = _status;
        }

        public ReturnObject()
        {

        }
    }
}

