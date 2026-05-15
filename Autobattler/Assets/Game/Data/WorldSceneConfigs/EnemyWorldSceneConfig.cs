using System;
using System.Collections.Generic;
using Game.Data.UnitConfigs;
using UnityEngine;

namespace Game.Data.WorldSceneConfigs
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "World Scene/Enemy")]
    public class EnemyWorldSceneConfig : ScriptableObject
    {
        [field:SerializeField] public EnemyStats EnemyStats { get; private set; }
        [field:SerializeField] public List<UnitWorldInfo> EnemyBattle { get; private set; }
        [field:SerializeField] public Animations Animations { get; private set; }
    }

    [Serializable]
    public class EnemyStats
    {
        public float speed;
        public int money;
    }

    [Serializable]
    public class Animations
    {
        public string walk;
        public string isAttack;
    }
}