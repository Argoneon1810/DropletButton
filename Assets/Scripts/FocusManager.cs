using UnityEngine;

namespace DropletButton.Management
{
    public class FocusManager : MonoBehaviour
    {
        Focusable itemToFocus;

        public void Focus(Focusable itemToFocus)
        {
            if (this.itemToFocus != itemToFocus)
            {
                if (this.itemToFocus)
                    this.itemToFocus.OnLostFocus();
                itemToFocus.OnGainFocus();
                this.itemToFocus = itemToFocus;
            }
        }
    }

}