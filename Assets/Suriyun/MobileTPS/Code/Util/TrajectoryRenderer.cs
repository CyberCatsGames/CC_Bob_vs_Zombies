using System.Collections.Generic;
using UnityEngine;

namespace Suriyun.MobileTPS
{
    public class TrajectoryRenderer : MonoBehaviour
    {
        private const int MaxPoints = 50;
        [SerializeField] private TrajectoryType _type;
        [SerializeField] private LayerMask _targetLayers;

        private LineRenderer _renderer;
        [SerializeField] private List<Vector3> _positions = new();

        private void Awake()
        {
            _renderer = GetComponent<LineRenderer>();
        }

        public void ShowTrajectory(Vector3 origin, Vector3 speed)
        {
            if (_type == TrajectoryType.Slanting)
                DrawSlanting(origin, speed);
        }

        private void DrawSlanting(Vector3 origin, Vector3 speed)
        {
            _positions.Clear();

            for (int i = 0; i < MaxPoints; i++)
            {
                float time = i * 0.1f;
                var position = origin + speed * time + Physics.gravity * (time * time) / 2f;

                if (Check(position) == true)
                {
                    break;
                }

                _positions.Add(position);
            }

            _renderer.positionCount = _positions.Count;
            _renderer.SetPositions(_positions.ToArray());
        }

        public void ShowTrajectory(Transform point, float distance)
        {
            if (_type == TrajectoryType.Direct)
            {
                DrawDirect(point, distance);
            }
        }

        private void DrawDirect(Transform point, float distance)
        {
            _renderer.positionCount = 2;
            _renderer.SetPosition(0, point.position);
            _renderer.SetPosition(1, point.position + point.forward * distance);
        }

        private bool Check(Vector3 position)
        {
            return Physics.CheckSphere(position, 0.4f, _targetLayers);
        }
    }

    internal enum TrajectoryType
    {
        Slanting,
        Direct
    }
}