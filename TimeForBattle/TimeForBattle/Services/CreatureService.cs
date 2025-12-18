using SQLite;

namespace TimeForBattle.Services;

public partial class CreatureService<T> where T : DatabaseObject, new()
{
    public SQLiteAsyncConnection database;

    public async Task Init()
    {
        if (database is null)
            database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        await database.CreateTableAsync<T>();
    }

    public async Task<List<T>> GetAllAsync()
    {
        await Init();
        return await database.Table<T>().ToListAsync();
    }
    public async Task<List<T>> GetAllByCombatAsync(int combatID)
    {
        await Init();
        return await database.Table<T>().Where(i => i.CombatID == combatID).ToListAsync();
    }

    public async Task<List<T>> GetAllByCreatureAsync(int creatureID)
    {
        await Init();
        return await database.Table<T>().Where(i => i.CreatureID == creatureID).ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        await Init();
        return await database.Table<T>().Where(i => i.Id == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveAsync(T t)
    {
        await Init();

        if (t is not null)
        {
            if (t.Id != 0)
                return await database.UpdateAsync(t);
            else
                return await database.InsertAsync(t);
        }

        return 0;
    } 

    public async Task<int> DeleteAsync(T t)
    {
        await Init();
        if (t is not null)
            return await database.DeleteAsync(t);

        return 0;
    }
}
