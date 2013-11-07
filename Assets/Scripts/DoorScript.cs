using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {
	
	private bool _isOpened = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider target)
	{
		if (!_isOpened)
		{
		_isOpened = true;
		animation.CrossFade("Take 001");
		}
	}
}
