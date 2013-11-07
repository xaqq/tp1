using UnityEngine;
using System.Collections;

public class arrow : MonoBehaviour {
	public float speed_ = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float move = speed_ * Time.deltaTime;
		
		Vector3 tmp = transform.position;
		//tmp = transform.position + transform.rotation.eulerAngles * move;
		//print (transform.rotation);
		
		transform.position = tmp;
	}
}
