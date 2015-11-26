using UnityEngine;
using System.Collections;

public class CustomSpawn : MonoBehaviour {

	public GameObject[] customs;
	public float initTime = 5;
	public GameObject[] spawnPoints;

	void Start()
	{
		StartCoroutine(InitCustome());
	}

	IEnumerator InitCustome()
	{
		while(true)
		{
			yield return new WaitForSeconds(initTime);
			int i = Random.Range(0, customs.Length);
			int j = Random.Range(0, spawnPoints.Length);
			Instantiate(customs[i], spawnPoints[j].transform.position, Quaternion.identity);
		}
	}
}
