using UnityEngine;
using System.Collections;

public class EndZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider collider)
	{
		playerController player = collider.gameObject.GetComponent<playerController>();
		if (player != null)
		{
			PlayerPrefs.SetFloat("maxHP", player._maxHp);
			PlayerPrefs.SetFloat("maxMP", player._maxMp);
			PlayerPrefs.SetFloat("speed", player.speed_);
			Application.LoadLevel(Application.loadedLevel + 1);
		}
	}
}
