using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///Stackable should be a base class that other objects can inherit from
public class Stackable : MonoBehaviour {

	public bool playerNear;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPlayerNearEnter() {
		this.playerNear = true;
		Debug.Log("Player in range");
	}

	public void OnPlayerNearExit() {
		this.playerNear = false;
		Debug.Log("Player in range");
	}

	public void OnAddedToPlayer(GameObject Player) {
		this.playerNear = false;

		//Player is parent now, don't need gravity?
		//Should need it, because transform is now governed by parent
		ToggleRigidBodyKinematic(true);

		//Don't want to trigger events on player being near
		TogglePlayerCollider(false);

		this.transform.parent = Player.transform;
	}

	public void OnRemovedFromPlayer() {
		ToggleRigidBodyKinematic(false);
	}

	public float GetTop() {
		Vector3 stackableBounds = GetComponent<Collider>().bounds.size;
		return this.transform.position.y + stackableBounds.y/2;
	}

	public Vector3 GetTopCenter() {
		return new Vector3(
			this.transform.position.x, 
			this.transform.position.y + GetComponent<Collider>().bounds.extents.y,
			this.transform.position.z
		);
	}

	public void TogglePlayerCollider(bool toggle) {
		GameObject trigger = GetTriggerCollider();
		trigger.GetComponent<Collider>().enabled = toggle;
	}

	public void ToggleRigidBodyKinematic(bool toggle) {
		Rigidbody rb = GetRigidBody();
		rb.isKinematic = toggle;
	}

	private GameObject GetTriggerCollider() {
		return this.gameObject.transform.Find("StackableTrigger").gameObject;
	}

	private Rigidbody GetRigidBody() {
		return GetComponent<Rigidbody>();
	}
}
