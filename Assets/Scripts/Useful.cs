using UnityEngine;
using UnityEngine.UI;

namespace DropletButton.EaseOfUse
{
    public class Useful
    {
        public class CanvasScale
        {
            public static float GetWidthMatchingScaler(CanvasScaler scaler) => Mathf.Lerp(
                scaler.referenceResolution.x,
                scaler.referenceResolution.y * Camera.main.aspect,
                scaler.matchWidthOrHeight
            );

            public static float GetHeightMatchingScaler(CanvasScaler scaler) => Mathf.Lerp(
                scaler.referenceResolution.x / Camera.main.aspect,
                scaler.referenceResolution.y,
                scaler.matchWidthOrHeight
            );
        }
        public class Calculation
        {
            public static bool Similar(Vector3 left, Vector3 right, float threshold)
            {
                if ((left - right).sqrMagnitude <= (threshold * threshold)) return true;
                return false;
            }
        }
    }
}