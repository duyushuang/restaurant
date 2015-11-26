using UnityEngine;
using System.Collections;

public class MainGameMgr : MonoBehaviour {

	public UILabel account;
	public UILabel level;

	void Awake()
	{
		InitMenus();
	}

	void Start () {

	}
	

	void Update () {
	
	}


	void InitMenus()
	{
		ConfigMgr con = new ConfigMgr();
		con.LoadXml("menus");
	}
}
