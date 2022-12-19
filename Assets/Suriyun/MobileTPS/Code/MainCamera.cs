using UnityEngine;

namespace Suriyun.MobileTPS
{
	public class MainCamera : MonoBehaviour
	{
	
		public Transform cam_holder;
		private Transform trans;

		private void Start ()
		{
			trans = transform;
		}

		private void Update ()
		{
			// Smmoth out camera transition //
			trans.position = Vector3.Lerp (trans.position, cam_holder.position, 60f * Time.deltaTime);
		}
	}
}
