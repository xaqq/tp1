using UnityEngine;
using System.Collections;

public class AggroZoneBoss : MonoBehaviour {
	
	public bossScript _monster;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider target)
	{
		playerController _target = target.GetComponent<playerController>();
		if (_target != null)
			_monster.SetTarget(_target);
	}
	
	void OnTriggerExit(Collider target)
	{
		playerController _target = target.GetComponent<playerController>();
		if (_target != null)
			_monster.SetTarget(null);
	}
}
