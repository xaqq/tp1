using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {
	public float speed_ = 10;
	public float rotationSpeed_ = 180;
	
	// Use this for initialization
	void Start () {
		animation.CrossFade("idle");
	}
	
	private void update_animation()
	{		
		if (animation["run"].speed < 0.1)
		{
			if (animation.IsPlaying("run"))
			{
				if (Random.Range(0, 100) < 50)
					animation.CrossFade("idle");
				else 
					animation.CrossFade("idle_alt");
			}
		}
		else
		{		
			animation.CrossFade("run");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		float vertical = Input.GetAxis ("Vertical");
		float horizontal = Input.GetAxis ("Horizontal");
		
		// Get the horizontal and vertical axis.
		// By default they are mapped to the arrow keys.
		// The value is in the range -1 to 1
		float translation = vertical * speed_;
		float rotation = horizontal * rotationSpeed_;
		
		// Make it move 10 meters per second instead of 10 meters per frame...
		translation *= Time.deltaTime;
		rotation *= Time.deltaTime;
		 
		// Move translation along the object's z-axis
		transform.Translate (0, 0, translation);
		// Rotate around our y-axis
		transform.Rotate (0, rotation, 0);
		animation["run"].speed = Mathf.Sqrt(Mathf.Pow(vertical, 2) + Mathf.Pow(horizontal, 2));
		print (animation["run"].speed);
		this.update_animation();
	}
}
