using ProjectExodus.GameLogic.Enumeration;

public class GameplayHUDEvents : SmartEnum
{

    #region - - - - - - Fields - - - - - -

    // Movement
    public static GameplayHUDEvents UpdateAfterburn = new("UpdateAfterburn", 0);
    public static GameplayHUDEvents FadeOutAfterburn = new("FadeOutAfterburn", 1);
    
    // Health
    public static GameplayHUDEvents SetupHealthHUD = new("SetupHealthHUD", 2);
    public static GameplayHUDEvents UpdateHealth = new("UpdateHealthValues", 3);
    
    // Weapons
    public static GameplayHUDEvents UpdateCooldown = new("UpdateCooldown", 4);
    public static GameplayHUDEvents AddWeaponIndicator = new("AddWeaponIndicator", 5);
    public static GameplayHUDEvents UpdateWeaponIndicator = new("UpdateWeaponIndicator", 6);
    
    // Screen
    public static GameplayHUDEvents ShowHUD = new("ShowGameplayHUD", 00);
    public static GameplayHUDEvents HideHUD = new("HideGameplayHUD", 00);

    #endregion Fields

    #region - - - - - - Constructors - - - - - -

    public GameplayHUDEvents(string name, int value) : base(name, value)
    {
    }

    #endregion Constructors

}
