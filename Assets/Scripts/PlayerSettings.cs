using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
	/* private float? _turnSpeed = null;
	public float TurnSpeed 
	{ 
		get
		{
			return _turnSpeed ?? 150.0f;
		}

		set
		{
			_turnSpeed = value;
		}
	}
	private float? _walkSpeed = null;
	public float WalkSpeed 
	{ 
		get
		{
			return _walkSpeed ?? 3.0f;
		}

		set
		{
			_walkSpeed = value;
		}
	} */
    public float TurnSpeed = 150.0f;
    public float WalkSpeed = 3.0f;
    public bool BlocksStackUp = true;

	public PlayerSettings(){}
}