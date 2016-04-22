using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FABRIK_v1: MonoBehaviour {
	// NOTE: this script is place on finger GameObject
	GameObject des;
	private Transform[] chain;
	private int numOfPoints;
	private float[] distBtwnEachJoint;
	private float[] distBtwnTargetAndJoint;
	private float[] ratio;
	private float totalDistOfJoints = 0f;
	private float tolerance = 0.1f;
	private Vector3 initialPosB;
	private float endEffectorToTarget = 0.0f;

	void Start () {
		des = GameObject.Find ("Destination");
		numOfPoints = this.gameObject.transform.childCount;
		distBtwnEachJoint = new float[numOfPoints-1];
		distBtwnTargetAndJoint = new float[numOfPoints];
		ratio = new float[numOfPoints];
		chain = new Transform[numOfPoints];
		for (int i = 0; i < numOfPoints; i++) {
			chain [i] = this.gameObject.transform.GetChild(i);
		}
		// calculate the distance between each joint
		for (int i = 0; i < (numOfPoints - 1); i++) {
			distBtwnEachJoint [i] = (chain[i+1].position - chain[i].position).magnitude;
		//	Debug.Log ("distance between joint "+i+" = " + distBtwnEachJoint [i]);

			totalDistOfJoints += distBtwnEachJoint [i];
		}
	}


	// Update is called once per frame
	void Update () {
		// check distance of root to target
		float distRootToTarget = (chain[0].position - des.transform.position).magnitude;

		//target not reachable
		if (distRootToTarget > totalDistOfJoints) {
			for (int i = 0; i < (numOfPoints - 1); i++) {
				//dist between target and joint
				distBtwnTargetAndJoint [i] = findDistBtwnPoints (des.transform, chain [i]);
				ratio [i] = distBtwnEachJoint [i] / distBtwnTargetAndJoint [i];
				// new joint position
				chain [i + 1].position = (1 - ratio [i]) * chain [i].position + ratio [i] * des.transform.position;
			}
		} 

		else { // target is reachable
			initialPosB = chain[0].position;
		//	Debug.Log ("initialPosB = " + initialPosB);
			endEffectorToTarget = (chain [numOfPoints - 1].position - des.transform.position).magnitude;
			while (endEffectorToTarget > tolerance) {
				//STAGE 1: Forward Reaching
				chain[numOfPoints - 1].position = des.transform.position;
				for (int i = (numOfPoints - 2); i >= 0; i--) {
					//NOTE: distBtwnTargetAndJoint is actually storing the distance between adjacent joints
					distBtwnTargetAndJoint [i] = (chain[i+1].position - chain [i].position).magnitude;
					ratio [i] = distBtwnEachJoint [i] / distBtwnTargetAndJoint [i];
					chain [i].position = (1 - ratio [i]) * chain [i+1].position + ratio [i] * chain [i].position;
				}

				//STAGE 2: Backward Reaching
				chain[0].position = initialPosB;
			//	Debug.Log ("chain[0], root = " + chain[0].position);
				for (int i = 0; i < (numOfPoints - 1); i++) {
					
					distBtwnTargetAndJoint [i] = (chain[i+1].position - chain [i].position).magnitude;
					ratio [i] = distBtwnEachJoint [i] / distBtwnTargetAndJoint [i];
					chain [i+1].position = (1 - ratio [i]) * chain [i].position + ratio [i] * chain [i+1].position;
				}
				endEffectorToTarget = (chain [(numOfPoints - 1)].position - des.transform.position).magnitude;
			}
		//	updatePosOfAllJoints ();
		} 
	}

	public float findDistBtwnPoints (Transform a, Transform b) {
		return (a.position - b.position).magnitude;
	}
}
