using UnityEngine;
using System.Collections;

public class WorldGen : MonoBehaviour {
	public Vector3 gridSize = new Vector3(1500f, 200f, 1500f);
	
	public GameObject[] set1;
	public GameObject[] set2;
	
	public int numSet1 = 1;
	public int numSet2 = 2;
	
	
	public void Build() {		
		GameObject root = GameObject.Find("root");
		if (root != null) {
			DestroyImmediate(root);
		}
		root = new GameObject("root");
		
		for (float x = -PlayerPilot.maxRadius; x < PlayerPilot.maxRadius; x += gridSize.x) {
			for (float y = 0f; y < 300f; y += gridSize.y) {
				for (float z = -PlayerPilot.maxRadius; z < PlayerPilot.maxRadius; z += gridSize.z) {
					
					Vector3 min = new Vector3(x, y, z);
					Vector3 max = min + gridSize;
					
					Spawn("set1", root, set1, numSet1, min, max);
					Spawn("set2", root, set2, numSet2, min, max);
				}
			}
		}
	}
	
	void Spawn(string s, GameObject root, GameObject[] prefabs, int count, Vector3 min, Vector3 max) {
		if (prefabs == null) return;
		if (count < 1) return;
		
		for (int i=0; i<count; i++) {
			int idx = Random.Range(0, prefabs.Length);
			GameObject prefab = prefabs[idx];
			
			Vector3 pos = min;
			pos.x = Random.Range(min.x, max.x);
			pos.y = Random.Range(min.y, max.y);
			pos.z = Random.Range(min.z, max.z);
			
			GameObject clone = root.InstantiateChild(prefab, pos, Quaternion.identity);
			clone.name = System.Text.RegularExpressions.Regex.Replace(clone.name, @"\s*\(Clone\)", "");
		}
	}
}
