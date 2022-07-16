using UnityEngine;
using Toast;

public class TestButton : MonoBehaviour
{
    public void OnClick()
    {
        ToastManager.instance.MakeText("Horey!", 3).Show();
    }
}
