using UnityEngine;

namespace Suriyun.MobileTPS
{
    public class TrajectoryRenderer : MonoBehaviour
    {
        private LineRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<LineRenderer>();
        }

        public void ShowTrajectory(Vector3 origin, Vector3 speed)
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
}