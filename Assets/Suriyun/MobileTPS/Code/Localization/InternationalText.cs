using TMPro;
using UnityEngine;

namespace Suriyun.MobileTPS {
    public class InternationalText : MonoBehaviour {
        [SerializeField] string _en;
        [SerializeField] string _ru;
        void Start() {
            if (Language.Instance.CurrentLanguage == "en") {
                GetComponent<TextMeshProUGUI>().text = _en;
            } else if (Language.Instance.CurrentLanguage == "ru") {
                GetComponent<TextMeshProUGUI>().text = _ru;
            } else {
                GetComponent<TextMeshProUGUI>().text = _en;
            }
        }


    }
}
