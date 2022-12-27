using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileElement : MonoBehaviour {

    private void Start() {
        Invoke(nameof(TurnOn), 1);
    }

    private void TurnOn() {
        if (MyYandex.Instance.IsMobile) {
            GetComponent<CanvasGroup>().alpha = 1;
        }
    }
}
