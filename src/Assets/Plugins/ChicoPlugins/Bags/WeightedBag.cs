﻿using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Bag returns item with weighted randomness. Items are NOT removed.
/// </summary>
public class WeightedBag<T> : AbstractBag<T> {
	const float DEFAULT_WEIGHT = 1f;
	
	float totalWeight;
	List<WeightedLink<T>> links;
	
	public override int count {
		get {
			return links.Count;
		}
	}
	
	public override T GetNext() {
		if (links.Count > 0) {
			float goal = Random.Range(0f, totalWeight);
			float progress = 0f;
			for (int i=0; i<links.Count; i++) {
				progress += links[i].weight;
				if (progress >= goal) {
					return links[i].target;
				}
			}
			Debug.LogWarning("WeightedRandom passed end of list!");
			return links[0].target;
		} else {
			string msg = string.Format("{0} of type {1} is empty!", this.GetType().ToString(), typeof(T).ToString());
			Debug.LogWarning(msg);
			return default(T);
		}
	}
	
	///<summary>
	///Adds item with requested weight.
	///<summary> 
	///<remarks>
	///Weights are NOT normalized. Recommend against particularly large (>1,000,000) or small (<0.001) weights.
	///</remarks>
	public void Add(T item, float weight) {
		Add(new WeightedLink<T>(item, weight));
	}
	
	///<summary>
	///Implementation for Add().
	///<summary> 		
	public virtual void Add(WeightedLink<T> link) {
		if (link == null) throw new System.ArgumentNullException("link");
		
		totalWeight += link.weight;
		links.Add(link);
	}
	
	public WeightedBag() {
		links = new List<WeightedLink<T>>();
	}
}
