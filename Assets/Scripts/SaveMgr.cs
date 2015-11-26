using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SaveMgr {
	
	private const int MAX_RECORD_HISTORY_ACCOUNT_CNT = 5;
	private static Dictionary<string, string> accountAndLevels = new Dictionary<string, string>();
	
	private static LinkedList<string> accountSortedList = new LinkedList<string>();
	
	public class UserInfo
	{
		public string account;
		public string level;
	}
	
	public static UserInfo GetRecentAccount()
	{
		UserInfo last = new UserInfo();
		if (accountSortedList.First == null)
			return last;
		last.account = accountSortedList.First.Value;
		last.level = accountAndLevels[accountSortedList.First.Value];
		return last;
	}
	
	public static Dictionary<string, string> GetHistoryAccountAndPwds()
	{
		return accountAndLevels;
	}
	
	public static void ReadHistoryAccountAndMoney()
	{
		accountAndLevels.Clear();
		accountSortedList.Clear();
		for (int i = 0; i < MAX_RECORD_HISTORY_ACCOUNT_CNT; ++i )
		{
			string key = "Account" + i.ToString();
			if (PlayerPrefs.HasKey(key))
			{
				string account = PlayerPrefs.GetString(key, "");
				if(string.IsNullOrEmpty(account))
				{
					break;
				}
				string level = PlayerPrefs.GetString("level" + i.ToString(), "");
				accountAndLevels[account] = level;
				accountSortedList.AddLast(account);
			}
			else
			{
				break;
			}
		}
	}
	
	public static void AddNewHistoryAccount(string account, string level, bool save = true)
	{
		if (string.IsNullOrEmpty(account))
		{
			return;
		}
		accountAndLevels[account] = level;
		accountSortedList.Remove(account);
		accountSortedList.AddFirst(account);
		
		if(accountSortedList.Count > MAX_RECORD_HISTORY_ACCOUNT_CNT)
		{
			accountAndLevels.Remove(accountSortedList.Last.Value);
			accountSortedList.RemoveLast();
		}
		
		if(save)
		{
			SaveHistoryAccountAndLevel();
		}
	}
	
	public static void RemoveHistoryAccount(string account, bool save = true)
	{
		if(!accountAndLevels.ContainsKey(account))
		{
			return;
		}
		accountAndLevels.Remove(account);
		accountSortedList.Remove(account);
		
		if (save)
		{
			SaveHistoryAccountAndLevel();
		}
	}
	
	public static void SaveHistoryAccountAndLevel()
	{
		for (int i = accountSortedList.Count; i < MAX_RECORD_HISTORY_ACCOUNT_CNT; ++i)
		{            
			PlayerPrefs.DeleteKey("Account" + i.ToString());
			PlayerPrefs.DeleteKey("Level" + i.ToString());
			
		}
		int index = 0;
		foreach(string account in accountSortedList)
		{
			PlayerPrefs.SetString("Account" + index.ToString(), account);
			PlayerPrefs.SetString("Level" + index.ToString(), accountAndLevels[account]);
			
			++index;
		}
		
		PlayerPrefs.Save();
	}
}
