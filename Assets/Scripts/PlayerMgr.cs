using UnityEngine;
using System.Collections;

public class PlayerMgr : MonoBehaviour {

	public float rotateSpeed = 20;
	public float moveSpeed = 5;
	private Vector3 targetPoint;
	private Animator ani;

	public Camera uiCamera;

	public static string userAccount;
	public static string userLevel;

	void Start()
	{
		targetPoint = transform.position;
		ani = GetComponent<Animator>();
		userAccount = StartGame.account;
		userLevel = StartGame.level;
	}
	
	void Update () {
		//获取鼠标点击的点
		if(Input.GetButtonDown("Fire1"))
		{
			RaycastHit hit;
			RaycastHit uiHit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Ray uiRay = uiCamera.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hit, 100))
			{
				if(hit.transform.gameObject.name == "HexFloorTilePrefab")
				{
					if(Physics.Raycast(uiRay, out uiHit))
					{
						if(uiHit.transform.gameObject.name != "UIRoot")
						{
							return;
						}
					}
					targetPoint = hit.point;
				}
			}
		}
		//移动函数
		MoveToPoint (targetPoint);
	}
	
	public void MoveToPoint(Vector3 point)
	{
		//获取在同一平面（y轴不变）上的方向
		Vector3 p = new Vector3 (point.x,transform.position.y,point.z);
		
		float distance = Vector3.Distance (p,transform.position);
		if (distance < 0.25f)
		{
			ani.SetBool("Walk", false);
			return;
		}
		ani.SetBool("Walk", true);	
		
		//通过差值旋转
		Quaternion wantedRot = Quaternion.LookRotation (p - transform.position);
		transform.rotation = Quaternion.Lerp(transform.rotation,wantedRot,rotateSpeed * Time.deltaTime);
		
		//通过差值移动
		Vector3 dir = (p - transform.position).normalized;
		transform.Translate (dir * Time.deltaTime * moveSpeed,Space.World);
	}

	void OnApplicationQuit()
	{
		SaveMgr.AddNewHistoryAccount(userAccount, userLevel);
	}
	
}
