using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Plane : MonoBehaviour {
	public bool isStalling {
		get {
			return rigidbody.useGravity;
		}
		protected set {
			rigidbody.useGravity = value;
		}
	}
	
	public float targetSpeed { get; protected set; }
	public float speed { get; protected set; }
	
	public Pilot pilot { get; protected set; }
	
	bool shootLeft = true;
	
	protected void Start() {
		speed = 25f;
		pilot = GetComponent<Pilot>();
	}
	
	protected void Update() {
		if (speed != targetSpeed) {
			float diff = targetSpeed - speed;
			float deltaSpeed = Mathf.Clamp(diff, 0f, 1f);
			speed += deltaSpeed;
		}
		
		
		
//		if (Input.GetKeyDown(KeyCode.P)) {
//			Explode();
//		}
	}
	
	protected void FixedUpdate() {
		rigidbody.velocity = transform.forward.WithLength(speed * 3f);
	}
	
	public void SetTargetSpeed(float target) {
		targetSpeed = Mathf.Clamp(target, 10f, 40f);
	}
	
	public void Shoot(float error) {
		Vector3 origin = transform.TransformPoint(5f * (shootLeft ? Vector3.left : Vector3.right));
		shootLeft = !shootLeft;
		
		GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		go.transform.position = origin;
		go.layer = LayerMask.NameToLayer("Bullet");
		
		//TODO
		if (collider != null) {
			Physics.IgnoreCollision(collider, go.collider);
		}
		
		Rigidbody rb = go.AddComponent<Rigidbody>();
		rb.AddForce(transform.forward.WithLength(300f), ForceMode.VelocityChange);
		
		go.AddComponent<ParticleSystem>();
		
		Bullet bullet = go.AddComponent<Bullet>();
		bullet.pilot = pilot;
		
		Destroy(go, 20f);
	}
	
	public void Explode() {
		rigidbody.velocity = Vector3.zero;
		enabled = false;
		
		for (int i=0; i<3; i++) {
			Transform t = new GameObject("ps").transform;
			transform.AttachChild(t);
			var ps = t.gameObject.AddComponent<ParticleSystem>();
			ps.startColor = i < 2 ? Color.red : Color.yellow;
			ps.transform.rotation = Quaternion.LookRotation(Vector3.up);
			ps.startSpeed = Random.Range(15f, 20f);
			ps.startSize = Random.Range(3f, 5f);
			t.position = gameObject.GetRendererBounds().center;
		}
	}
}
