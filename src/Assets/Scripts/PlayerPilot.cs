using UnityEngine;
using System.Collections;

public class PlayerPilot : Pilot {
	const float YAW_RATE = 1f;
	const float ROLL_RATE = 1f;
	const float TURN_RATE = 1f;
	
	public const float maxRadius = 4500f;
	
	Vector3 startPos;
	
	protected override void Start() {
		base.Start();
		startPos = transform.position;
	}
	
	protected override void Update() {
		base.Update();
		
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
			Yaw(-YAW_RATE);
		} else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
			Yaw(YAW_RATE);
		}
		
		if (Input.GetKey(KeyCode.A)) {
			Roll(-ROLL_RATE);
		} else if (Input.GetKey(KeyCode.D)) {
			Roll(ROLL_RATE);
		}
		
		if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow)) {
			FlatTurn(-TURN_RATE);
		} else if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.RightArrow)) {
			FlatTurn(TURN_RATE);
		}
		
		if (Input.GetKeyDown(KeyCode.Equals)) {
			Throttle(-5f);
		} else if (Input.GetKeyDown(KeyCode.KeypadMinus)) {
			Throttle(5f);
		}
		
		if (Input.GetKeyDown(KeyCode.Space)) {
			inebriation += 1f;
		}
		
		if (Input.GetMouseButtonDown(0)) {
			StartShooting();
		}
		if (Input.GetMouseButtonUp(0)) {
			StopShooting();
		}
		
		float dist = Vector3.Distance(startPos, transform.position);
		if (dist > maxRadius) {
			Score.endMessage = "You flew out of bounds";
			Lose();
		} else if (dist > maxRadius * 0.9f) {
			FlashMessage("You are leaving the map; turn back!", 1f, Color.red);
		}
	}
}
