using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {
	
	private Transform player;
	
	public float speed = 2;

	void Start () {
		player = GameObject.Find("Player").transform;
	}

	void Update () {

		Vector3 targetPos = player.position + new Vector3(-2f, 1.35f, 0);
		transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
//		Quaternion targetRotation = Quaternion.LookRotation( player.position-transform.position );
//		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
		
	}
}
