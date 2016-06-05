using SQLite.Net.Interop;

namespace Pedidos.Interface
{
    public interface IConfig
    {
        string DirectorioDB {get;}
        ISQLitePlatform Platforma { get; }
    }
}
