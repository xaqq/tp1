using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {
	
	public int Damage;
	public float Speed;
	public float Range;
	private float _DistanceTravelled;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (_DistanceTravelled >= Range)
		{
			Destroy(gameObject);
		}
		else if (_DistanceTravelled + Speed * Time.deltaTime > Range)
		{
			transform.Translate(Vector3.forward * (Range - _DistanceTravelled));
			_DistanceTravelled = Range + 1;
		}
		else
		{
			transform.Translate(Vector3.forward * Speed * Time.deltaTime);
			_DistanceTravelled += Speed * Time.deltaTime;
		}
	}
	
	void OnTriggerEnter(Collider collider)
	{
		if (collider.isTrigger)
			return ;
		MonsterScript mob = collider.gameObject.GetComponent<MonsterScript>();
		if (mob != null)
		{
			mob.hurt(Damage);
		}
		if (collider.gameObject.GetComponent<playerController>() == null)
			Destroy(gameObject);
	}
}
