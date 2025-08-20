namespace TimeForBattle.Services;

public partial class InitiativeService<T> : CreatureService<T> where T : InitiativeCreatureData, new() 
{
    public async Task<List<T>> GetAllByCombatAsync(int combatID)
    {
        await base.Init();
        return await database.Table<T>().Where(i => i.CombatID == combatID).ToListAsync();
    }

    public async Task<List<T>> GetAllByCreatureAsync(int creatureID)
    {
        await base.Init();
        return await database.Table<T>().Where(i => i.CreatureID == creatureID).ToListAsync();
    }
}
