﻿using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Bag returns items in random order.
/// </summary>
public class RandomBag<T> : SimpleBag<T> {
	protected List<T> members;
	
	public override int count {
		get {
			return members.Count;
		}
	}

	///<summary>
	///Returns a random item. Returns default(T) if empty.
	///</summary>	
	public override T GetNext() {
		if (members.Count > 0) {
			int i = Random.Range(0, members.Count);
			T item = members[i];
			return item;
		} else {
			string msg = string.Format("{0} of type {1} is empty!", this.GetType().ToString(), typeof(T).ToString());
			Debug.LogWarning(msg);
			return default(T);
		}
	}

	public override void Add(T item) {
		members.Add(item);
	}

	public override bool Remove(T item) {
		return members.Remove(item);
	}	

	public RandomBag() {
		members = new List<T>();
	}

	///<summary>
	///Memory optimization: request initial capacity.
	///<summary>	
	public RandomBag(int capacity) {
		members = new List<T>(capacity);
	}

	///<summary>
	///Add all items to the new bag.
	///<summary> 		
	public RandomBag(IEnumerable<T> items) : this() {
		AddRange(items);
	}
	
	///<summary>
	///Add all items to the new bag.
	///<summary> 	
	public RandomBag(params T[] items) : this(items) {
	}
}
