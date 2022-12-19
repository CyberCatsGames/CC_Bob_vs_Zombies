using UnityEngine;
using TouchScript.Gestures;

namespace Suriyun.MobileTPS
{
	public class TouchScriptButton : Button
	{
        protected Material m;

        private PressGesture press;
        private ReleaseGesture release;

		protected virtual void Awake ()
		{
            m = GetComponent<SpriteRenderer>().material;
            press = GetComponent<PressGesture> ();
			release = GetComponent<ReleaseGesture> ();
		}

		private void OnEnable ()
		{
			press.Pressed += Press_Pressed;
			release.Released += Release_Released;
		}

		private void OnDisable ()
		{
			press.Pressed -= Press_Pressed;
			release.Released -= Release_Released;
		}

		protected virtual void Press_Pressed (object sender, System.EventArgs e)
		{
			m.color = Color.grey;
			pressed = true;
		}

		protected virtual void Release_Released (object sender, System.EventArgs e)
		{
			m.color = Color.white;
			pressed = false;
		}

	}
}
