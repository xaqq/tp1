using UnityEngine;
using System.Collections;


public class cameraFollowPlayer : MonoBehaviour {
	public Transform target;
    public float distance;
	
	// Use this for initialization
	void Start () {
	
	}
	
    void Update(){
     
    transform.position = new Vector3(target.position.x - distance / 2,
									target.position.y + distance,
									target.position.z - distance / 2);
    transform.LookAt(target.position); 
    }
}
