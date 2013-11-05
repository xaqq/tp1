﻿using UnityEngine;
using System.Collections;

public class MonsterScript : MonoBehaviour {
	private playerController _target = null;
	public int HealthPoints;
	public int AttackRange;
	public int Damage;
	public int AttackSpeed;
	public Vector3[] NavigationNodes;
	public float NodeDelay = 2.0f;
	public int Speed;
	private int _defSpeed;
	private int _currentNavNode = 0;
	private float _currentDelay = 0.0f;
	private bool _isDelay = false;
	private bool _isAttackRecovery = false;
	private float _recoveryTimer = 0.0f;
	private float _speedRecoveryTimer = -1.0f;
	// Use this for initialization
	void Start () {
	_defSpeed = Speed;
	}
	
	public void SetTarget(playerController target)
	{
		_target = target;
	}
	
	public void hurt(int damage)
	{
		HealthPoints -= damage;
		if (HealthPoints <= 0)
			Destroy(gameObject);
	}
	
	public void setSpeed(int speed, int delay)
	{
		Speed = speed;
		_speedRecoveryTimer = delay;
	}
	
	// Update is called once per frame
	void Update () {
		if (_speedRecoveryTimer > 0)
		{
			_speedRecoveryTimer -= Time.deltaTime;
			if (_speedRecoveryTimer < 0)
				Speed = _defSpeed;
		}
		
		rigidbody.velocity = Vector3.zero;
		if (_isAttackRecovery && _target != null)
		{
			_recoveryTimer += Time.deltaTime;
			if (_recoveryTimer > AttackSpeed)
			{
				_target.hurt(Damage);
				_isAttackRecovery = false;
			}
		}
		else if (_target != null)
		{
			Vector3 TargetPosition = _target.transform.position;
			TargetPosition.y = transform.position.y;
			transform.LookAt(TargetPosition);
			if ((TargetPosition - transform.position).magnitude < AttackRange)
			{
				_isAttackRecovery = true;
				_recoveryTimer = 0.0f;
				return;
			}
			else
				transform.Translate(Vector3.forward * Speed * Time.deltaTime);
		}
		else if (_isDelay)
		{
			_currentDelay += Time.deltaTime;
			if (_currentDelay > NodeDelay)
				_isDelay = false;
		}
		else
		{
			transform.LookAt(NavigationNodes[_currentNavNode]);
			if ((NavigationNodes[_currentNavNode] - transform.position).magnitude <= Speed * Time.deltaTime)
			{
				transform.position = NavigationNodes[_currentNavNode];
				_isDelay = true;
				_currentDelay = 0;
				_currentNavNode++;
				if (_currentNavNode >= NavigationNodes.Length)
				{
					_currentNavNode = 0;
				}
			}
			else
				transform.Translate(Vector3.forward * Speed * Time.deltaTime);
		}
	}
}
