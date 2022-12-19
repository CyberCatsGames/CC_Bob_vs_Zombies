using TMPro;
using UnityEngine;

namespace Suriyun.MobileTPS
{
    [RequireComponent(typeof(TMP_Text))]
    public class LocalizeText : MonoBehaviour
    {
        [SerializeField] private string _key;
        [SerializeField] private bool _isCapitalize;

        private TMP_Text _valueTMP;

        private void Awake()
        {
            _valueTMP = GetComponent<TMP_Text>();
            LocalizationManager.Instance.OnLocaleChanged += OnLocaleChanged;
            Localize();
        }

        private void OnDestroy()
        {
            LocalizationManager.Instance.OnLocaleChanged -= OnLocaleChanged;
        }

        private void OnLocaleChanged()
        {
            Localize();
        }

        private void Localize()
        {
            var localization = LocalizationManager.Instance.GetLocalization(_key);
            _valueTMP.text = _isCapitalize == true ? localization.ToUpper() : localization;
        }
    }
}