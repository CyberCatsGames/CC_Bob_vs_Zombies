﻿using UnityEngine;

namespace Suriyun.MobileTPS
{
    public class GunSwitcher : MonoBehaviour
    {
        [SerializeField] private Gun[] _guns;

        private int _currentGun;

        public Gun CurrentGun => _guns[_currentGun];

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

        private void SelectGun()
        {
            for (var i = 0; i < _guns.Length; i++)
            {
                _guns[i].gameObject.SetActive(i == _currentGun);
            }
        }
    }
}