﻿using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Extension methods for Unity's Transform class.
/// </summary>
public static class TransformExtensions {
	
	/// <summary>
	/// Gets all direct children.
	/// </summary>
	/// <returns>
	/// The children.
	/// </returns>
	/// <param name='root'>
	/// Root.
	/// </param>
	public static Transform[] GetChildren(this Transform root) {
		Transform[] children = new Transform[root.childCount];
		int i=0;
		foreach(Transform child in root) {
			children[i++] = child;
		}
		return children;
	}

	/// <summary>
	/// Attaches a list of transforms to this one.
	/// </summary>
	/// <param name='root'>
	/// Parent transform.
	/// </param>
	/// <param name='children'>
	/// Array, list, or other collection of transforms to add.
	/// </param>
	public static void AttachChildren(this Transform root, IEnumerable<Transform> children) {
		foreach(var child in children) {
			child.parent = root;
		}
	}
	
	public static void AttachChild(this Transform root, Transform child) {
		child.parent = root;
	}
	
	/// <summary>
	/// Clears the position of this transform without affecting its children.
	/// </summary>
	/// <param name='root'>
	/// Root.
	/// </param>
	public static void ClearLocalPosition(this Transform root) {
		var children = root.GetChildren();
		root.DetachChildren();
		root.localPosition = Vector3.zero;
		root.AttachChildren(children);
	}
	
	/// <summary>
	/// Clears rotation of this transform without affecting its children.
	/// </summary>
	/// <param name='root'>
	/// Root.
	/// </param>
	public static void ClearLocalRotation(this Transform root) {
		var children = root.GetChildren();
		root.DetachChildren();
		root.localRotation = Quaternion.identity;
		root.AttachChildren(children);
	}

	/// <summary>
	/// Clears scale of this transform without affecting children.
	/// </summary>
	/// <param name='root'>
	/// Root.
	/// </param>
	public static void ClearLocalScale(this Transform root) {
		var children = root.GetChildren();
		root.DetachChildren();
		root.localScale = Vector3.one;
		root.AttachChildren(children);
	}
	
	public static void MultiplyScale(this Transform root, float multiplier) {
		root.transform.localScale = root.transform.localScale * multiplier;
	}
	
	public static void MultiplyScale(this Transform root, Vector3 multiplier) {
		root.transform.localScale = root.transform.localScale.WithScale(multiplier);
	}
}
