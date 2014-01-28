using UnityEngine;
using System.Collections;

public static class Score {
	public static float totalSeconds;
	public static float points;
	public static float multiplier;
	
	public static string endMessage;
	
	public static void Reset() {
		totalSeconds = 0f;
		points = 0f;
		multiplier = 1f;
	}
	
	public static void GetPoints(float amount) {
		points += amount * multiplier;
	}
}
