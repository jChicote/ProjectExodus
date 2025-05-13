using System;
using System.Collections.Generic;
using UnityEditor;

namespace ProjectExodus.GameLogic.Enumeration
{

    public class GameTag : SmartEnum
    {

        #region - - - - - - Fields - - - - - -

        private static readonly Dictionary<string, GameTag> Tags = new();

        public static GameTag Interactable = new("Interactable", 0);
        public static GameTag Enemy = new("Enemy", 1);
        public static GameTag Projectile = new("Projectile", 2);
        public static GameTag CollectablePickup = new("CollectablePickup", 3);
        public static GameTag Default = new("Default", 999);
        
        // Pre-existing tags
        public static GameTag Player = new("Player", 2);

        #endregion Fields
  
        #region - - - - - - Constructors - - - - - -

        public GameTag(string name, int value) : base(name, value)
            => Tags[name] = this;

        #endregion Constructors
        
        #region - - - - - - Methods - - - - - -

        public static implicit operator GameTag(string value)
        {
            if (Tags.TryGetValue(value, out var _GameTag))
                return _GameTag;
            
            throw new ArgumentException($"Invalid value '{value}' for GameTag.");
        }

        public static implicit operator int(GameTag gameLayer)
            => gameLayer.GetValue();

        public static implicit operator string(GameTag gameLayer)
            => gameLayer.ToString();

        public static bool IsValid(string tag)
            => tag == Player | tag == Enemy | tag == Interactable;

        #endregion Methods

    }

}