using UnityEngine;
using XNode; 


[CreateAssetMenu(menuName = "Dialogue/Dialogue Graph")] // adds to Create menu
public class DialogueGraph : NodeGraph
{
    
    public Dialogue GetStartNode()
    {
        foreach (Node node in nodes) // check every box in the graph
        {
            Dialogue dialogueNode = node as Dialogue; // treat as dialogue box

            if (dialogueNode != null && dialogueNode.isStart) 
            {
                return dialogueNode; // this is the start
            }
        }

        Debug.LogError("No start node found in " + name); // nothing was ticked as start
        return null; // nothing found
    }
}

