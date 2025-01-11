namespace ProjectExodus.GameLogic.Common.Health
{

    public interface IDamageable
    {

        #region - - - - - - Methods - - - - - -

        bool CanDamage();

        void SendDamage(float damage);

        #endregion Methods

    }

}