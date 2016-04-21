using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FABRIK : MonoBehaviour {
	// NOTE: this script is place on finger GameObject
	GameObject des;
	public Transform root;
	public Transform point2;
	public Transform point3;
	public Transform point4;
	public Transform point5;
	public Transform point6;
	public Transform point7;
	public Transform point8;
	public Transform point9;
	public Transform point10;
	public Transform point11;
	public Transform endEffector;

	private float[] distBtwnEachJoint;
	private float[] distBtwnTargetAndJoint;
	private float[] ratio;
	private Transform[] chain;
	private float totalDistOfJoints = 0f;
	private float tolerance = 0.1f;
	private Transform initialPosB;
	private float endEffectorToTarget = 0.0f;

	void Start () {
		des = GameObject.Find ("Destination");
		distBtwnEachJoint = new float[11];
		distBtwnTargetAndJoint = new float[12];
		ratio = new float[12];

		chain = new Transform[12] {root,point2,point3,point4,point5,point6,
								point7,point8,point9,point10,point11,endEffector};
		// calculate the distance between each joint
		for (int i = 0; i < 11; i++) {
			distBtwnEachJoint [i] = (chain[i].position - chain[i+1].position).magnitude;
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
			for (int i = 0; i < 11; i++) {
				//dist between target and joint
				distBtwnTargetAndJoint [i] = (des.transform.position - chain [i].position).magnitude;
				ratio [i] = distBtwnEachJoint [i] / distBtwnTargetAndJoint [i];
				// new joint position
				chain [i + 1].position = (1 - ratio [i]) * chain [i].position + ratio [i] * des.transform.position;
			}
			updatePosOfAllJoints ();
		} else { // target is reachable
			initialPosB = chain[0];
			Debug.Log ("initialPosB = " + initialPosB.position);
			endEffectorToTarget = (chain [11].position - des.transform.position).magnitude;
			while (endEffectorToTarget > tolerance) {
				//STAGE 1: Forward Reaching
				chain[11].position = des.transform.position;
				for (int i = 10; i >= 0; i--) {
					//NOTE: distBtwnTargetAndJoint is actually storing the distance between adjacent joints
					distBtwnTargetAndJoint [i] = (chain[i+1].position - chain [i].position).magnitude;
					ratio [i] = distBtwnEachJoint [i] / distBtwnTargetAndJoint [i];
					chain [i].position = (1 - ratio [i]) * chain [i+1].position + ratio [i] * chain [i].position;
				}

				//STAGE 2: Backward Reaching
				chain[0].position = initialPosB.position;
				Debug.Log ("chain[0], root = " + chain[0].position);
				for (int i = 0; i < 11; i++) {
					
					distBtwnTargetAndJoint [i] = (chain[i+1].position - chain [i].position).magnitude;
					ratio [i] = distBtwnEachJoint [i] / distBtwnTargetAndJoint [i];
					chain [i+1].position = (1 - ratio [i]) * chain [i].position + ratio [i] * chain [i+1].position;
				}
				endEffectorToTarget = (chain [11].position - des.transform.position).magnitude;
			}
			updatePosOfAllJoints ();
		}
	}

	void updatePosOfAllJoints() {
		root = chain [0];
		point2 = chain [1];
		point3 = chain [2];
		point4 = chain [3];
		point5 = chain [4];
		point6 = chain [5];
		point7 = chain [6];
		point8 = chain [7];
		point9 = chain [8];
		point10 = chain [9];
		point11 = chain [10];
		endEffector = chain [11];
	}
}
