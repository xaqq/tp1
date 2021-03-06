﻿using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {
	public GameObject _hud;
	public AudioClip _teleportSound;
	public AudioClip _fireSound;
	public AudioClip _healSound;
	public AudioClip _snareSound;
	public AudioClip _attackSound;
	public float speed_ = 4;
	public float _maxTpRange = 30;
	private float rootDuration;
	private float lastSpeed;
	public float rotationSpeed_ = 180;
	public UISlider _life;
	public UISlider _mana;
	private UILabel _name;
	public float _curHp = 100;
	public float _maxHp = 100;
	public float _curMp = 100;
	public float _maxMp = 100;
	private bool _isAttacking = false;
	private Vector3 _clickingPosition;
	private Vector3 _startRotation;
	private Vector3 _endRotation;
	private float _currentRotationTime = 0;
	public Transform arrow;
	private Vector3 _click = Vector3.zero;
	private bool _isClicked = false;
	private bool _isFA = false;
	public float ManaRegenerationOver5Seconds = 10;
	private bool _firepressed = false;
	private float _lastSinceFire = 0.0f;
	public GameObject TPSCamera;
	public GameObject FPSCamera;
	public GameObject _rooted;
	private bool _isTPS = true;
	private bool _isCReleased = true;
	
	// Use this for initialization
	void Start () {
		if (Application.loadedLevel != 1)
		{
			if (PlayerPrefs.HasKey("maxHP"))
			{
			_maxHp = PlayerPrefs.GetFloat("maxHP");
			_curHp = _maxHp;
			}
			if (PlayerPrefs.HasKey("maxMP"))
			{
			_maxMp = PlayerPrefs.GetFloat("maxMP");
			_curMp = _maxMp;
			}
			
			if (PlayerPrefs.HasKey("speed"))
			{
			speed_ = PlayerPrefs.GetFloat("speed");
			}
			PlayerPrefs.SetInt("NbMonstre", 0);
		}
		animation["attack01"].speed = 1.9f;
		animation.CrossFade("idle");
		_name = _life.GetComponentInChildren<UILabel>();
		_name.text = PlayerPrefs.GetString("Pseudo");
	}
	
	public void hurt(int damage)
	{
		_curHp -= damage;
		audio.PlayOneShot(_attackSound);
		if (_curHp <= 0)
		{
			Application.LoadLevel(5);
		}
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
	
	private void update_move()
	{
		float vertical = Input.GetAxis ("Vertical");
		float horizontal = Input.GetAxis ("Horizontal");
		
		if (Mathf.Abs (vertical) > 0.1 || Mathf.Abs (horizontal) > 0.1)
		{
			_isClicked = false;
		}
		if (_isClicked == true)
			return ;
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
		this.update_animation();
	}
	
	// Updates lifebar
	protected void UpdateLife()
	{
		if (_hud)
		{
			_hud.transform.position = new Vector3(transform.position.x + 0.1f,
									transform.position.y + 3.8f,
									transform.position.z + 0.3f);
    		_hud.transform.rotation = Camera.main.transform.rotation;
		}
		if (_life)
		{
			_life.sliderValue = (float)(_curHp / _maxHp);
		}
		if (_mana)
		{
			_mana.sliderValue = (float)(_curMp / _maxMp);
		}
	}
	
	void fireFA()
	{
		 Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit hit;
		if(Physics.Raycast(ray, out hit)){
			animation.CrossFade("attack01");
			_isAttacking = true;
			_clickingPosition = hit.point;
			_currentRotationTime = 0.0f;
			_startRotation = transform.eulerAngles;
			transform.LookAt(_clickingPosition);
			_endRotation = transform.eulerAngles;
			_endRotation.x = 0;
			_endRotation.z = 0;
			transform.eulerAngles = _startRotation;
			_startRotation.x = 0;
			_startRotation.z = 0;
			if (Mathf.Abs(_startRotation.y + 360 - _endRotation.y) < Mathf.Abs(_startRotation.y - _endRotation.y))
				_startRotation.y += 360;
			if (Mathf.Abs(_startRotation.y - 360 - _endRotation.y) < Mathf.Abs(_startRotation.y - _endRotation.y))
				_startRotation.y -= 360;
			_currentRotationTime = 0;
			_isFA = true;
		}
	}
	
	void fire()
	{
		 Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit hit;
		if(Physics.Raycast(ray, out hit)){
			if (hit.transform.gameObject.GetComponent<MonsterScript>() == null && hit.transform.gameObject.GetComponent<bossScript>() == null && _isTPS)
			{
				_isClicked = true;
				_click = hit.point;
				_click.y = transform.position.y;
				transform.LookAt(_click);
			}
			else
			{
			animation.CrossFade("attack01");
			_isAttacking = true;
			_clickingPosition = hit.point;
			_currentRotationTime = 0.0f;
			_startRotation = transform.eulerAngles;
			transform.LookAt(_clickingPosition);
			_endRotation = transform.eulerAngles;
			_endRotation.x = 0;
			_endRotation.z = 0;
			transform.eulerAngles = _startRotation;
			_startRotation.x = 0;
			_startRotation.z = 0;
			if (Mathf.Abs(_startRotation.y + 360 - _endRotation.y) < Mathf.Abs(_startRotation.y - _endRotation.y))
				_startRotation.y += 360;
			if (Mathf.Abs(_startRotation.y - 360 - _endRotation.y) < Mathf.Abs(_startRotation.y - _endRotation.y))
				_startRotation.y -= 360;
			_currentRotationTime = 0;
				_isFA = false;
			}
		}
	}
	
	void attack()
	{
		GameObject o;
		if (_isFA)
		{
			o = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Fire Arrow"));
			_curMp -= 10;
		}
		else
			o = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Arrow"));
		Vector3 temp = transform.position;
			temp.y = 2;
			o.transform.position = temp;
			o.transform.LookAt(_clickingPosition);
		if (_isTPS)
			o.transform.eulerAngles = new Vector3(0, o.transform.eulerAngles.y, 0);
		audio.PlayOneShot(_fireSound);
	}
	
	void regenerateMana()
	{
		_curMp += ManaRegenerationOver5Seconds / 5 * Time.deltaTime;
		if (_curMp > _maxMp)
			_curMp = _maxMp;
	}
	
	void root()
	{
		
		 Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit hit;
		if(Physics.Raycast(ray, out hit)){
			MonsterScript __target = hit.transform.gameObject.GetComponent<MonsterScript>();
			if (__target && __target.Speed > 0)
			{
				_curMp -= 30;
				__target.setSpeed(0, 2);
				audio.PlayOneShot(_snareSound);
				return;
			}
			bossScript __target2 = hit.transform.gameObject.GetComponent<bossScript>();
			if (__target2 && __target2.Speed > 0)
			{
				_curMp -= 30;
				__target2.setSpeed(0, 2);
				audio.PlayOneShot(_snareSound);
				return;
			}
			
		}
	}
	
	/**
	 * This unit will be rooted for sec seconds
	 */
	public void rootFor(int sec)
	{
		lastSpeed = speed_;
		speed_ = 0;
	    rootDuration = (float)sec;
		animation.CrossFade("stun");
	}	
	
	public void isRooted()
	{
		if (speed_ == 0 && (rootDuration -= Time.deltaTime) < 0)
		{
			speed_ = lastSpeed;
		}
	}
	
	void heal()
	{
		if (_curHp != _maxHp)
		{
			_curHp += 20;
			if (_curHp > _maxHp)
			{
				_curHp = _maxHp;
			}
			_curMp -= 20;
				GameObject.Instantiate(Resources.Load("Prefabs/Effects/Heal"), transform.position, Quaternion.identity);
			audio.PlayOneShot(_healSound);
		}
	}
	
	void teleport()
	{
		
		 Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit hit;
		if(Physics.Raycast(ray, out hit)){
			if (hit.transform.gameObject.GetComponent<MonsterScript>() == null && hit.transform.gameObject.GetComponent<bossScript>() == null)
			{
				Vector3 pos = hit.point;
				pos.y = transform.position.y + 1;
				Vector3 charpos = transform.position;
				charpos.y += 1;
				Ray newray = new Ray(charpos, pos - charpos);
				if (Physics.Raycast(newray, out hit, (pos - charpos).magnitude) || (pos - charpos).magnitude > _maxTpRange)
				{
					return ;
				}
					_curMp -= 20;
				pos.y -= 1;
				GameObject.Instantiate(Resources.Load("Prefabs/Effects/Blink"), transform.position, Quaternion.identity);
				transform.position = pos;
				audio.PlayOneShot(_teleportSound);
			}
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		if (speed_ < 0.1f)
		{
			_rooted.SetActive(true);
		}
		else
		{
			_rooted.SetActive(false);
		}
		if (Input.GetKeyUp(KeyCode.C))
		{
			_isCReleased = true;
		}
		isRooted();
		_lastSinceFire += Time.deltaTime;
		if (Input.GetAxis("Fire1") < 0.5 || _lastSinceFire > 0.5)
			_firepressed = false;
		regenerateMana();
		rigidbody.velocity = Vector3.zero;
		if (_isAttacking)
		{
			_currentRotationTime += Time.deltaTime * 1000;
			animation.CrossFade("attack01");
			transform.eulerAngles = Vector3.Lerp(_startRotation, _endRotation, _currentRotationTime / 1000);
			if (_currentRotationTime > 1000.0f)
			{
				_isAttacking = false;
				attack();
			}
		}
		else if (_isClicked)
		{
			if ((_click - transform.position).magnitude <= speed_ * Time.deltaTime)
			{
				transform.position = _click;
				_isClicked = false;
				if (Random.Range(0, 100) < 50)
					animation.CrossFade("idle");
				else 
					animation.CrossFade("idle_alt");
			}
			else
			{
				animation["run"].speed = 1.0f;
				animation.CrossFade("run");
				transform.LookAt(_click);
				transform.Translate(Vector3.forward * speed_ * Time.deltaTime);
			}
		}
			this.update_move();
		
		if (!_isAttacking)
		{
			if (Input.GetKeyDown (KeyCode.Alpha3) && _curMp >= 20)
			{
					teleport();
			}
			else if (Input.GetKeyDown (KeyCode.Alpha2) && _curMp >= 30)
			{
				root();
			}
			else if (Input.GetKeyDown (KeyCode.Alpha1) && _curMp >= 10)
			{
				fireFA();
			}
			else if (Input.GetKeyDown (KeyCode.H) && _curMp >= 20)
			{
				heal();
			}
			else if (_firepressed == false)
			{		
				float fire = Input.GetAxis("Fire1");
				if (Mathf.Abs(fire) > 0.5)
				{
					this.fire();
					_firepressed = true;
					_lastSinceFire = 0.0f;
				}
			}
		}
		UpdateLife();
		
		if (Input.GetKeyDown(KeyCode.C) && _isCReleased)
		{
			_isCReleased = false;
			if (_isTPS)
			{
				TPSCamera.SetActive(false);
				FPSCamera.SetActive(true);
			}
			else
			{
				TPSCamera.SetActive(true);
				FPSCamera.SetActive(false);
			}
			_isTPS = !_isTPS;
		}
	}
}
