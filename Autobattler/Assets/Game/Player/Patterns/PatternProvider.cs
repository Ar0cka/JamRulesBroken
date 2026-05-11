using System;
using System.Collections.Generic;
using Game.Core.PatternSystem;
using Game.Data.Patterns;
using Game.Data.Player;
using NUnit.Framework;
using UnityEngine;

namespace Game.Player.Patterns
{
    public class PatternProvider : MonoBehaviour
    {
        private GeneralPatternContainer _generalContainer;
        private SamplePatternContainer _sampleContainer;

        [SerializeField] private PatternLimits patternLimits;
        [SerializeField] private PlayerStartPatterns startPlayerPatterns;
        
        private void Awake()
        {
            var generalPatternData = new PatternData<GeneralPatternData>(
                new List<GeneralPatternData>(startPlayerPatterns.StartPlayerPatterns.generalsPatterns));
            var samplePatternData = new PatternData<SamplePatternData>(
                new List<SamplePatternData>(startPlayerPatterns.StartPlayerPatterns.samplePatterns));

            var patternLimit = patternLimits.Clone();
            
            _generalContainer.InitializePatternContainer(patternLimit, generalPatternData);
            _sampleContainer.InitializePatternContainer(patternLimit, samplePatternData);
        }
    }
}