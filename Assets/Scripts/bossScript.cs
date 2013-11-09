using UnityEngine;
using System.Collections;

public class bossScript : MonoBehaviour {
	private playerController _target = null;	
	public AudioClip _hurtSound;
	private int _maxHp;
	public int HealthPoints;
	public int AttackRange;
	public int Damage;
	public float AttackSpeed;
	public int Speed;
	public bool _isDestroyed = false;
	private int _defSpeed;
	private int _currentNavNode = 0;
	private float _currentDelay = 0.0f;
	private bool _isDelay = false;
	private bool _isAttackRecovery = false;
	private float _recoveryTimer = 0.0f;
	private float _speedRecoveryTimer = -1.0f;
	private float rootCooldown_ = 10.0f;
	private bool isRootOnCooldown_ = false;
		public bool canCharge = false;
	private float chargeCooldown = 0;
	
	
	//GUI
	public GameObject _hud;
	private UISlider _life;
	private UILabel _name;
		
	// Use this for initialization
	void Start () {
		_life = _hud.GetComponentInChildren<UISlider>();
		_name = _life.GetComponentInChildren<UILabel>();
		_name.text = "Boss";
		_maxHp = HealthPoints;
		_defSpeed = Speed;
		//this.GetComponentInChildren<Animation>()["attack_main"].speed = 1.5f;
	}
	
	public void SetTarget(playerController target)
	{
		_target = target;
	}
	
	public void hurt(int damage)
	{
		if (_isDestroyed)
			return;
		audio.PlayOneShot(_hurtSound);
		HealthPoints -= damage;
		if (HealthPoints <= 0)
		{
			_isDestroyed = true;
			this.GetComponentInChildren<Animation>().animation.Stop();
			this.GetComponentInChildren<Animation>().animation.CrossFade("pudge_death");
		}
	}
	
	public void setSpeed(int speed, int delay)
	{
		Speed = speed;
		_speedRecoveryTimer = delay;
	}
	
	// Updates lifebar
	protected void UpdateLife()
	{
		if (_hud)
		{
			_hud.transform.position = new Vector3(transform.position.x + 0.1f,
									transform.position.y + 8.85f,
									transform.position.z + 0.3f);
    		_hud.transform.rotation = Camera.main.transform.rotation;
		}
		if (_life)
		{
			_life.sliderValue = (float)((float)HealthPoints / (float)_maxHp);
		}
	}
	
	public void move_or_idle()
	{
		if (Speed > 0.1)
		{
			this.GetComponentInChildren<Animation>().animation.CrossFade("pudge_run_haste");
		}
		else{
			this.GetComponentInChildren<Animation>().animation.CrossFade("pudge_stun");
		}
	}
	
	
	
	
	void try_charge()
	{
		if (canCharge)
		{
			if (chargeCooldown <= 0)
			{
				print ("Charging");
				_speedRecoveryTimer += 3;
				chargeCooldown = Random.Range(7, 15);
				Speed *= 3;
				this.GetComponentInChildren<Animation>().animation["pudge_run_haste"].speed = 3;
			}
		}
	}
	
	
	// Update is called once per frame
	void Update () {
						if (isRootOnCooldown_)
				{
					rootCooldown_ += Time.deltaTime;
					if (rootCooldown_ >= 10)
					{
						isRootOnCooldown_ = false;
					}
				}
		
		UpdateLife();
		if (_isDestroyed)
		{
			if (this.GetComponentInChildren<Animation>().animation.IsPlaying("pudge_death"))
			{
				return;
			}
			NGUITools.Destroy(_life);
			Destroy(_hud);
			Destroy(gameObject);
			PlayerPrefs.SetInt ("NbMonstre", PlayerPrefs.GetInt("NbMonstre") + 1);
			return;
		}		
		
		chargeCooldown -= Time.deltaTime;
		if (_speedRecoveryTimer > 0)
		{
			_speedRecoveryTimer -= Time.deltaTime;
			if (_speedRecoveryTimer < 0)
			{
				Speed = _defSpeed;
				this.GetComponentInChildren<Animation>().animation["pudge_run_haste"].speed = 1;
			}
		}
		
		rigidbody.velocity = Vector3.zero;
		if (_isAttackRecovery && _target != null)
		{			
			this.GetComponentInChildren<Animation>().animation.CrossFade("pudge_attack1");
			_recoveryTimer += Time.deltaTime;
			if (_recoveryTimer > AttackSpeed)
			{			
				_target.hurt(Damage);
				_isAttackRecovery = false;
			}
		}
		else if (_target != null)
		{
			try_charge();
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
			{				
				if (!isRootOnCooldown_)
				{
					print("ROOT THE TARGET");
					_target.rootFor(3);
					isRootOnCooldown_ = true;
					rootCooldown_ = 0.0f;
				}
				transform.Translate(Vector3.forward * Speed * Time.deltaTime);				
				move_or_idle();
			}
		}
		else
		{
				transform.Translate(Vector3.forward * Speed * Time.deltaTime);
				move_or_idle();
		}
	}
}
