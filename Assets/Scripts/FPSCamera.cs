using UnityEngine;
using System.Collections;

public class FPSCamera : MonoBehaviour {
	
	public Transform target;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	Vector3 newpos = target.transform.position;
		newpos.y += 3;
		transform.position = newpos;
		
		transform.eulerAngles = target.eulerAngles;
	}
}
