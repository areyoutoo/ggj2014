using UnityEngine;
using System.Collections;

public class Spinner : MonoBehaviour {

	public Vector3 spin = Vector3.up;
	public float degPerSecond = 90f;
	
	void Update () {
		transform.Rotate(spin.normalized * Time.deltaTime * degPerSecond);
		
	}
}
