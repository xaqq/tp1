using UnityEngine;
using System.Collections;

public class DeathHandler : MonoBehaviour {
	
	public GameObject _reset;
	public GameObject _quit;
	
	void Awake() {
	
		UIEventListener.Get(_quit).onClick += OnClickQuit;
		UIEventListener.Get(_reset).onClick += OnClickReset;
			
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	void OnClickReset(GameObject go)
	{
		Application.LoadLevel("Level1");
	}
	
	void OnClickQuit(GameObject go)
	{
		Application.Quit();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
