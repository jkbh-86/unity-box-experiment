using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Block
{
	///Stackable should be a base class that other objects can inherit from
	public class Stackable : BlockBase {

		public bool playerNear;

		// Use this for initialization
		void Start () {
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		public void OnPlayerNearEnter() {
			this.playerNear = true;
			//Debug.Log("Player in range");
		}

		public void OnPlayerNearExit() {
			this.playerNear = false;
			//Debug.Log("Player out of range");
		}

		public void OnAddedToPlayer(GameObject Player) {
			this.ToggleRigidBodyKinematic(true);
			this.ToggleBoxCollider(false);
			this.playerNear = false;

			//Player is parent now, don't need gravity?
			//Should need it, because transform is now governed by parent
			//ToggleRigidBodyKinematic(true);

			//Don't want to trigger events on player being near
			TogglePlayerCollider(false);

			//Update object rotation to match player
			this.transform.rotation = Player.transform.rotation;

			//Parent the transform to the player
			this.transform.parent = Player.transform;
		}

		public void OnRemovedFromPlayer() {
			this.ToggleRigidBodyKinematic(false);
			this.ToggleBoxCollider(true);
			this.transform.parent = null;

			this.TogglePlayerCollider(true);
		}

		public void TogglePlayerCollider(bool toggle) {
			GameObject trigger = GetTriggerCollider();
			trigger.GetComponent<Collider>().enabled = toggle;
		}

		public void ToggleBoxCollider(bool toggle) {
			this.GetComponent<BoxCollider>().enabled = toggle;
		}

		private GameObject GetTriggerCollider() {
			return this.gameObject.transform.Find("StackableTrigger").gameObject;
		}
	}
}