using UnityEngine;

namespace Suriyun.MobileTPS
{
    public class TrajectoryRenderer : MonoBehaviour
    {
        [SerializeField] private TrajectoryType _type;

        private LineRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<LineRenderer>();
        }

        public void ShowTrajectory(Vector3 origin, Vector3 speed)
        {
            if (_type == TrajectoryType.Slanting)
                DrawSlanting(origin, speed);
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

        private void DrawSlanting(Vector3 origin, Vector3 speed)
        {
            var points = new Vector3[50];
            _renderer.positionCount = points.Length;

            for (var i = 0; i < points.Length; i++)
            {
                float time = i * 0.1f;
                points[i] = origin + speed * time + Physics.gravity * (time * time) / 2f;
            }

            _renderer.SetPositions(points);
        }
    }

    internal enum TrajectoryType
    {
        Slanting,
        Direct
    }
}