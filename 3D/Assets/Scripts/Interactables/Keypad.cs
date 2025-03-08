using UnityEngine;

public class Keypad : Interactable
{
    public GameObject door;
    private bool doorOpen;

    protected override void Interact()
    {
        doorOpen = !doorOpen; //toggle
        door.GetComponent<Animator>().SetBool("IsOpen", doorOpen);
    }
}
