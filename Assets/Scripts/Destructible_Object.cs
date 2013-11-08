using UnityEngine;
using System.Collections;

public class Destructible_Object : MonoBehaviour {
	
	public int HealthPoints = 2;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if (HealthPoints == 0)
		{
			transform.Translate(transform.up * Time.deltaTime);
			if (transform.position.y < -10)
				Destroy(gameObject);
		}
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.GetComponent<bossScript>())
		{
			HealthPoints = 0;
			Destroy(collider);
		}
		else if (collision.gameObject.GetComponent<playerController>() == null)
		{
			HealthPoints -= 1;
			if (HealthPoints == 0)
				Destroy(collider);
		}
	}
}
