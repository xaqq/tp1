using UnityEngine;
using System.Collections;

public class EndZone : MonoBehaviour {
	
	public string endMob;
	public GameObject particles;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if (particles)
		{
			if (endMob != null && endMob != "")
			{
				if (! GameObject.Find(endMob))
				{
					particles.SetActive(true);
				}
			}
		}
	}
	
	void OnTriggerEnter(Collider collider)
	{
		playerController player = collider.gameObject.GetComponent<playerController>();
		if (player != null)
		{
			if (endMob != null && endMob != "")
			{
				if (GameObject.Find(endMob))
				{
					return ;
				}
			}
				PlayerPrefs.SetFloat("maxHP", player._maxHp);
				PlayerPrefs.SetFloat("maxMP", player._maxMp);
				PlayerPrefs.SetFloat("speed", player.speed_);
			
			Application.LoadLevel(Application.loadedLevel + 1);
		}
	}
}
