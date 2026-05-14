using System;

namespace Game.Data.SpellConfigs
{
    [Serializable]
    public class EffectInfo
    {
        public EffectType effectType;
        public int turns;
    }
}