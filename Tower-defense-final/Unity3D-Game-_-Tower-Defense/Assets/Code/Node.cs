using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node{
	public Vector2 curr;
	public ArrayList past;
	public Node(Vector2 c, ArrayList p){
		curr = c;
		past = p;
	}
}


