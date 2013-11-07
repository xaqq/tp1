using UnityEngine;
using System.Collections;

public class SecretPassage : MonoBehaviour {
	
	bool _isInside = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.GetComponent<playerController>() != null)
		{
			print (2 * (transform.position - collider.transform.position));
			collider.transform.position = collider.transform.position + (2 * (transform.position - collider.transform.position));
			collider.transform.position = new Vector3(collider.transform.position.x, 0 , collider.transform.position.z);
		}
	}
}
