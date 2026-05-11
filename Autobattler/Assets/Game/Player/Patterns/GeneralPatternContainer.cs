using Game.Core.PatternSystem;
using Game.Data.Patterns;
using UnityEngine;

namespace Game.Player.Patterns
{
    public class GeneralPatternContainer : PatternsContainer<GeneralPatternData>
    {
        public override void AddPattern(string patternId, GeneralPatternData pattern)
        {
            if (PatternsCollection.Count >= PlayerLimits.maxGeneralPatterns)
                return;

            if (!PatternsCollection.TryAdd(patternId, pattern))
            {
                Debug.Log("This pattern is alreay exists");
                //TODO Show UI with message
            }
        }

        public override void RemovePattern(string patternId)
        {
            if (PatternsCollection.Count <= 0)
            {
                Debug.Log("General patterns is 0");
                return;
            }

            var isRemoved = PatternsCollection.Remove(patternId);
            
            Debug.Log($"Pattern with name {patternId} has been removed with status {isRemoved}");
        }

        public override void ChangePattern(string oldPatternId, string newPatternId, GeneralPatternData pattern)
        {
            if (PatternsCollection.ContainsKey(newPatternId) || !PatternsCollection.ContainsKey(oldPatternId))
                return;

            if (!PatternsCollection.Remove(oldPatternId))
            {
                Debug.Log("Failed delete pattern from collection");
                return;
            }
            
            PatternsCollection[newPatternId] = pattern;
        }
    }
}