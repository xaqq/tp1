using UnityEngine;
using System.Collections;

public class MenuHandler : MonoBehaviour {

	public GameObject _pseudo;
	public GameObject _button;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnSubmit(GameObject go)
	{
		print ("HUEHUEHUEHUEHUEHUEUHUUHEUH");
		if (_pseudo.GetComponent<UIInput>().text != "" && _pseudo.GetComponent<UIInput>().text != "pseudo")
			Application.LoadLevel(1);
	}
}
