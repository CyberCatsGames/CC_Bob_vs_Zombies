using System;
using UnityEngine;

namespace Suriyun.MobileTPS
{
    public class GunSwitcher : MonoBehaviour
    {
        [SerializeField] private Gun[] _guns;

        private int _currentGun;
        private GunSwitcherView _view;

        public Gun CurrentGun => _guns[_currentGun];

        private void Start()
        {
            _view = FindObjectOfType<GunSwitcherView>();
        }

        private void Update()
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if (_currentGun >= _guns.Length - 1)
                {
                    _currentGun = 0;
                }
                else
                {
                    _currentGun++;
                }

                SelectGun();
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (_currentGun <= 0)
                {
                    _currentGun = _guns.Length - 1;
                }
                else
                {
                    _currentGun--;
                }

                SelectGun();
            }
        }

        public void SelectGun(int currentGun = -1)
        {
            if (currentGun != -1)
                _currentGun = currentGun;

            for (var i = 0; i < _guns.Length; i++)
            {
                _guns[i].gameObject.SetActive(i == _currentGun);
            }

            _view.SelectGun(_currentGun);
        }
    }
}