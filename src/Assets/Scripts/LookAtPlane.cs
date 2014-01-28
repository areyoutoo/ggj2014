using UnityEngine;
using System.Collections;

public class LookAtPlane : MonoBehaviour {
	
	Plane plane;

	// Use this for initialization
	void Start () {
		plane = (Plane)GameObject.FindObjectOfType(typeof(Plane));
	}
	
	// Update is called once per frame
	void Update () {
		if (plane != null) {
			transform.LookAt(plane.transform.position);
		} else {
			Debug.Log("Something bad happened");
			Debug.Break();
		}
	}
}
