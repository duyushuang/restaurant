using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StartGame : MonoBehaviour {

	public GameObject[] userItems;
	private List<string> userAccounts = new List<string>();

	void Start()
	{
		SaveMgr.ReadHistoryAccountAndMoney();
		LoadHistory();
	}

	void LoadHistory()
	{
		int userCount = SaveMgr.GetHistoryAccountAndPwds().Count;
		foreach(string key in SaveMgr.GetHistoryAccountAndPwds().Keys)
		{
			userAccounts.Add(key);
		}
		for(int i = 0; i < userCount; i++)
		{
			userItems[i].SetActive(true);
			userItems[i].GetComponentInChildren<UILabel>().text = userAccounts[i];
		}
		for(int i = userCount; i < 5; i ++)
		{
			userItems[i].SetActive(false);
		}
	}

	public GameObject bg;
	public GameObject startCon;
	public GameObject loadCon;

	public static string account;
	public static string level;

	public void StartBtn()
	{
		bg.SetActive(true);
		startCon.SetActive(true);
	}

	public void LoadBtn()
	{
		bg.SetActive(true);
		loadCon.SetActive(true);
	}

	public void Close()
	{
		startCon.SetActive(false);
		loadCon.SetActive(false);
		bg.SetActive(false);
	}

	public void StartGameFun()
	{
		Application.LoadLevel("MainGame");
	}

	public void LoadGame(GameObject o)
	{
		account = o.GetComponent<UILabel>().text;
		level = SaveMgr.GetHistoryAccountAndPwds()[account];
		Application.LoadLevel("MainGame");
	}

	public void RemoveGame(GameObject o)
	{
		account = o.GetComponent<UILabel>().text;
		SaveMgr.RemoveHistoryAccount(account);
		LoadHistory();
	}
}
