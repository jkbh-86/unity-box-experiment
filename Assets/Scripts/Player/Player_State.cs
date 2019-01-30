//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Look into setting up a proper state machine? 
// Ref: https://unity3d.com/learn/tutorials/topics/navigation/transitions-between-states

namespace Player.States
{
	public interface IPlayerState
	{
		string Name { get; }
		Player Player { get; set; }

		void Update();
		void SetState(string state);
	}

	public abstract class PlayerStateBase
	{
		public Player Player { get; set; }
		public virtual string Name { get { return "Base"; } }
		public PlayerStateBase(Player player)
		{
			this.Player = player;
			Debug.Log($"Player entering state: " + this.Name);
		}

		public virtual void Update(){}
		public abstract void SetState(string state);
	}

	public class PlayerState_Default : PlayerStateBase, IPlayerState
	{
		public override string Name { get { return "Default"; } }
		Animator animator;

		public PlayerState_Default(Player player)
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

			if (Input.GetKey(KeyCode.LeftShift))
			{
				if (Input.GetKeyDown(KeyCode.Space))
				{
					Player.RemoveStackable(Player.BodyStack.GetLastAdded());
				}
			}
			else
			{
				if (Input.GetKeyDown(KeyCode.Space))
				{
					Player.AddStackable(Player.GetNearestStackable());
					//this.SetState("Idle");
				}
			}
			

			/* if (Input.GetKeyDown(KeyCode.LeftShift))
			{
				Player.ChangePlayerState(new PlayerState_Idle(Player));
			} */
		}
		
		public override void SetState(string state)
		{
			switch(state)
			{
				case "Idle":
					SetWalkAnim(false);	// Glitch where animator stays active after state change, ensure turns false
					this.Player.State = state;
					break;
				default:
					Debug.Log("Cannot switch to the state: " + state + " from the state: " + this.Name);
					break;
			}
		}
		
		private void Player_Walk()
		{
			float h_Input = Input.GetAxis("Horizontal") * Time.deltaTime * Player.Settings.TurnSpeed;
			float z_Input = Input.GetAxis("Vertical") * Time.deltaTime * Player.Settings.WalkSpeed;

			this.SetWalkAnim(h_Input + z_Input != 0);			

			Player.transform.Rotate(0, h_Input, 0);
			Player.transform.Translate(0, 0, z_Input);
		}

		private void SetWalkAnim(bool On)
		{
			if(animator)
			{
				animator.SetBool("Walking", On);
			}
		}
	}

	public class PlayerState_Idle : PlayerStateBase, IPlayerState
	{
		public override string Name { get { return "Idle"; } }
		public PlayerState_Idle(Player player)
			:base(player)
		{
		}

		public override void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				this.SetState("Default");
			}
		}

		public override void SetState(string state)
		{
			switch(state)
			{
				case "Default":
					this.Player.State = state;
					break;
				default:
					Debug.Log("Cannot switch to the state: " + state + " from the state: " + this.Name);
					break;
			}
		}
	}
}