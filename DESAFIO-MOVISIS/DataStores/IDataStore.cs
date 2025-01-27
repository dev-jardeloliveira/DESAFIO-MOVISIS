namespace DESAFIO_MOVISIS.DataStore;

public interface IDataStore
{
    Task<TX> SingleAsync<TX>(Expression<Func<TX, bool>> query) where TX : new();
    Task<TX> SingleAsync<TX>() where TX : new();
    Task<int> CreateUniqueAsync<TX>(TX entity) where TX : new();
    Task<int> CreateOrReplaceAsync<TX>(TX entity) where TX : new();
    Task<int> CreateRangeUniqueAsync<TX>(List<TX> entity) where TX : new();
    Task<int> CreateRangeAsync<TX>(List<TX> entity) where TX : new();
    Task<int> CreateRangeUpdateAsync<TX>(List<TX> entity) where TX : new();
    Task<int> DeleteAsync<TX>(object key) where TX : new();
    Task<int> DeleteAsync<TX>(Expression<Func<TX, bool>> query) where TX : new();
    Task<int> DeleteAllAsync<TX>() where TX : new();
    Task<List<TX>> AllAsync<TX>() where TX : new();
    Task<List<TX>> AllAsync<TX>(Expression<Func<TX, bool>> query) where TX : new();
    Task<List<TX>> ManyAsync<TX>(Expression<Func<TX, bool>> query) where TX : new();
}
