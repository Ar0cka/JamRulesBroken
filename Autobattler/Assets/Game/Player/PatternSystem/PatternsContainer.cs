using System;
using System.Collections.Generic;
using Game.Data.Patterns;
using Game.Data.Player;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using UnityEngine;

namespace Game.Player.PatternSystem
{
    public class PatternsContainer : MonoBehaviour, IPatternController
    {
        //[SerializeField] private string pathToDefaultConfig;

        [SerializeField] private PlayerStartPatterns playerPatterns;

        private readonly Dictionary<string, GeneralPattern> _generalPatterns = new();
        private readonly Dictionary<string, SamplePattern> _samplePatterns = new();
        
        public void InitializePatternContainer()
        {
            //TODO Check save and after loading base data from resources

            var isCanLoadSave = CheckSave();

            if (isCanLoadSave.IsResult)
            {
                Load();
                return;
            }

            var playerCollection = playerPatterns.Clone();
            
            InitializeGeneralPatterns(playerCollection.generalsPatterns);
            InitializeSamplePatterns(playerCollection.samplePatterns);
        }
        private void InitializeGeneralPatterns(List<GeneralPattern> generalPatterns)
        {
            foreach (var pattern in generalPatterns)
            {
                if (!_generalPatterns.TryAdd(pattern.PatternId, pattern))
                {
                    Debug.Log($"Pattern with id {pattern.PatternId} already exists");
                }
            }
        }
        private void InitializeSamplePatterns(List<SamplePattern> samplePatterns)
        {
            foreach (var pattern in samplePatterns)
            {
                if (!_samplePatterns.TryAdd(pattern.PatternId, pattern))
                {
                    Debug.Log($"Pattern with id {pattern.PatternId} already exists");
                }
            }
        }
        
        
        
        private (bool IsResult, PlayerStartPatterns value) CheckSave()
        {
            return (false, null);
        }

        private void Load()
        {
            
        }

        public void AddPattern(string patternId, DefaultPattern pattern)
        {
            throw new NotImplementedException();
        }

        public void RemovePattern(string patternId)
        {
            throw new NotImplementedException();
        }
    }

    public interface IPatternController
    {
        public void AddPattern(string patternId, DefaultPattern pattern);
        public void RemovePattern(string patternId);
    }
}