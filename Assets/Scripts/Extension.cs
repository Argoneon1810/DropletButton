using UnityEngine;
using UnityEngine.UI;

namespace DropletButton.EaseOfUse
{
    public static class Extension
    {
        public static bool AnyMatch(this float self, params float[] target)
        {
            foreach (float elem in target)
            {
                if (self == elem) return true;
            }
            return false;
        }

        public static Vector2 Rotate(this Vector2 v, float delta)
        {
            return new Vector2(
                v.x * Mathf.Cos(delta) + v.y * Mathf.Sin(delta),
                v.y * Mathf.Cos(delta) - v.x * Mathf.Sin(delta)
            );
        }

        public static Vector2 ToVector2(this Vector3 v) => new Vector2(v.x, v.y);

        public static Vector3 ToVector3(this Vector2 v) => new Vector3(v.x, v.y, 0);

        public static bool AsTrigger(this ref bool original)
        {
            if (original)
            {
                original = false;
                return true;
            }
            return false;
        }

        public static Vector3 GetMousePositionMatchingScaler(this Vector3 mousePos, Canvas targetCanvas)
        {
            CanvasScaler scaler = targetCanvas.GetComponent<CanvasScaler>();

            float scaledCanvasWidth = Useful.CanvasScale.GetWidthMatchingScaler(scaler);
            float scaledCanvasHeight = Useful.CanvasScale.GetHeightMatchingScaler(scaler);

            mousePos.x = mousePos.x / Screen.width * scaledCanvasWidth;
            mousePos.y = mousePos.y / Screen.height * scaledCanvasHeight;

            return mousePos;
        }

        public static Vector3 ToCoordinateZeroCenter(this Vector3 mousePos, Canvas targetCanvas)
        {
            CanvasScaler scaler = targetCanvas.GetComponent<CanvasScaler>();

            float scaledCanvasWidth = Useful.CanvasScale.GetWidthMatchingScaler(scaler);
            float scaledCanvasHeight = Useful.CanvasScale.GetHeightMatchingScaler(scaler);

            mousePos.x -= scaledCanvasWidth / 2;
            mousePos.y -= scaledCanvasHeight / 2;

            return mousePos;
        }
    }
}