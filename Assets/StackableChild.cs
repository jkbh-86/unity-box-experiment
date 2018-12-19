using UnityEngine;

public class StackableChild : MonoBehaviour {
    protected Stackable GetParentStackable() {
		return this.transform.root.gameObject.GetComponent<Stackable>();		
	}
}