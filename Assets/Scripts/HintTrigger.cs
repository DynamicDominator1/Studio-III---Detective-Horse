using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class HintTrigger : MonoBehaviour
{
    private static HashSet<string> shownHints = new HashSet<string>(); // names of every hint already shown, static so it survives rooms unloading

    [TextArea] public string message = ""; // hint text 
    public Sprite keyIcon; // sprite slot
    public Key dismissKey = Key.None; // popup dismissal button

    private string hintID; //hints name tag

    void Awake()
    {
        hintID = gameObject.scene.name + "-" + gameObject.name; // set unique name
    }

    void Reset()
    {
        GetComponent<Collider>().isTrigger = true; //set collider to trigger
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>() == null) return; // only player has character controller

        if (shownHints.Contains(hintID)) return; // hint has been shown

        shownHints.Add(hintID); // remember hint has been shown
        NotificationUI.Instance.ShowHint(message, keyIcon, dismissKey); // show hint until dismisssed
    }
}