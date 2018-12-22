using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : BlockBase {

	public BlockStack BodyStack;
	public bool BlocksStackUp = true;
    Animator animator;
    // Use this for initialization
    void Start () {
      if (BodyStack == null) { BodyStack = new BlockStack(); }
      try
      {
          animator = GetComponentsInChildren<Animator>()[0];
      }
      catch {
      }
    }
	
	// Update is called once per frame
	void Update () {
		float h_Input = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
		float z_Input = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        if(animator)
            animator.SetBool("Walking", h_Input + z_Input > 0);
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
	private void AddStackable(Stackable stackable) {
		this.ToggleRigidBodyKinematic(true);

		if (this.BlocksStackUp)
		{
			StackOnTop(stackable);
		}
		else
		{
			StackOnBottom(stackable);
		}

		BodyStack.Stack.Add(stackable);
		stackable.OnAddedToPlayer(this.gameObject);

		//If we do this, hilarity ensues
		//this.ToggleRigidBodyKinematic(false);
	}

	/// <summary>
	/// Updates the passed in Stackables transform by moving the object to be centered above the player
	/// </summary>
	/// <param name="stackable"></param>
	private void StackOnTop(Stackable stackable)
	{
		Vector3 stackableBottomCenter = stackable.GetBottomCenter();
		Vector3 playerTopCenter = this.GetTopCenter();
		Vector3 targetTranslation = playerTopCenter - stackableBottomCenter;

		stackable.transform.Translate(targetTranslation, Space.World);
	}

	/// <summary>
	/// Updates the players transform by moving the player above the center of the Stackable object
	/// </summary>
	/// <param name="stackable"></param>
	private void StackOnBottom(Stackable stackable)
	{
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
		this.transform.Translate(targetTranslation, Space.World);
	}

	/// <summary>
	/// Gets a float value representing the y-axis value of the player top plus the height of the player BlockStack
	/// </summary>
	/// <returns></returns>
	public override float GetTop()
	{
		return base.GetTop() + this.BodyStack.GetStackHeight();
	}

	public override Vector3 GetTopCenter()
	{
		return new Vector3(
			this.transform.position.x, 
			this.GetTop(),
			this.transform.position.z
		);
	}
}
