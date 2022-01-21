using System.Threading.Tasks;

namespace MagazCore2._0.Repos
{
    public interface ICartRepo
    {
        Task AddprodAsync(string user_id, int prod_id);
        Task ClearCartAsync(string user_id);
    }
}