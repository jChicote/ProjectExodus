namespace ProjectExodus.GameLogic.Enumeration
{

    public abstract class SmartEnum
    {

        #region - - - - - - Constructors - - - - - -

        public SmartEnum(string name, int value)
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
            => this.m_Value; 


        #endregion Methods

    }

}