using System.Collections;
using UnityEngine;
using DropletButton.Shape;
using DropletButton.EaseOfUse;

namespace DropletButton.Interaction
{
    [ExecuteAlways]
    public class CallDropletButton : MonoBehaviour
    {
        Vector2 initialPositionSmall;
        Vector2 initialPositionBig;

        [Header("Fields of the droplet button")]
        [SerializeField] float playTimeLength = 1;
        [SerializeField] Circle theBigOne;
        [SerializeField] AnimationCurve bigOutCurve;
        [SerializeField] AnimationCurve bigInCurve;
        [SerializeField] Circle theSmallOne;
        [SerializeField] AnimationCurve smallOutCurve;
        [SerializeField] AnimationCurve smallInCurve;

        Coroutine smallCoroutine, bigCoroutine;

        [Header("Fields of the button calling the droplet button")]
        [SerializeField] AnimationCurve callButtonShowCurve;
        [SerializeField] AnimationCurve callButtonHideCurve;
        [SerializeField] float thisButtonShowhideTimeLength = 1;
        [SerializeField] bool isLeft;
        Coroutine showhideCoroutine;
        Vector2 posHidden, posShown;

        [Header("Fields to edit the position where the droplet button will stop")]
        public bool editTarget;
        public Vector2 target;
        private GameObject _targetSetterGameObject;

        enum State
        {
            Show,
            Hide,
        }

        private void Start()
        {
            initialPositionSmall = theSmallOne.position;
            initialPositionBig = theBigOne.position;
            posShown = (transform as RectTransform).anchoredPosition;
            posHidden = posShown + (isLeft ? 1 : -1) * Vector2.right * -(transform as RectTransform).sizeDelta.x;
        }

        private void Update()
        {

            if (editTarget)
            {
                if (!_targetSetterGameObject)
                {
                    _targetSetterGameObject = new GameObject("Target Setter", typeof(RectTransform));
                    _targetSetterGameObject.transform.SetParent(transform.GetComponentInParent<Canvas>().transform, false);
                    (_targetSetterGameObject.transform as RectTransform).anchoredPosition = target;
                }

                target = (_targetSetterGameObject.transform as RectTransform).anchoredPosition;
            }
            else
            {
                if (_targetSetterGameObject)
                    DestroyImmediate(_targetSetterGameObject);
            }
        }

        public void HideCallAndShowDroplet()
        {
            if (showhideCoroutine != null)
                StopCoroutine(showhideCoroutine);
            showhideCoroutine = StartCoroutine(CallCoroutine(State.Hide));

            if (smallCoroutine != null)
                StopCoroutine(smallCoroutine);
            smallCoroutine = StartCoroutine(SmallCoroutine(State.Show));
            if (bigCoroutine != null)
                StopCoroutine(bigCoroutine);
            bigCoroutine = StartCoroutine(BigCoroutine(State.Show));
        }

        public void ShowCallAndHideDroplet()
        {
            if (showhideCoroutine != null)
                StopCoroutine(showhideCoroutine);
            showhideCoroutine = StartCoroutine(CallCoroutine(State.Show));

            if (smallCoroutine != null)
                StopCoroutine(smallCoroutine);
            smallCoroutine = StartCoroutine(SmallCoroutine(State.Hide));
            if (bigCoroutine != null)
                StopCoroutine(bigCoroutine);
            bigCoroutine = StartCoroutine(BigCoroutine(State.Hide));
        }

        IEnumerator SmallCoroutine(State state)
        {
            RectTransform rt = theSmallOne.transform as RectTransform;
            float t = 0;
            Vector2 toPosition =
                ( Useful.Calculation.Similar(rt.anchoredPosition, target, 0.01f)
                || Useful.Calculation.Similar(rt.anchoredPosition, initialPositionSmall, 0.01f) )
                ? target
                : rt.anchoredPosition;
            while (t <= 1)
            {
                t += Time.deltaTime / playTimeLength;
                rt.anchoredPosition = Vector2.Lerp(
                    initialPositionSmall,
                    toPosition,
                    state == State.Show
                    ? smallOutCurve.Evaluate(t)
                    : smallInCurve.Evaluate(t)
                );
                yield return null;
            }
        }

        IEnumerator BigCoroutine(State state)
        {
            RectTransform rt = theBigOne.transform as RectTransform;
            float t = 0;
            Vector2 toPosition =
                ( Useful.Calculation.Similar(rt.anchoredPosition, target, 0.01f)
                || Useful.Calculation.Similar(rt.anchoredPosition, initialPositionBig, 0.01f) )
                ? target
                : rt.anchoredPosition;
            while (t <= 1)
            {
                t += Time.deltaTime / playTimeLength;
                rt.anchoredPosition = Vector2.Lerp(
                    initialPositionBig,
                    toPosition,
                    state == State.Show
                    ? bigOutCurve.Evaluate(t)
                    : bigInCurve.Evaluate(t)
                );
                yield return null;
            }
        }

        IEnumerator CallCoroutine(State state)
        {
            RectTransform rt = transform as RectTransform;
            Vector2 fromPosition = rt.anchoredPosition;
            Vector2 toPosition = state == State.Show ? posShown : posHidden;
            float t = 0;
            while (t <= 1)
            {
                t += Time.deltaTime / thisButtonShowhideTimeLength;
                rt.anchoredPosition = Vector2.Lerp(
                    fromPosition,
                    toPosition,
                    state == State.Hide
                    ? callButtonHideCurve.Evaluate(t)
                    : callButtonShowCurve.Evaluate(t)
                );
                yield return null;
            }
            showhideCoroutine = null;
        }
    }
}