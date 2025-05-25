public interface IEnemySpawner
{

    #region - - - - - - Methods - - - - - -

    void Spawn(SpawnerRequest spawnRequest, EnemySpawnFilter spawnFilter);

    #endregion Methods

}
