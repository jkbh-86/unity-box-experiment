using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour {

	public List<Stackable> BodyStack;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float h_Input = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
		float z_Input = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

		transform.Rotate(0, h_Input, 0);
		transform.Translate(0, 0, z_Input);

		if (Input.GetKeyDown(KeyCode.Space))
		{
			Stackable stackable = GetNearestStackable();
			if (stackable != null)
			{
				AddStackable(stackable);
			}
		}
	}

	Stackable GetNearestStackable() {
		Object[] foundStackables = FindObjectsOfType(typeof(Stackable));
		for (int i = 0; i < foundStackables.Length; i++)
		{
			if (((Stackable)foundStackables[i]).playerNear) {
				return (Stackable)foundStackables[i];
			}
		}

		return null;
	}
	void AddStackable(Stackable stackable) {
		ToggleRigidBodyKinematic(true);

		//Increase player y-coord by amount to rest on top of stackable		
		//float stackableTop = stackable.GetTop();
		//Vector3 targetPos = stackable.GetTopCenter();

		//We want the bottom of the player cube to be at the top of the stackable
		//float targetY = GetRelativeYTarget(stackableTop);
		//this.transform.Translate(0, GetRelativeYTarget(stackableTop), 0, Space.Self);

		Vector3 playerBottomCenter = this.GetPlayerBottomCenter();
		Vector3 stackableTopCenter = stackable.GetTopCenter();
		Vector3 targetTranslation = stackableTopCenter - playerBottomCenter;

		Debug.Log($"Player bottom center: {playerBottomCenter.x}, {playerBottomCenter.y}, {playerBottomCenter.z}");
		Debug.Log($"Stackable top center: {stackableTopCenter.x}, {stackableTopCenter.y}, {stackableTopCenter.z}");
		Debug.Log($"Target Relative Vector: {targetTranslation.x}, {targetTranslation.y}, {targetTranslation.z}");

		this.transform.Translate(targetTranslation);
		
		BodyStack.Add(stackable);
		stackable.OnAddedToPlayer(this.gameObject);
	}

	private float GetRelativeYTarget(float stackableTop) {
		float playerBottom = GetPlayerBottom();

		float targetY = stackableTop;
		if (playerBottom < stackableTop) {
			targetY = stackableTop - playerBottom;
		}
		else if (playerBottom > stackableTop) {
			targetY = playerBottom - stackableTop;
		}

		return targetY;
	}

	private float GetWorldYTarget(float stackableTop) {
		
		Vector3 bounds = this.GetComponent<Collider>().bounds.size;
		return stackableTop + bounds.y/2;
	}

	private float GetPlayerBottom() {
		Vector3 bounds = this.GetComponent<Collider>().bounds.size;
		return this.transform.position.y - bounds.y/2;
	}

	private Vector3 GetPlayerBottomCenter() {
		return new Vector3(
			this.transform.position.x,
			this.transform.position.y - GetComponent<Collider>().bounds.extents.y,
			this.transform.position.z
		);
	}

	private void ToggleRigidBodyKinematic(bool toggle) {
		Rigidbody rb = GetRigidBody();
		rb.isKinematic = toggle;
	}

	private Rigidbody GetRigidBody() {
		return GetComponent<Rigidbody>();
	}
}
