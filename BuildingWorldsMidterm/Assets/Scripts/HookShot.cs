using UnityEngine;
//using UnityEngine.
using UnityEngine.Rendering;
using System.Collections;

public class HookShot : MonoBehaviour {

	Rigidbody rigid;
//	Rigidbody parentRigid;

	GameObject curTarget;
	Joint hingeTarget;

	public bool swinging { get; private set; }
	// Use this for initialization
	void Awake () {
		rigid = GetComponent<Rigidbody> ();
//		parentRigid = transform.parent.GetComponent<Rigidbody> ();
		HookTarget.OnHookTargetAcquired += OnTargetAcquiredHandler;
	}
	void OnDestroy () {
		HookTarget.OnHookTargetAcquired -= OnTargetAcquiredHandler;
	}
	
	// Update is called once per frame
	void Update () {
		if (!swinging) {
		    if (Input.GetButtonDown ("Fire1")) {
				SetGrapple (potentialTarget, 0);
			} else if (Input.GetButtonDown ("Fire2")) {
				SetGrapple (potentialTarget, 1);
			}
		} else {
			if(Input.GetButtonUp ("Fire1") || Input.GetButtonUp ("Fire2")) {
				FreeGrapple ();
			}
		}
//		float val = Mathf.Sin (Time.time);
//		RenderSettings.fogColor = new Color (val,0,0);
	}


	void SetGrapple (GameObject targ, int grappleType) {
		if (targ == null) return;

		if (curTarget != null) FreeGrapple ();



		swinging = true;
		curTarget = targ;
		Rigidbody targRigid = curTarget.GetComponent<Rigidbody> ();
		if (targRigid != null) {



			if (grappleType == 0) {
				HingeJoint target_hinge = targRigid.gameObject.AddComponent <HingeJoint> ();
				target_hinge.connectedBody = rigid;
//				parentRigid.isKinematic = true;
				target_hinge.autoConfigureConnectedAnchor = false;
				target_hinge.connectedAnchor = targRigid.transform.position;
				hingeTarget = target_hinge;

//				target_hinge.maxDistance = 50;
//				target_hinge.minDistance = 45;
			} else if (grappleType == 1) {
				SpringJoint target_spring = targRigid.gameObject.AddComponent <SpringJoint> ();
				target_spring.connectedBody = rigid;
				target_spring.autoConfigureConnectedAnchor = false;
				target_spring.connectedAnchor = targRigid.transform.position;
				target_spring.maxDistance = 50;
				target_spring.minDistance = 5;
				hingeTarget = target_spring;
			}
		}

	}
//	IEnumerator 

	public void FreeGrapple () {
//		if (hingeTarget != null) 
		if (curTarget != null) {
			Destroy (hingeTarget);
			curTarget = null;
			swinging = false;
//			parentRigid.isKinematic = false;
		}
	}

	GameObject potentialTarget;
	void OnTargetAcquiredHandler (GameObject obj, bool entering) {
		if (entering) {
			potentialTarget = obj;
		} else {
			potentialTarget = null;
		}
	}
}
