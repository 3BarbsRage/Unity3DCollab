using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class useComputer : MonoBehaviour, IInteractable
{
    public Vector3 FacingComputer;
    public Vector3 SteppingAway;
    public GameObject Player;
    public GameObject MainCam;
    public Vector3 FinalEulerAngles;
    public bool DoneMoving = true;
    //public bool DoneRotating = true;
    public bool exitting = false;
    public void Interact()
    {
        Player.GetComponent<FirstPersonController>().enabled = false;
        Player.GetComponent<CharacterController>().enabled = false;
        Player.GetComponent<Interactor>().inComputer = true;
        //interactor.transform.parent.GetComponent<Rigidbody>().enabled = false;
        DoneMoving = false;
    }
    public void enterComputer()
    {
        if (!DoneMoving)
        {
            Vector3 Direction = (FacingComputer - Player.transform.position);
            Player.transform.position += Direction.normalized * 1.2f * Time.deltaTime;
            Player.transform.rotation = Quaternion.RotateTowards(
            Player.transform.rotation,
            Quaternion.Euler(FinalEulerAngles),
            ((Player.transform.rotation.eulerAngles - Quaternion.Euler(FinalEulerAngles).eulerAngles).magnitude + 19f) * Time.deltaTime);
            MainCam.transform.rotation = Quaternion.RotateTowards(
            MainCam.transform.rotation,
            Quaternion.Euler(0,-180,0),
            ((MainCam.transform.rotation.eulerAngles - Quaternion.Euler(0, -180, 0).eulerAngles).magnitude + 19f) * Time.deltaTime);

            if (Direction.magnitude <= 0.05 &&
                (Player.transform.rotation.eulerAngles - Quaternion.Euler(FinalEulerAngles).eulerAngles).magnitude <= 0.35 &&
                (MainCam.transform.rotation.eulerAngles - Quaternion.Euler(0, -180, 0).eulerAngles).magnitude <= 0.35) 
            { 
                DoneMoving = true;
                Player.transform.rotation = Quaternion.Euler(FinalEulerAngles);
                MainCam.transform.rotation = Quaternion.Euler(0, -180, 0);
            }
        }
    }


    public void exitComputer()
    {
        Vector3 Direction = (SteppingAway - Player.transform.position);
        Player.transform.position += Direction.normalized * 1.2f * Time.deltaTime;
        //Debug.Log(Direction.magnitude);
        if (Direction.magnitude <= 0.04)
        {
            Player.GetComponent<FirstPersonController>().enabled = true;
            Player.GetComponent<CharacterController>().enabled = true;
            Player.GetComponent<Interactor>().inComputer = false;
            exitting = false;
        }
    }
    void Update()
    {
        enterComputer();
        if (exitting && DoneMoving)
        {
            exitComputer();
        }
    }
}
