using ProjectExodus.GameLogic.Enumeration;

namespace ProjectExodus
{

    public class EnemyType : SmartEnum
    {

        #region - - - - - - Fields - - - - - -

        public static EnemyType ZetoPawn = new("Zeto Pawn", 1);
        public static EnemyType ZetoFighter = new("Zeto Fighter", 2);
        public static EnemyType ZetoDrone = new("Zeto Drone", 3);
        public static EnemyType ZetoKnight = new("Zeto Knight", 4);
        public static EnemyType ZetoSentinelDrone = new("Zeto Sentinel Drone", 5);

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public EnemyType(string name, int value) : base(name, value) { }

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        public static implicit operator int(EnemyType enemyType)
            => enemyType.GetValue();

        public static implicit operator string(EnemyType enemyType)
            => enemyType.ToString();

        #endregion Methods
        
    }

}