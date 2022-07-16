using UnityEngine;
using UnityEngine.UI;
using DropletButton.EaseOfUse;

namespace DropletButton.Movement
{
    public class MoveToOffscreen : MonoBehaviour
    {
        public enum Mode
        {
            Top,
            Bottom,
            Left,
            Right,
        }
        public Mode mode;
        public float offsetX, offsetY;

        void Awake()
        {
            CanvasScaler scaler = GetComponentInParent<CanvasScaler>();
            RectTransform rt = transform as RectTransform;
            Vector2 pos = rt.anchoredPosition;
            if (mode == Mode.Right)
            {
                pos.x = Useful.CanvasScale.GetWidthMatchingScaler(scaler) / 2 + offsetX;
                pos.y += offsetY;
            }
            else if (mode == Mode.Left)
            {
                pos.x = -Useful.CanvasScale.GetWidthMatchingScaler(scaler) / 2 + offsetX;
                pos.y += offsetY;
            }
            else if (mode == Mode.Top)
            {
                pos.x += offsetX;
                pos.y = Useful.CanvasScale.GetHeightMatchingScaler(scaler) / 2 + offsetY;
            }
            else //mode == Mode.Bottom
            {
                pos.x += offsetX;
                pos.y = -Useful.CanvasScale.GetHeightMatchingScaler(scaler) / 2 + offsetY;
            }

            rt.anchoredPosition = pos;
        }
    }

}