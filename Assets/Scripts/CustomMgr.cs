using UnityEngine;
using System.Collections;

public class CustomMgr : MonoBehaviour {

	public GameObject[] eatPoints;
	public float moveSpeed = 2;

	private Animator ani;
	private bool needMove = false;
	private int dirPoint;
	// Use this for initialization
	void Start () {
		eatPoints = GameObject.FindGameObjectsWithTag("Chair");
		print(eatPoints.Length);
		ani = GetComponent<Animator>();
		while(true)
		{
			int i = Random.Range(0, eatPoints.Length);
			if(!eatPoints[i].GetComponent<EatPoint>().isFree)
			{
				eatPoints[i].GetComponent<EatPoint>().isFree = true;
				dirPoint = i;
				needMove = true;
				break;
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		if(needMove)
		{
			MoveToPoint(new Vector3(eatPoints[dirPoint].transform.position.x, transform.position.y, eatPoints[dirPoint].transform.position.z));
		}
	}


	public void MoveToPoint(Vector3 point)
	{
		//获取在同一平面（y轴不变）上的方向
		Vector3 p = new Vector3 (point.x,transform.position.y,point.z);
		
		float distance = Vector3.Distance (p,transform.position);
		if (distance < 0.05f)
		{
			needMove = false;
			transform.position = p;
			transform.LookAt(eatPoints[dirPoint].GetComponent<EatPoint>().lookPoint.transform.position);
			ani.SetBool("Walk", false);
			return;
		}
		ani.SetBool("Walk", true);	
		
		//通过差值旋转
		Quaternion wantedRot = Quaternion.LookRotation (p - transform.position);
		transform.rotation = Quaternion.Lerp(transform.rotation,wantedRot,10 * Time.deltaTime);
		
		//通过差值移动
		Vector3 dir = (p - transform.position).normalized;
		transform.Translate (dir * Time.deltaTime * moveSpeed,Space.World);
	}
}
