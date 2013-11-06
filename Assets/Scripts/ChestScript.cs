using UnityEngine;
using System.Collections;

public class ChestScript : MonoBehaviour {
	
	private bool isOpened = false;
	public GameObject key;
	// Use this for initialization
	void Start () {
	key.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider target)
	{
		var player = target.GetComponent<playerController>();
		
		if (!isOpened && player != null) 
		{
			key.SetActive(true);
			animation.CrossFade("Take 001");
			int rand = Random.Range(1, 4);
			switch (rand)
			{
			case 1:
				player._curHp += 20;
				player._maxHp += 20;
				break;
			case 2:
				player._curMp += 20;
				player._maxMp += 20;
				break;
			case 3:
				player.speed_ += 1;
				break;
			}
			isOpened = true;
		}
		
	}
}
