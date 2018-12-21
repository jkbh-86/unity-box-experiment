using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ColorPicker))]
[RequireComponent(typeof(Celebrate))]
public class BlockState : MonoBehaviour {

    Collider satisfied = null;

    public void SetAsTrigger() {
        Color color = this.GetComponent<MeshRenderer>().material.color;
        color.a -= 0.5f;
        this.GetComponent<MeshRenderer>().material.color = color;
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "IgnoreEndTrigger")
            return;
        if (satisfied != null)
            return;
        if (CheckColor()) {
            Debug.Log("Satisfied");
            this.satisfied = col;
        }
    }

    private bool IsTrigger() {
        return GetComponent<Collider>().isTrigger;
    }

    private bool CheckColor() {
        // TODO:: add a check with a passed in color, too
        return GetComponent<ColorPicker>().color == "DEFAULT";
    }
    private void OnTriggerExit(Collider col) {
        this.satisfied = null;
        Debug.Log("Unsatisfied");
    }

    public bool IsSatisfied()
    {
        return (IsTrigger() == false || satisfied != null);
    }

}
