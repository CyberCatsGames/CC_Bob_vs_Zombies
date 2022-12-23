using UnityEngine;

public class NormalAdOnEnable : MonoBehaviour
{
    private bool isStart = true;
    private void Start() {
        if (MyYandex.Instance) {
            isStart = false;
            MyYandex.Instance.ShowNormalAD_Intern();
        }
    }

    private void OnEnable() {
        if (MyYandex.Instance && isStart == false) {
            MyYandex.Instance.ShowNormalAD_Intern();
        }
    }
}
