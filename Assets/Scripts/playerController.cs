using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {
	public float speed_ = 10;
	public float rotationSpeed_ = 180;
	private UISlider _life;
	private UILabel _name;
	public float _curHp = 80;
	public float _maxHp = 100;
	private bool _isAttacking = false;
	private Vector3 _clickingPosition;
	private Vector3 _startRotation;
	private Vector3 _endRotation;
	private float _currentRotationTime = 0;
	public Transform arrow;
	
	// Use this for initialization
	void Start () {
		animation["attack01"].speed = 4.0f;
		animation.CrossFade("idle");
		_life = gameObject.GetComponentInChildren<UISlider>();
		_name = _life.GetComponentInChildren<UILabel>();
		_name.text = PlayerPrefs.GetString("Pseudo");
	}
	
	public void hurt(int damage)
	{
		_curHp -= damage;
		if (_curHp <= 0)
			Destroy(gameObject);
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
		if (_life)
		{
			_life.transform.position = new Vector3(transform.position.x - 0.5f,
									transform.position.y + 1.85f,
									transform.position.z + 0.3f);
    		_life.transform.rotation = Camera.main.transform.rotation;
			
			
			
			//_life.transform.LookAt(Camera.main.transform);
			Vector3 posTmp = _life.transform.eulerAngles;
			posTmp.z = 190;
			//_life.transform.eulerAngles = posTmp;
			
			_life.sliderValue = (float)(_curHp / _maxHp);
		}
	}
	
	void fire()
	{
		 Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit hit;
		if(Physics.Raycast(ray, out hit)){
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
		}
	}
	
	void attack()
	{
		GameObject o = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Arrow"));
		Vector3 temp = transform.position;
		temp.y = 1;
			o.transform.position = temp;
			o.transform.LookAt(_clickingPosition);
			o.transform.eulerAngles = new Vector3(0, o.transform.eulerAngles.y, 0);
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody.velocity = Vector3.zero;
		if (_isAttacking)
		{
			_currentRotationTime += Time.deltaTime * 1000;
			animation.CrossFade("attack01");
			transform.eulerAngles = Vector3.Lerp(_startRotation, _endRotation, _currentRotationTime / 500);
			if (_currentRotationTime > 500.0f)
			{
				_isAttacking = false;
				attack();
			}
		}
		else
		{
			this.update_move();
		}
		UpdateLife();
		if (!_isAttacking)
		{
		float fire = Input.GetAxis("Fire1");
		
		if (Mathf.Abs(fire) > 0.5)
		{
			this.fire();
			animation.CrossFade("attack01");
		}
		}
		}
}
