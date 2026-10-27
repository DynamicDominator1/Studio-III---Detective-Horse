using System.Collections.Generic;
using UnityEngine;

public class WorldStateManager : MonoBehaviour
{
    private static HashSet<string> removedID = new HashSet<string>(); // static string names of removed items

    private string itemID; // this items tag 

    private void Awake()
    {
        itemID = gameObject.scene.name + "-" + gameObject.name; // set item ID to "scene name" - "object Name"
    }

    private void Start()
    {
        if (removedID.Contains(itemID)) // does removed objects hash set contain items ID
        {
            gameObject.SetActive(false); // turn game object off
        }
    }

    public static void Remove(GameObject obj) // called by other scripts
    {
        WorldStateManager item = obj.GetComponent<WorldStateManager>(); // checks if object has world state manager

        if (item != null) // cancels if there is no world state manager
        {
            removedID.Add(item.itemID); // add item id to removed id
        }

        obj.SetActive(false); // turn game object off
    }

}
