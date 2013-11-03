using UnityEngine;
using System.Collections;

public class playerMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		animation.CrossFade("idle");
	}	
	
	// Update is called once per frame
	void Update () {
		float fire = Input.GetAxis("Fire1");
		if (Mathf.Abs(fire) > 0.5)
			animation.CrossFade("attack01");
	}
}
