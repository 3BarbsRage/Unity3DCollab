using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact();
}

public class Interactor : MonoBehaviour
{
    public float interactionRange = 2f;
    public bool inComputer = false;
    public GameObject Computer;
    public Transform ChildTransform;

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(ChildTransform.rotation);
        if (Input.GetMouseButtonDown(1))
        {
            if (inComputer)
            {
                Computer.GetComponent<useComputer>().exitting = true;
            }
            //Vector3 dir = new Vector3(ChildTransform.rotation.x, gameObject.transform.rotation.y, 0);
            Ray ray = new Ray(gameObject.transform.position, ChildTransform.forward);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, interactionRange))
            {
                try
                    {
                    hitInfo.collider.gameObject.GetComponent<IInteractable>().Interact();
                    }
                catch { return; }
            }
        }

    }
}
