﻿using UnityEngine;
using System.Collections;

public class HookTarget : MonoBehaviour {

	public delegate void HookTargetAcquired (GameObject obj, bool entering);
	public static event HookTargetAcquired OnHookTargetAcquired;

	Light lite;	
	void Start () {
		lite = GetComponent<Light> ();
	}
	
	void OnMouseEnter () {
		if (OnHookTargetAcquired != null)
			OnHookTargetAcquired (gameObject, true);

		lite.enabled = true;
	}


	void OnMouseExit () {
		if (OnHookTargetAcquired != null)
			OnHookTargetAcquired (gameObject, false);

		lite.enabled = false;
	}
}
