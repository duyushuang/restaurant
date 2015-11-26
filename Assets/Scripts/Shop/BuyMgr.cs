using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuyMgr : MonoBehaviour {

	public GameObject[] cells;
	public GameObject item;

	public TweenPosition buyTween;
	public GameObject pack;

	public Transform player;
	public Transform SaleNPC;

	public static Dictionary<string, int> bagDic = new Dictionary<string, int>();

	public void Buy(GameObject product)
	{
		UISprite uis = product.GetComponent<UISprite>();

		string name  = uis.spriteName;

		for(int i = 0; i < cells.Length; i++)
		{
			if(cells[i].transform.childCount > 0)
			{
				MyDragandDropItem g = cells[i].GetComponentInChildren<MyDragandDropItem>();
				if(g.sprite.spriteName == name)
				{
					
					g.AddCount(1);
					bagDic[name]++;
					return;
				}
			}
		}
		
		for(int i = 0; i < cells.Length; i++)
		{
			if(cells[i].transform.childCount == 0)
			{
				GameObject go = NGUITools.AddChild(cells[i],item);
				go.GetComponent<UISprite>().spriteName = name;
				go.transform.localPosition = Vector3.zero;
				bagDic.Add(name, 1);
				break;
			}
		}
	}

	void Update()
	{
		if(Input.GetButtonDown("Fire1"))
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hit, 100))
			{
				if(hit.transform.gameObject.name == "SaleNPC" && Vector3.Distance(player.position, SaleNPC.position) < 1 )
				{
					buyTween.PlayForward();
					pack.SetActive(true);
				}
			}
		}
	}

	public void Close()
	{
		buyTween.PlayReverse();
		pack.SetActive(false);
	}
}






