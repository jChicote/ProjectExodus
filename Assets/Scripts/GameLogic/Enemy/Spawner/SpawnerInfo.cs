using System;

[Serializable]
public class SpawnerInfo
{
    
    #region - - - - - - Fields - - - - - -

    public float SpawnRatio;
    public float MinDeviation;
    public float MaxDeviation;

    #endregion Fields

}

[Serializable]
public class SpawnerDifficultyMultiplier
{

    #region - - - - - - Fields - - - - - -

    public float Easy;
    public float Normal;
    public float Hard;

    #endregion Fields

}