namespace ProjectExodus.GameLogic.Enumeration
{

    public abstract class SmartEnum
    {

        #region - - - - - - Constructors - - - - - -

        protected SmartEnum(string name, int value)
        {
            this.m_Name = name;
            this.m_Value = value;
        }

        #endregion Constructors
  
        #region - - - - - - Properties - - - - - -

        private int m_Value;
        private string m_Name;

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        public int GetValue()
            => m_Value;

        public override string ToString() 
            => this.m_Name;

        #endregion Methods

    }

}