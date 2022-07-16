using UnityEngine;
using UnityEngine.Events;

namespace DropletButton.Management
{
    public class Focusable : MonoBehaviour
    {
        public UnityEvent LostFocusEvents;
        public UnityEvent GainFocusEvents;

        public void OnGainFocus()
        {
            GainFocusEvents?.Invoke();
        }

        public void OnLostFocus()
        {
            LostFocusEvents?.Invoke();
        }
    }

}