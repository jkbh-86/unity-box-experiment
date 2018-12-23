using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState
{
	string StateName { get; }
    Player_Move Player { get; set; }

    void Update();
}

public abstract class PlayerStateBase
{
	public Player_Move Player { get; set; }
	public virtual string StateName { get { return "Base"; } }
	public PlayerStateBase(Player_Move player)
	{
		this.Player = player;
		Debug.Log($"Player entering state: " + this.StateName);
	}

	public virtual void Update()
	{

	}
}

public class PlayerState_Default : PlayerStateBase, IPlayerState
{
	public override string StateName { get {return "Default"; }}
	Animator animator;

	public PlayerState_Default(Player_Move player)
		:base(player)
	{
		try
      	{
       		animator = Player.GetComponentsInChildren<Animator>()[0];   
      	}
      	catch {}
	}

	public override void Update()
	{
		Player_Walk();

		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (Input.GetKeyDown(KeyCode.LeftShift))
			{
				Player.RemoveStackable(Player.BodyStack.GetLastAdded());
			}
			else
			{
				Player.AddStackable(Player.GetNearestStackable());	
			}
		}

		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			Player.ChangePlayerState(new PlayerState_Idle(Player));
		}
	}

	private void Player_Walk()
	{
		float h_Input = Input.GetAxis("Horizontal") * Time.deltaTime * Player.Settings.TurnSpeed; // 150.0f;
		float z_Input = Input.GetAxis("Vertical") * Time.deltaTime * Player.Settings.WalkSpeed;   //3.0f;

        if(animator)
            animator.SetBool("Walking", h_Input + z_Input > 0);

        Player.transform.Rotate(0, h_Input, 0);
		Player.transform.Translate(0, 0, z_Input);

		/* if (h_Input > 0 || z_Input > 0)
		{
			Debug.Log($"Player Turn Speed setting value: " + Player.Settings.TurnSpeed);
			Debug.Log($"Player Walk Speed setting value: " + Player.Settings.WalkSpeed);
		} */
	}
}

public class PlayerState_Idle : PlayerStateBase, IPlayerState
{
	public override string StateName { get {return "Idle"; }}
	public PlayerState_Idle(Player_Move player)
		:base(player)
	{
	}

	public override void Update()
	{
		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			Player.ChangePlayerState(new PlayerState_Default(Player));
		}
	}
}