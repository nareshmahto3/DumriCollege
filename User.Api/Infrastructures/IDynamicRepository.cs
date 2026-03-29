using System.Threading.Tasks;

namespace User.Api.Infrastructures
{
    public interface IDynamicRepository
    {
        Task<object> GetTableDataAsync(string tableName);
    }
}