using System;
using System.Collections.Generic;
using ScriptableObjects.UnitConfigs;
using UnityEngine;

namespace ScriptableObjects.WorldSceneConfigs
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "World Scene/Enemy")]
    public class EnemyWorldSceneConfig : ScriptableObject
    {
        [field:SerializeField] public EnemyStats EnemyStats { get; private set; }
        [field:SerializeField] public List<EnemyBattle> EnemyBattle { get; private set; }
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

    [Serializable]
    public class EnemyBattle
    {
        public UnitConfig units;
        public int count;
    }
}