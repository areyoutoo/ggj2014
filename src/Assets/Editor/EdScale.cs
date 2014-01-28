using UnityEngine;
using UnityEditor;
using System.Collections;

public class EdScale : MonoBehaviour {
	[MenuItem("GameJam/Scale Randomly")]
	static void ScaleRandomly() {
		Undo.RegisterSceneUndo("Scale randomly");
		foreach (Transform t in GetTransforms()) {
			float min = 0.75f;
			float max = 1.25f;
			t.localScale *= Random.Range(min, max);
		}
	}
	
	[MenuItem("GameJam/Rotate randomly")]
	static void RotateRandomly() {
		Undo.RegisterSceneUndo("Rotate randomly");
		foreach (Transform t in GetTransforms()) {
			Vector3 euler = t.eulerAngles;
			euler.y = Random.Range(0f, 360f);
			t.eulerAngles = euler;
		}
	}
	
	static Transform[] GetTransforms() {
		//return (Transform[])Selection.GetFiltered(typeof(Transform), SelectionMode.TopLevel);
		return Selection.transforms;
	}
}
