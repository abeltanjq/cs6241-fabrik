using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FABRIK : MonoBehaviour {
	// NOTE: this script is place on finger GameObject

	// Use this for initialization
	GameObject des;

	public Transform point1;
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
	public Transform point12;

	private float[] distBtwnEachJoint;
	private Transform[] chain;

	void Start () {
		des = GameObject.Find ("Destination");
		distBtwnEachJoint = new float[11];
		chain = new Transform[12] {point1,point2,point3,point4,point5,point6,
									point7,point8,point9,point10,point11,point12};
		// calculate the distance between each joint
		for (int i = 0; i < 11; i++) {
			distBtwnEachJoint [i] = (chain[i].position - chain[i+1].position).magnitude;
			Debug.Log ("distance between joint "+i+" = " + distBtwnEachJoint [i]);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
