using UnityEngine;

namespace Suriyun.MobileTPS
{
    public class GunSwitcherView : MonoBehaviour
    {
        [SerializeField] private UnityEngine.UI.Button _left;
        [SerializeField] private UnityEngine.UI.Button _right;

        private GunView[] _gunViews;
        private int _currentGunIndex;

        private void Awake()
        {
            _gunViews = GetComponentsInChildren<GunView>();
            SelectGun();
        }

        private void OnEnable()
        {
            _left.onClick.AddListener(OnLeftClick);
            _right.onClick.AddListener(OnRightClick);
        }

        private void OnDisable()
        {
            _left.onClick.RemoveListener(OnLeftClick);
            _right.onClick.RemoveListener(OnRightClick);
        }

        private void OnRightClick()
        {
            _currentGunIndex++;

            if (_currentGunIndex >= _gunViews.Length)
                _currentGunIndex = 0;

            SelectGun();
            Game.instance.GunSwitcher.SelectGun(_currentGunIndex);
        }

        private void OnLeftClick()
        {
            _currentGunIndex--;

            if (_currentGunIndex < 0)
                _currentGunIndex = _gunViews.Length - 1;

            SelectGun();
            Game.instance.GunSwitcher.SelectGun(_currentGunIndex);
        }

        private void SelectGun()
        {
            for (var i = 0; i < _gunViews.Length; i++)
            {
                _gunViews[i].gameObject.SetActive(i == _currentGunIndex);
            }
        }
    }
}