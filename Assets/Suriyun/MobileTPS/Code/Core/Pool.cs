using System;
using System.Collections.Generic;
using UnityEngine;

namespace Suriyun.MobileTPS
{
    public class Pool<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private T _prefab;
        [Space(10)] [SerializeField] private string _poolName = "Pool";
        [SerializeField] private int _minCapacity;
        [SerializeField] private int _maxCapacity;
        [Space(10)] [SerializeField] private bool _autoExpand;

        protected List<T> PoolContainer;
        private Transform _container;

        private void OnValidate()
        {
            if (_autoExpand == true)
            {
                _maxCapacity = int.MaxValue;
            }
        }

        private void Awake()
        {
            CreatePool();
        }

        public T GetFreeElement(Vector3 position, Quaternion rotation, bool isActive = true)
        {
            var element = GetFreeElement(position, isActive);
            element.transform.rotation = rotation;
            return element;
        }

        public T GetFreeElement(Transform point, bool isActive = true)
        {
            var element = GetFreeElement(isActive);
            element.transform.position = point.position;
            element.transform.forward = point.forward;
            return element;
        }

        public T GetFreeElement(Vector3 position, bool isActive = true)
        {
            var element = GetFreeElement(isActive);
            element.transform.position = position;
            return element;
        }

        public T GetFreeElement(bool isActive = true)
        {
            if (TryGetFreeElement(out var element, isActive))
            {
                return element;
            }

            if (_autoExpand == true)
            {
                return CreateElement(isActive);
            }

            if (PoolContainer.Count < _maxCapacity)
            {
                return CreateElement(isActive);
            }

            throw new Exception("Pool is over!");
        }

        private void CreatePool()
        {
            PoolContainer = new List<T>(_minCapacity);
            _container = new GameObject(_poolName).transform;

            for (int i = 0; i < _minCapacity; i++)
            {
                CreateElement();
            }
        }

        private T CreateElement(bool isActiveByDefault = false)
        {
            var createdObject = Instantiate(_prefab, _container);
            createdObject.gameObject.SetActive(isActiveByDefault);
            PoolContainer.Add(createdObject);
            return createdObject;
        }

        private bool TryGetFreeElement(out T element, bool isActive = true)
        {
            foreach (var item in PoolContainer)
            {
                if (item != null && item.gameObject.activeInHierarchy == false)
                {
                    element = item;
                    element.gameObject.SetActive(isActive);
                    return true;
                }
            }

            element = null;
            return false;
        }
    }
}