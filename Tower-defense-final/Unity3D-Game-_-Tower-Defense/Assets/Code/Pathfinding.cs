using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Pathfinding : MonoBehaviour
{
	

	//store the enemy path
    public static Transform[] points;
	public static Transform[] total;
	public static bool[][] record = new bool[5][];


	public static int upbound = 0;
	public static int downbound = -20;
	public static int leftbound = 0;
	public static int rightbound = 20;

	void Awake () {
		total = new Transform[transform.childCount];
	    for (int i = 0; i < total.Length; i++)
	    {
	        total[i] = transform.GetChild(i); 
	    }
		for (int i = 0; i < 5; i++) {
			record [i] = new bool[5]{ true, true, true, true, true };
		}
		renew ();
	}    

	public static void renew (){
		ArrayList path = bfsPath(record);
		points = new Transform[path.Count];
		for (int i = 0; i < path.Count; i++) {
			Vector2 temp = (Vector2)path[i];
			points [i] = total [5 * (int)temp.x + (int)temp.y];
		}
	}

	public static ArrayList bfsPath(bool[][] record){
		
		Vector2[] direct = new Vector2[4];
		direct [0] = new Vector2 (1, 0);
		direct [1] = new Vector2 (-1, 0);
		direct [2] = new Vector2 (0, 1);
		direct [3] = new Vector2 (0, -1);
		Vector2 endpoint = new Vector2 (4, 4);

		bool[][] record2 = new bool[5][];
		for (int i = 0; i < 5; i++) {
			record2 [i] = new bool[5];
			for (int j = 0; j < 5; j++) {
				record2 [i] [j] = record [i] [j];
			}
		}


		Vector2 start = new Vector2 (0, 0);
		Queue<Node> q = new Queue<Node> ();
		q.Enqueue (new Node(start, new ArrayList()));
		while (q.Count != 0) {
			int size = q.Count;
			for (int i = 0; i < size; i++) {
				Node tmpNode = q.Dequeue ();
				Vector2 tmp = tmpNode.curr;
				record2 [(int)tmp.x] [(int)tmp.y] = false;
				for (int j = 0; j < direct.Length; j++) {
					Vector2 next = tmp + direct [j];
					if (next.Equals (endpoint)) {
						ArrayList result = new ArrayList (tmpNode.past);
						result.Add (tmp);
						result.Add (endpoint);
						return result;
					}
					if (isValid (next) && record2[(int)next.x][(int)next.y]) {
						ArrayList nList = new ArrayList (tmpNode.past);
						nList.Add (tmp);
						q.Enqueue (new Node(next, nList));
					}
				}
			}
		}

		return null;

	}


	public static bool isValid(Vector2 vec){
		return vec.x < 5 && vec.x >= 0 && vec.y >= 0 && vec.y < 5;
	}
	
}
