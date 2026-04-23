using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UISystem
{
    public class PopObject : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textBox;
        [SerializeField] private float speed;
        [SerializeField] private float lifeTime;

        public void LaunchPop(string text)
        {
            textBox.text = text;

            var distance = speed * lifeTime;
            Vector3 endPosition = transform.position + Vector3.up * distance;

            gameObject.transform.DOMove(endPosition, speed).SetSpeedBased(true).SetEase(Ease.Linear);
            
            Destroy(gameObject, lifeTime);
        }
    }
}