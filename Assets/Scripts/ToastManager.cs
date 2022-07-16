using UnityEngine;

namespace Toast
{
    public class ToastManager : MonoBehaviour
    {
        public static ToastManager instance;
        public GameObject toastPrefab;
        public Canvas toastCanvas;
        public AnimationCurve toastShowHideCurve;

        private void Awake()
        {
            instance = this;
        }

        public Toast MakeText(string text, float length)
        {
            Toast toast = Instantiate(toastPrefab, toastCanvas.transform).GetComponent<Toast>();
            //toast.transform.SetParent(toastCanvas.transform);
            toast.text = text;
            toast.length = length;
            toast.toastShowHideCurve = toastShowHideCurve;
            return toast;
        }
    }
}