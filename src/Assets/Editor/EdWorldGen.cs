using UnityEngine;
using UnityEditor;
using System.Collections;

public class EdWorldGen : Editor {
	[MenuItem("GameJam/Build scene")]
	static void BuildScene() {
		Undo.RegisterSceneUndo("Build scene");
		foreach (var o in (WorldGen[])Object.FindObjectsOfType(typeof(WorldGen))) {
			o.Build();
		}
	}
}
