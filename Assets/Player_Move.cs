using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : BlockBase {

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
		this.ToggleRigidBodyKinematic(true);

		//Increase player y-coord by amount to rest on top of stackable		
		//float stackableTop = stackable.GetTop();
		//Vector3 targetPos = stackable.GetTopCenter();

		//We want the bottom center of the player cube to be at the top center of the stackable
		//float targetY = GetRelativeYTarget(stackableTop);
		//this.transform.Translate(0, GetRelativeYTarget(stackableTop), 0, Space.Self);

		Vector3 playerBottomCenter = this.GetBottomCenter();
		Vector3 stackableTopCenter = stackable.GetTopCenter();
		Vector3 targetTranslation = stackableTopCenter - playerBottomCenter;

		Debug.Log($"Player bottom center: {playerBottomCenter.x}, {playerBottomCenter.y}, {playerBottomCenter.z}");
		Debug.Log($"Stackable top center: {stackableTopCenter.x}, {stackableTopCenter.y}, {stackableTopCenter.z}");
		Debug.Log($"Target Relative Vector: {targetTranslation.x}, {targetTranslation.y}, {targetTranslation.z}");


		//TODO: sort out vector bug, we only accurately pop on the top center if the player and 
		// the passed in stackable object are aligned in the same way
		// ie. the local z-axes are pointing in the same direction
		// I think this might be a issue with local vs. world corrdinate systems and my own confusion about working with them
		this.transform.Translate(targetTranslation);
		
		BodyStack.Add(stackable);
		stackable.OnAddedToPlayer(this.gameObject);
	}

	private float GetRelativeYTarget(float stackableTop) {
		float playerBottom = this.GetBottom();

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
}
