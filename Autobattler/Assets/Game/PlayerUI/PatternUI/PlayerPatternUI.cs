using Game.Core.HudAbstraction;
using Game.Data.UIConfigs;
using Game.Player.Patterns;
using NUnit.Framework;
using UnityEngine;

namespace Game.PlayerUI
{
    public class PlayerPatternUI : BaseMenu<PatternProvider, PatternBookConfig>
    {
        //TODO List for Pattern Cards
        
        public override void Initialize(PatternProvider initializeData)
        {
            Data = initializeData;
        }
    }
}