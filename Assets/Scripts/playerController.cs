using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {
	public float speed_ = 10;
	public float rotationSpeed_ = 180;
	private UISlider _life;
	public float _curHp = 80;
	public float _maxHp = 100;
	public Transform arrow;
	
	// Use this for initialization
	void Start () {
		animation.CrossFade("idle");
		_life = gameObject.GetComponentInChildren<UISlider>();
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
			print ("OK");
			GameObject o = (GameObject)GameObject.Instantiate(arrow.gameObject, transform.position, transform.rotation));
			o.transform.LookAt(hit.point);
		}
	}
	
	// Update is called once per frame
	void Update () {
		this.update_move();
		UpdateLife();
		float fire = Input.GetAxis("Fire1");
		
		if (Mathf.Abs(fire) > 0.5)
		{
			this.fire();
			animation.CrossFade("attack01");
		}
		}
}
