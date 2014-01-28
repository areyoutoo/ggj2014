using UnityEngine;
using System.Collections;

public class MenuUI : MonoBehaviour {
	void OnGUI()
	{
		GUILayout.BeginHorizontal();
		GUILayout.Space(40f);
		GUILayout.BeginVertical();
		GUILayout.Space(40f);
		
		GUI.contentColor = Color.black;
		GUILayout.Label("Flying Over the Influence");
		
		//GUI.contentColor = Color.white;
		GUILayout.Label("");
		GUILayout.Label("a quick and dirty flight sim");
		GUILayout.Label("by Matt Shouse and Robert Utter");
		GUILayout.Label("");
		GUILayout.Label("Special thanks to CG Textures");
		GUILayout.Label("");
		GUILayout.Label("");
		
		GUI.contentColor = Color.white;
		if (GUILayout.Button("Play!")) {
		        LevelManager.Load("Valley_Level");
		}
		
#if !UNITY_WEBPLAYER
		if (GUILayout.Button("Quit :(")) {
		        Application.Quit();
		}
#endif
		
		GUILayout.EndVertical();
		
		GUILayout.Space(60f);
		
		GUILayout.BeginVertical();
		
		GUILayout.Space(90f);
		GUI.contentColor = Color.Lerp(Color.black, Color.gray, 0.5f);
		GUILayout.Label("CONTROLS");
		GUILayout.Label("");
		GUILayout.Label("Yaw: W/S");
		GUILayout.Label("Roll: A/D");
		GUILayout.Label("Flat Turn: Q/E");
		GUILayout.Label("");
		GUILayout.Label("Throttle: -/+");
		GUILayout.Label("Shoot: hold Mouse1");
		GUILayout.Label("");
		GUILayout.Label("");
		GUILayout.Label("Hint: press space to drink");
		
		GUILayout.EndVertical();
		
		GUILayout.EndHorizontal();
	}
}
