using UnityEngine;

namespace ProjectExodus.UserInterface.OptionsMenu
{

    public class OptionsMenuContentViews
    {

        #region - - - - - - Constructors - - - - - -

        public OptionsMenuContentViews(
            GameObject audioOptionsContentGroup,
            GameObject inputOptionsContentGroup,
            GameObject graphicsOptionsContentGroup,
            GameObject userInterfaceOptionsContentGroup)
        {
            this.AudioOptionsContentGroup = audioOptionsContentGroup;
            this.InputOptionsContentGroup = inputOptionsContentGroup;
            this.GraphicsOptionsContentGroup = graphicsOptionsContentGroup;
            this.UserInterfaceOptionsContentGroup = userInterfaceOptionsContentGroup;
        }

        #endregion Constructors
        
        #region - - - - - - Properties - - - - - -

        public GameObject AudioOptionsContentGroup { get; private set; }
        
        public GameObject InputOptionsContentGroup { get; private set; }
        
        public GameObject GraphicsOptionsContentGroup { get; private set; }
        
        public GameObject UserInterfaceOptionsContentGroup { get; private set; }

        #endregion Properties

    }

}