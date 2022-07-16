using System.Collections;
using UnityEngine;
using TMPro;

namespace Toast
{
    public class Toast : MonoBehaviour
    {
        RectTransform mRectTransform;
        Vector2 originalPosition;

        private float showSpeed = 1;

        public float length;
        public string text;
        public AnimationCurve toastShowHideCurve;
        public TextMeshProUGUI textMeshProUGUI;

        private void Awake()
        {
            mRectTransform = transform as RectTransform;
            originalPosition = mRectTransform.anchoredPosition;
        }

        public void Show()
        {
            textMeshProUGUI.text = text;
            StartCoroutine(ShowCoroutine());
        }

        IEnumerator ShowCoroutine()
        {
            float t = 0;
            Vector2 targetPosition = originalPosition + new Vector2(0, 200);
            while (t < 1)
            {
                t += Time.deltaTime / showSpeed;
                mRectTransform.anchoredPosition = Vector2.Lerp(originalPosition, targetPosition, toastShowHideCurve.Evaluate(t));
                yield return null;
            }
            yield return new WaitForSeconds(length);
            StartCoroutine(HideCoroutine());
        }

        IEnumerator HideCoroutine()
        {
            float t = 0;
            Vector2 fromPosition = mRectTransform.anchoredPosition;
            while (t < 1)
            {
                t += Time.deltaTime / showSpeed;
                mRectTransform.anchoredPosition = Vector2.Lerp(fromPosition, originalPosition, toastShowHideCurve.Evaluate(t));
                yield return null;
            }
            Destroy(gameObject);
        }
    }
}