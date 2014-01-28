using UnityEngine;
using System.Collections;

public class Pilot : MonoBehaviour {
	public float inebriation { get; protected set; }
	
	public float inebriationMulti {
		get {
			return (1f + inebriation);
		}
	}
	
	protected Plane plane;
	
	float seconds = 60f;
	
	string message;
	float messageTimeLeft;
	Color messageColor;
	
	
	
	
	public float GetInebriationMulti(float ratio) {
		return (1f + inebriation * ratio);
	}
	
	protected virtual void Start() {
		Score.Reset();
		plane = GetComponent<Plane>();
		InvokeRepeating("Tick", 0.01f, 1f);
		
		gameObject.AddComponent<EscToMenu>();
		
		StartCoroutine(Tutorial());
	}
	
	protected virtual void Update() {
	}
	
	const float responsiveness = 4f;
	
	protected void FlatTurn(float rate) {
		rigidbody.AddTorque(rate * transform.up * Time.deltaTime * 10f * responsiveness);
	}
	
	protected void Roll(float rate) {
		rigidbody.AddTorque(rate * transform.forward * Time.deltaTime * -15f * responsiveness);
	}
	
	protected void Yaw(float rate) {
		rigidbody.AddTorque(rate * transform.right * Time.deltaTime * -10f * responsiveness);
	}
	
	protected void Throttle(float rate) {
		float currentTarget = plane.targetSpeed;
		rate *= GetInebriationMulti(2f);
		plane.SetTargetSpeed(currentTarget + rate);
	}
	
	protected void StartShooting() {
		InvokeRepeating("Shoot", 0.001f, 0.25f);
	}
	
	protected void StopShooting() {
		CancelInvoke("Shoot");
	}
	
	protected void Shoot() {
		plane.Shoot(inebriation);
	}
	
	
	
	
	
	public void OnCollisionEnter(Collision collision) {
		GameObject other = collision.gameObject;
		
		Debug.Log("oce" + collision.collider.name);
		
		if (other.name == "GroundCol") {
			plane.Explode();
			Score.endMessage = "You had an accident.";
			Lose();
		}
		
		OnTriggerEnter(collision.collider);
		
		
	}
	
	public void OnTriggerEnter(Collider coll) {
		GameObject other = coll.gameObject;
		
		Debug.Log("ote" + other.name);
		
		Color c = Color.black;
		
		if (other.name == "Balloon_Shoot_Drink") {
			Score.multiplier += 0.5f;
			Score.GetPoints(10f);
			Drink();
			FlashMessage("Drink up!", 1f, c);
		}
		
		if (other.name == "Balloon_Shoot_Time") {
			Score.GetPoints(10f);
			seconds += 15;
			FlashMessage("Time up!", 1f, c);
		}
		
		if (other.name == "Ring_Easy") {
			Score.GetPoints(100f);
			FlashMessage("Easy ring, 100 points!", 1f, c);
		}
		
		if (other.name == "Ring_Medium") {
			Score.GetPoints(200f);
			FlashMessage("Medium ring, 200 points!", 1f, c);
		}
		
		if (other.name == "Ring_Hard") {
			Score.GetPoints(300f);
			FlashMessage("Hard ring, 300 points!", 1f, c);
		}
		
		Debug.Log("Destroying: " + other.name);
		Destroy(other);
	}
	
	
	
	
	protected void Tick() {
		seconds -= 1f;
		Score.totalSeconds += 1f;
		if (seconds < 0f) {
			Lose();
		}
	}
	
	
	protected void Lose() {
		StopShooting();
		enabled  = false;
		gameObject.AddComponent<Scoreboard>();
	}
	
	
	protected void OnGUI() {
		GUILayout.BeginHorizontal();
		GUILayout.Space(40f);
		GUILayout.BeginVertical();
		GUILayout.Space(40f);
		
		GUI.contentColor = Color.black;
		GUILayout.Label(string.Format("Time: {0}", seconds.ToString("n0")));
		GUILayout.Label(string.Format("Score: {0}", Score.points.ToString("n0")));
		GUILayout.Label(string.Format("Multi: {0}x", Score.multiplier.ToString("n1")));
		
		
		
		GUILayout.Space(100f);
		if (messageTimeLeft > 0f) {
			messageTimeLeft -= Time.deltaTime;
			
			float fade = 0.3f;
			Color c = messageColor;
			if (messageTimeLeft < fade) {
				float t = messageTimeLeft / fade;
				Color clear = messageColor;
				clear.a = 0f;
				c = Color.Lerp(clear, messageColor, t);
			}
						
			GUI.contentColor = c;
			GUILayout.Label(message);
		}
		GUILayout.EndVertical();
		GUILayout.EndHorizontal();
	}
	
	
	
	public void Drink() {
		inebriation += 1f;
	}
	
	
	
	public void FlashMessage(string text, float duration, Color c) {
		message = text;
		messageTimeLeft = duration;
		messageColor = c;
		
		Debug.Log(text);
	}
	
	IEnumerator Tutorial() {
		float delay = 3f;
		Color color = Color.black;
		
		string[] messages = new string[]{
			"Use WS, AD, and QE to steer the plane",
			"Use -/+ to control your throttle",
			"Shoot by holding left-click",
			"Try to collect balloons!",
		};
		
		foreach (string msg in messages) {
			FlashMessage(msg, delay, color);
			yield return new WaitForSeconds(delay);
		}
	}
}
