using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public Pilot pilot;
	
	protected void OnCollisionEnter(Collision collision) {
		GameObject other = collision.gameObject;
		
		if (other.name != "GroundCol" && !other.name.StartsWith("Ring")) {
			pilot.OnCollisionEnter(collision);
		}
		
		Destroy(gameObject);
	}
	
	protected void OnTriggerEnter(Collider col) {
		GameObject other = collider.gameObject;
		if (!other.name.StartsWith("Ring")) {
			pilot.OnTriggerEnter(col);
		}
	}
}
