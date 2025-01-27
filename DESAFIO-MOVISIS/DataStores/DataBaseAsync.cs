namespace DESAFIO_MOVISIS.DataStore;

public class DataBaseAsync : IDataStore
{
    private SQLiteAsyncConnection database;

    private async Task Init()
    {
        if (this.database != null)
        {
            return;
        }

        this.database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);

        _ = await this.database.CreateTableAsync<Usuario>();
    }

    public async Task<List<TX>> AllAsync<TX>() where TX : new()
    {
        await this.Init();
        return await this.database!.Table<TX>().ToListAsync();
    }

    public async Task<List<TX>> AllAsync<TX>(Expression<Func<TX, bool>> query) where TX : new()
    {
        await this.Init();
        return await this.database!.Table<TX>().Where(query).ToListAsync();
    }

    public async Task<int> CreateUniqueAsync<TX>(TX entity) where TX : new()
    {
        await this.Init();
        _ = await this.database!.DeleteAllAsync<TX>();
        return await this.database!.InsertAsync(entity, typeof(TX));
    }

    public async Task<int> CreateOrReplaceAsync<TX>(TX entity) where TX : new()
    {
        await this.Init();
        return await this.database!.InsertOrReplaceAsync(entity, typeof(TX));
    }

    public async Task<int> CreateRangeUniqueAsync<TX>(List<TX> entity) where TX : new()
    {
        await this.Init();
        _ = await this.database!.DeleteAllAsync<TX>();
        return await this.database!.InsertAllAsync(entity, typeof(TX));
    }

    public async Task<int> CreateRangeAsync<TX>(List<TX> entity) where TX : new()
    {
        await this.Init();
        return await this.database!.InsertAllAsync(entity, typeof(TX));
    }

    public async Task<int> CreateRangeUpdateAsync<TX>(List<TX> entity) where TX : new()
    {
        await this.Init();
        return await this.database!.UpdateAllAsync(entity);
    }

    public async Task<int> DeleteAllAsync<TX>() where TX : new()
    {
        await this.Init();
        return await this.database!.DeleteAllAsync<TX>();
    }

    public async Task<int> DeleteAsync<TX>(Expression<Func<TX, bool>> query) where TX : new()
    {
        await this.Init();
        return await this.database!.DeleteAsync(query);
    }

    public async Task<List<TX>> ManyAsync<TX>(Expression<Func<TX, bool>> query) where TX : new()
    {
        await this.Init();
        return await this.database!.Table<TX>().Where(query).ToListAsync();
    }

    public async Task<TX> SingleAsync<TX>(Expression<Func<TX, bool>> query) where TX : new()
    {
        await this.Init();
        return await this.database!.GetAsync(query);
    }

    public async Task<TX> SingleAsync<TX>() where TX : new()
    {
        await this.Init();
        return await this.database!.Table<TX>().FirstOrDefaultAsync();
    }

    public async Task<int> DeleteAsync<TX>(object key) where TX : new()
    {
        await this.Init();
        return await this.database!.DeleteAsync<TX>(key);
    }
}

