using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Block
{
	public class BlockBase : MonoBehaviour {
		// Use this for initialization
		void Start () { }
		
		// Update is called once per frame
		void Update () { }

		/// <summary>
		/// Returns the size of the collider bounds in the y-axis
		/// </summary>
		/// <returns></returns>
		public virtual float GetHeight()
		{
			//Updated: BoxCollider.size gives us local space values, Collider.bounds.size gives us world-space
			// returning world space sizes if the object is rotated doesn't give an accurate value
			BoxCollider collider = GetComponent<BoxCollider>();
			return collider.size.y; //.bounds.size.y;
		}

		/// <summary>
		/// Gets a float value representing the position of the top block face along the world y-axis
		/// </summary>
		/// <returns></returns>
		public virtual float GetTop() {
			BoxCollider collider = GetComponent<BoxCollider>();
			return collider.bounds.center.y + collider.size.y/2;// .bounds.extents.y;
			
			//return this.transform.position.y + collider.bounds.center.y + collider.bounds.extents.y;
			//return this.transform.position.y + GetComponent<Collider>().bounds.extents.y;
		}

		/// <summary>
		/// Gets a float value representing the position of the bottom block face along the world y-axis
		/// </summary>
		/// <returns></returns>
		public virtual float GetBottom() {
			BoxCollider collider = GetComponent<BoxCollider>();
			//return this.transform.position.y - GetComponent<Collider>().bounds.extents.y;

			//Updated: use local y-axis size, rather than possible world-space rotated value
			return collider.bounds.center.y - collider.size.y/2;
		}

		/// <summary>
		/// Gets a Vector3 object representing the position of the center of the block face
		/// on the top side of the object in world coordinates
		/// </summary>
		/// <returns></returns>
		public virtual Vector3 GetTopCenter() {
			return new Vector3(
				this.transform.position.x, 
				this.GetTop(),
				this.transform.position.z
			);
		}

		/// <summary>
		/// Gets a Vector3 object representing the position of the center of the block face
		/// on the bottom side of the object in world coordinates
		/// </summary>
		/// <returns></returns>
		public virtual Vector3 GetBottomCenter() {
			return new Vector3(
				this.transform.position.x, 
				this.GetBottom(),
				this.transform.position.z
			);
		}

		/// <summary>
		/// Gets the RigidBody component for this object and sets the isKinematic bool to the passed in value
		/// </summary>
		/// <param name="toggle"></param>
		protected void ToggleRigidBodyKinematic(bool toggle) {
			Rigidbody rb = GetRigidBody();
			rb.isKinematic = toggle;
		}

		/// <summary>
		/// Return the RigidBody component for this object
		/// </summary>
		/// <returns></returns>
		protected Rigidbody GetRigidBody() {
			return GetComponent<Rigidbody>();
		}
	}
}