using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FPSUpdater : MonoBehaviour
{

    public Text text;
    public int fps;

    private void Start()
    {
        text = GetComponent<Text>();
        StartCoroutine(UpdateFPS());
    }

    private void Update()
    {
        fps = (int)(1f / Time.deltaTime);
    }

    private IEnumerator UpdateFPS()
    {
        while (true)
        {
            text.text = "" + fps + " FPS";
            yield return new WaitForSeconds(0.16f);
        }
    }

}
