using UnityEngine;
using System.Collections;

public class MenuHandler : MonoBehaviour {

	public GameObject _pseudo;
	public GameObject _button;
	public GameObject _door;
	private bool	_launchGame = false;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (_launchGame)
		{
			if (!_door.animation.IsPlaying("Take 001"))
				Application.LoadLevel(1);
		}
	}
	
	void OnSubmit(GameObject go)
	{
		if (_pseudo.GetComponent<UIInput>().text != "" && _pseudo.GetComponent<UIInput>().text != "pseudo")
		{
			_launchGame = true;
			_door.animation.CrossFade("Take 001");
			PlayerPrefs.SetString("Pseudo", _pseudo.GetComponent<UIInput>().text);
		}
	}
}
