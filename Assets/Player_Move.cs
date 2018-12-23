using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player_Settings))]
public class Player_Move : BlockBase {
	private IPlayerState PlayerState;
	public BlockStack BodyStack;
	public Player_Settings Settings;
	
    //Animator animator;
    // Use this for initialization
    void Start () {
      if (BodyStack == null) { BodyStack = new BlockStack(); }
	  PlayerState = new PlayerState_Default(this);
	  Settings = GetComponentInParent<Player_Settings>(); //new Player_Settings();
	  
      /* try
      {
          animator = GetComponentsInChildren<Animator>()[0];
      }
      catch {
      } */
    }
	
	// Update is called once per frame
	void Update () {
		//TODO: replace single update function with access to PlayerState class update?
		// Divide actions across multiple states?

		// When a specific context or player action arises that causes the player to switch states, we update a 
		// property that contains the current state

		// Create PlayerStateBase and PlayerStateChild classes where base class virtual functions
		// provide default, overrideable behaviour available to all child classes

		// Have default state that handles regular actions, and provides means to switch states with button toggles or holds

		// How does this work though? Actions performed would require using data from the player that might be private
		// Do we pass in the player object and call public facing functions on it?

		this.PlayerState.Update();

		/* float h_Input = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
		float z_Input = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        if(animator)
            animator.SetBool("Walking", h_Input + z_Input > 0);

        transform.Rotate(0, h_Input, 0);
		transform.Translate(0, 0, z_Input);

		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (Input.GetKeyDown(KeyCode.LeftShift))
			{
				RemoveStackable(this.BodyStack.GetLastAdded());
			}
			else
			{
				AddStackable(GetNearestStackable());	
			}
		} */
	}

	public Stackable GetNearestStackable() {
		Object[] foundStackables = FindObjectsOfType(typeof(Stackable));
		for (int i = 0; i < foundStackables.Length; i++)
		{
			if (((Stackable)foundStackables[i]).playerNear) {
				return (Stackable)foundStackables[i];
			}
		}

		return null;
	}
	public void AddStackable(Stackable stackable) {
		if (stackable != null)
		{
			//this.ToggleRigidBodyKinematic(true);

			if (this.Settings.BlocksStackUp)
			{
				StackOnTop(stackable);
			}
			else
			{
				StackOnBottom(stackable);
			}

			BodyStack.AddStackable(stackable);
			stackable.OnAddedToPlayer(this.gameObject);

			//If we do this, hilarity ensues
			//this.ToggleRigidBodyKinematic(false);
		}
	}
	public void RemoveStackable(Stackable stackable)
	{
		if (stackable != null)
		{
			//TODO: place object on ground in front of player
		}
	}

	public void ChangePlayerState(IPlayerState playerState)
	{
		this.PlayerState = playerState;
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
		Vector3 playerBottomCenter = this.GetBottomCenter();
		Vector3 stackableTopCenter = stackable.GetTopCenter();
		Vector3 targetTranslation = stackableTopCenter - playerBottomCenter;

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