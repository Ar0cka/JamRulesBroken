using System;
using System.Collections.Generic;
using Game.Data.Patterns;
using Game.Data.Player;
using Game.SaveSystem;

namespace Game.Core.PatternSystem
{
    public abstract class PatternsContainer<TType> : ISavable
        where TType : DefaultPattern
    {
        
        protected readonly Dictionary<string, TType> PatternsCollection = new();
        public IReadOnlyDictionary<string, TType> GetCollection() => PatternsCollection;

        protected PlayerPatternSettings PlayerLimits;
        
        public virtual void InitializePatternContainer(PlayerPatternSettings playerPatternSettings, PatternData<TType> patterns)
        {
            foreach (var item in patterns.Patterns)
            {
                PatternsCollection.TryAdd(item.PatternID, item);
            }

            PlayerLimits = playerPatternSettings;
        }

        public abstract void AddPattern(string patternId, TType pattern);
        public abstract void RemovePattern(string patternId);
        public abstract void ChangePattern(string oldPatternId, string newPatternId, TType pattern);
        
        public object Capture()
        {
            return new PatternData<TType>(new List<TType>(PatternsCollection.Values));
        }
        public void Restore(object data)
        {
            if (data is List<TType> list)
            {
                foreach (var item in list)
                {
                    PatternsCollection.TryAdd(item.PatternID, item);
                }
            }
        }
    }

    [Serializable]
    public class PatternData<TType> where TType : DefaultPattern
    {
        public List<TType> Patterns { get; private set; }
        
        public PatternData(List<TType> patterns)
        {
            Patterns = patterns;
        }
    }
}