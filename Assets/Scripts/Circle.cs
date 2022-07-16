using UnityEngine;

namespace DropletButton.Shape
{
    public class Circle : MonoBehaviour
    {
        public float radius = 50;
        public Vector2 position => (transform as RectTransform).anchoredPosition;

        public virtual void OnValidate()
        {
            RectTransform rt = transform as RectTransform;

            Vector2 sizeDelta = rt.sizeDelta;
            sizeDelta.x = radius * 2;
            sizeDelta.y = radius * 2;
            rt.sizeDelta = sizeDelta;
        }
    }

}