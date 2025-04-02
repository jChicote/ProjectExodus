using System;

[Flags]
public enum EnemySpawnFilter
{
    Pawn = 1,
    Fighter = 2,
    Drone = 4, 
    Knight = 8
}
