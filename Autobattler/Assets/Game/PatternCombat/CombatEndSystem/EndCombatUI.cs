using System;
using System.Collections.Generic;
using Game.Core.SceneManagerWorld.SendData;
using Game.Data.UnitConfigs;
using UnityEngine;

namespace Game.PatternCombat.CombatEndSystem
{
    public class EndCombatUI : MonoBehaviour
    {
        private Dictionary<string, UnitWorldInfo> _playerStartUnits;

        public void Initialize(ref Action<SendToOutputData> onEndCombat)
        {
            _playerStartUnits = new();

            onEndCombat += ChooseEndPanel;
        }

        private void ChooseEndPanel(SendToOutputData outputData)
        {
            //TODO Show Lost fight or Show win fight with lost units and rewards
        }
    }
}