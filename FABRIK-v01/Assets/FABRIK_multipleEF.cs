using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FABRIK_multipleEF : MonoBehaviour {

	public Transform target;
	public Transform root;
	private Vector3 initialPosB;
	private Queue<float> initialDist = new Queue<float>();

	// Use this for initialization
	void Start () {
		initialPosB = root.position;
	}
	
	// Update is called once per frame
	void Update () {
	//	while (true) { // within tolerance
			forwardReaching (root);
			root.position = initialPosB;
			backwardReaching (root);
	//	}
	}

	void forwardReaching (Transform currNode) {
		int childCount = currNode.childCount;
		if (childCount == 0) {
			// Reached End Effector
			currNode.position = target.position;
			return;
		} else {
			Vector3[] allSubBasePos = new Vector3[childCount];
			for (int i = 0; i < childCount; i++) {
				float oldDistBtwnNode = findDistBtwnNodes (currNode, currNode.GetChild (i));
				initialDist.Enqueue (oldDistBtwnNode);
				forwardReaching (currNode.GetChild (i));
				float newDistBtwnNode = findDistBtwnNodes (currNode, currNode.GetChild (i));
				float ratio = oldDistBtwnNode / newDistBtwnNode;
				allSubBasePos [i] = (1 - ratio) * currNode.GetChild (i).position + ratio * currNode.position;
			}
			Vector3 newCurrNodePos = new Vector3 (0, 0, 0);
			for (int j = 0; j < childCount; j++) {
				newCurrNodePos += allSubBasePos [j];
			}
			currNode.position = newCurrNodePos/childCount;
			return;
		}
	}

	void backwardReaching (Transform currNode) {
		int childCount = currNode.childCount;
		if (childCount == 0) {
			// Reached End Effector, do nothing
		} else {
			Vector3[] allSubBasePos = new Vector3[childCount];
			for (int i = 0; i < childCount; i++) {
				float oldDistBtwnNode = initialDist.Dequeue ();
				float newDistBtwnNode = findDistBtwnNodes (currNode, currNode.GetChild (i));
				float ratio = oldDistBtwnNode / newDistBtwnNode;
				currNode.GetChild(i).position = (1 - ratio) * currNode.position + ratio * currNode.GetChild(i).position;
				backwardReaching (currNode.GetChild (i));
			}
		}
	}

	public float findDistBtwnNodes (Transform a, Transform b) {
		return (a.position - b.position).magnitude;
	}
}
