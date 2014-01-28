using UnityEngine;
using System.Collections;

public class Scoreboard : MonoBehaviour {
	void OnGUI() {
		GUILayout.BeginHorizontal();
		GUILayout.Space(40f);
		
		GUILayout.BeginVertical();
		GUILayout.Space(40f);
		
		GUI.contentColor = Color.red;
		GUILayout.Label("Game Over!");
		
		GUI.contentColor = Color.gray;
		GUILayout.Label(Score.endMessage);
		GUILayout.Label("");
		
		
		GUI.contentColor = Color.cyan;		
		string s = string.Format("You earned {0} points in {1} seconds.", Score.points.ToString("n0"), Score.points.ToString("n0"));
		GUILayout.Label(s);
		
		GUILayout.EndVertical();
		GUILayout.EndHorizontal();
	}
}
