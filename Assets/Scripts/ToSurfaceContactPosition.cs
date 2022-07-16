using System;
using UnityEngine;
using DropletButton.Shape;
using DropletButton.EaseOfUse;

namespace DropletButton.Movement
{
    public class ToSurfaceContactPosition : MonoBehaviour
    {
        [SerializeField] Circle self;
        [SerializeField] Circle targetA, targetB;
        [SerializeField] bool escapeDownward;

        float a, b, c;

        public event Action OnDiscontact;

        public bool isLeft;

        private void Start()
        {
            a = self.radius + targetA.radius;
            c = targetB.radius + self.radius;
        }

        private void Update()
        {
            Vector2 dirAB = targetB.position - targetA.position;
            b = dirAB.magnitude;
            float allowance = b - targetB.radius - targetA.radius;

            if (allowance > self.radius * 2 || allowance < -targetA.radius * 2)
            {
                SnapToZeroPosition();
                OnDiscontact?.Invoke();
                return;
            }

            float theta = dirAB.x != 0 ? Mathf.Atan(dirAB.y / dirAB.x) : 0;

            float gamma = Mathf.Acos((a * a + b * b - c * c) / (2 * a * b));

            float angle;
            angle = escapeDownward ? theta + gamma : theta - gamma;

            Vector3 offset = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg) * (isLeft ? -targetA.transform.right : targetA.transform.right) * a;
            (self.transform as RectTransform).anchoredPosition = targetA.position + offset.ToVector2();
        }

        private void SnapToZeroPosition()
        {
            (self.transform as RectTransform).anchoredPosition = targetB.position + (targetA.position - targetB.position).normalized * (targetB.radius + self.radius);
        }
    }

}