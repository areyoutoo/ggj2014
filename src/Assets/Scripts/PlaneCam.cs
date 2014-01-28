using UnityEngine;
using System.Collections;

public class PlaneCam : MonoBehaviour {
	protected Plane plane;
	protected Pilot pilot;
	
	Vector3 velocity;
	
	protected void Start() {
		plane = GameObject.Find("Player").GetComponent<Plane>();
		pilot = GameObject.Find("Player").GetComponent<Pilot>();
	}
	
	protected void FixedUpdate() {
		float multi = pilot.GetInebriationMulti(0.5f);
		
		Vector3 offset = Vector3.zero;
		offset += Vector3.right * (Mathf.Cos(Time.time * 0.6f) * multi);
		offset += Vector3.up * (Mathf.Sin(Time.time * 0.4f) * multi);
		offset += Vector3.forward * (Mathf.Sin(Time.time * 0.1f + 0.4f) * 0.2f * multi);
		offset = plane.transform.TransformPoint(offset * 0.2f);
		
		Vector3 baseOffset = plane.transform.TransformDirection(Vector3.forward * -30f + Vector3.up);
		
		Vector3 targetVelocity = plane.rigidbody.velocity;
		if (velocity != targetVelocity) {
			Vector3 diff = targetVelocity - velocity;
			Vector3 acc = Vector3.ClampMagnitude(diff, 0.0001f * Time.deltaTime);
			velocity += acc;
		}
		
		Vector3 target = plane.transform.position + velocity;
		Vector3 pos = baseOffset + offset - Vector3.ClampMagnitude(velocity, 10f);
		
		transform.position = pos;
		transform.LookAt(target);
	}
}
