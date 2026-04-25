using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class BossScene : MonoBehaviour
    {
        [SerializeField] private List<GameObject> bossScene;
        [SerializeField] private float bossSceneDelay;

        private void Awake()
        {
            foreach (var item in bossScene)
            {
                item.SetActive(false);
            }
        }

        public IEnumerator SpawnBossScene()
        {
            foreach (var item in bossScene)
            {
                item.SetActive(true);
                yield return new WaitForSeconds(bossSceneDelay);
            }
        }
    }
}