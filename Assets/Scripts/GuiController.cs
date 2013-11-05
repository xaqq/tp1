using UnityEngine;
using System.Collections;

public class GuiController : MonoBehaviour {
	public GameObject _player;
	public GameObject _info;
	public GameObject _menu;
	public GameObject _menuIG;
	public GameObject _fireArrow;
	public GameObject _snare;
	public GameObject _blink;
	public GameObject _heal;
	public UILabel _lifeLabel;
	public UILabel _manaLabel;
	public UILabel _time;
	public UILabel _nbMonster;
	public UISlider _lifeBar;
	public UISlider _manaBar;
	public GameObject _tooltip;
	private playerController _playerController;
	private System.DateTime _tmpTime;
	
	void Awake() {
		UIEventListener.Get(_info).onHover += OnHoverInfo;
		UIEventListener.Get(_fireArrow).onHover += OnHoverFireArrow;
		UIEventListener.Get(_snare).onHover += OnHoverSnare;
		UIEventListener.Get(_blink).onHover += OnHoverBlink;
		UIEventListener.Get(_heal).onHover += OnHoverHeal;
		UIEventListener.Get(_menu).onClick += OnClickMenu;
	}
	// Use this for initialization
	void Start () {
		_nbMonster.text = "0";
		_time.text = "00:00";
		_playerController = _player.GetComponent<playerController>();
		_lifeLabel.text = _playerController._curHp + "/" + _playerController._maxHp;
		_manaLabel.text = _playerController._curMp + "/" + _playerController._maxMp;
		_manaBar.sliderValue = (float)(_playerController._curMp / _playerController._maxMp);
		_lifeBar.sliderValue = (float)(_playerController._curHp / _playerController._maxHp);
	}
	
	void OnClickMenu(GameObject go)
	{
		if (_menuIG.activeSelf)
			_menuIG.SetActiveRecursively(false);
		else
			_menuIG.SetActiveRecursively(true);
	}
	
	
	void OnHoverInfo(GameObject _obj, bool pressed)
	{
		if (pressed == true)
		{
			_tooltip.SetActiveRecursively(true);
			_tooltip.GetComponentInChildren<UILabel>().text = "You are a rogue. Armed with your bow, you need to fight your way through the dungeon without fading to the ennemies! Good luck, Champion!";
		}
		else
		{
			_tooltip.SetActiveRecursively(false);
		}
	}
	
	void OnHoverFireArrow(GameObject _obj, bool pressed)
	{
		if (pressed == true)
		{
			_tooltip.SetActiveRecursively(true);
			_tooltip.GetComponentInChildren<UILabel>().text = "Fire Arrow : Press 1 while targeting an ennemy to deal extra fire damage!";
		}
		else
		{
			_tooltip.SetActiveRecursively(false);
		}
	}
	
	void OnHoverHeal(GameObject _obj, bool pressed)
	{
		if (pressed == true)
		{
			_tooltip.SetActiveRecursively(true);
			_tooltip.GetComponentInChildren<UILabel>().text = "Heal : Heal yourself for a small amount of life, this can ALWAYS be useful versus the forces of evil!";
		}
		else
		{
			_tooltip.SetActiveRecursively(false);
		}
	}
	
	void OnHoverBlink(GameObject _obj, bool pressed)
	{
		if (pressed == true)
		{
			_tooltip.SetActiveRecursively(true);
			_tooltip.GetComponentInChildren<UILabel>().text = "Blink : You need to escape a corner or a trap made by your ennemies? Blink away by mousing over a direction and pressing 3!";
		}
		else
		{
			_tooltip.SetActiveRecursively(false);
		}
	}
	
	void OnHoverSnare(GameObject _obj, bool pressed)
	{
		if (pressed == true)
		{
			_tooltip.SetActiveRecursively(true);
			_tooltip.GetComponentInChildren<UILabel>().text = "Snare : Target an ennemy while pressing 2 to snare him during a few seconds! Remember, you are an archer, keep your distance from those filthy creatures!";
		}
		else
		{
			_tooltip.SetActiveRecursively(false);
		}
	}
	
	void UpdateTopMenu()
	{
		_tmpTime = System.DateTime.Now;
		_time.text = _tmpTime.Hour.ToString() + ":" + _tmpTime.Minute.ToString();
		_nbMonster.text = _playerController.getNbMonsterKilled().ToString();
	}
	
	void UpdateBottomMenu()
	{
		_lifeLabel.text = (int)_playerController._curHp + "/" + (int)_playerController._maxHp;
		_manaLabel.text = (int)_playerController._curMp + "/" + (int)_playerController._maxMp;
		_manaBar.sliderValue = (float)(_playerController._curMp / _playerController._maxMp);
		_lifeBar.sliderValue = (float)(_playerController._curHp / _playerController._maxHp);
	}
	
	// Update is called once per frame
	void Update () {
		UpdateTopMenu();
		UpdateBottomMenu();
	}
}
