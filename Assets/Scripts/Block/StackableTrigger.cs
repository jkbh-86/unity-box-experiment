using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Block
{
	[RequireComponent(typeof(Stackable))]
	[RequireComponent(typeof(Collider))]
	public class StackableTrigger : StackableChild {

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		/// <summary>
		/// OnTriggerEnter is called when the Collider other enters the trigger.
		/// </summary>
		/// <param name="other">The other Collider involved in this collision.</param>
		void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.name.Equals("Player"))
			{		
				//this.SendMessageUpwards("OnPlayerNearEnter");
				Stackable parent = GetParentStackable();
				parent.OnPlayerNearEnter();		
			}
		}

		/// <summary>
		/// OnTriggerExit is called when the Collider other has stopped touching the trigger.
		/// </summary>
		/// <param name="other">The other Collider involved in this collision.</param>
		void OnTriggerExit(Collider other)
		{
			if (other.gameObject.name.Equals("Player"))
			{
				//this.SendMessageUpwards("OnPlayerNearExit");
				Stackable parent = GetParentStackable();
				parent.OnPlayerNearExit();
			}
		}
	}
}