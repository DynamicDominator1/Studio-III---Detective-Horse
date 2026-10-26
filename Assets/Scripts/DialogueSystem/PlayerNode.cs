using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class PlayerNode : Node
{

	[Range(0,100)]public float jumpPower;

	[SerializeField] private ClueData erquiredClue;

	[Input] public bool enter;

	[TextArea(10, 25)] public string text;

	// Use this for initialization
	protected override void Init() {
		base.Init();
		
	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
		return null; // Replace this
	}
}