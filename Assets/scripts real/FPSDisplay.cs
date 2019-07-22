using UnityEngine;
using System.Collections;
 
public class FPSDisplay : MonoBehaviour
{
	float deltaTime = 0.0f;

    [Range(0f, 1f)]
    public float opaque =1f;

    [Range(0f, 1f)]
    public float R = 0f;

    [Range(0f, 1f)]
    public float G = 0f;

    [Range(0f, 1f)]
    public float B = 0f;

 
	void Update()
	{
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
	}
 
	void OnGUI()
	{
		int w = Screen.width, h = Screen.height;
 
		GUIStyle style = new GUIStyle();
 
		Rect rect = new Rect(0, 0, w, h * 2 / 100);
		style.alignment = TextAnchor.UpperLeft;
		style.fontSize = h * 2 / 100;
		style.normal.textColor = new Color (R, G, B, opaque);
		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
		GUI.Label(rect, text, style);
	}
}
