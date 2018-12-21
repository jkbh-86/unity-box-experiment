using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBase : MonoBehaviour {
    // Use this for initialization
	void Start () { }
	
	// Update is called once per frame
	void Update () { }



    /// <summary>
    /// Gets a float value representing the position of the top block face along the world y-axis
    /// </summary>
    /// <returns></returns>
    public float GetTop() {
		return this.transform.position.y + GetComponent<Collider>().bounds.extents.y;
	}

    /// <summary>
    /// Gets a float value representing the position of the bottom block face along the world y-axis
    /// </summary>
    /// <returns></returns>
    public float GetBottom() {
		return this.transform.position.y - GetComponent<Collider>().bounds.extents.y;
	}

    /// <summary>
    /// Gets a Vector3 object representing the position of the center of the block face
    /// on the top side of the object in world coordinates
    /// </summary>
    /// <returns></returns>
	public Vector3 GetTopCenter() {
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
	public Vector3 GetBottomCenter() {
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