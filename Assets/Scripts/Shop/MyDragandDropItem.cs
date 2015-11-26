using UnityEngine;
using System.Collections;

public class MyDragandDropItem : UIDragDropItem {
	
	public UISprite sprite;
	public UILabel label;
	private int count = 1;
	
	public void AddCount(int number = 1)
	{
		count += 1;
		label.text = count + "";
	}
	
	protected override void OnDragDropRelease (GameObject surface)
	{
		base.OnDragDropRelease (surface);

		if(surface.tag == "Key")
		{
			Transform parent = surface.transform.parent;

			surface.transform.parent = transform.parent;
			surface.transform.localPosition = Vector3.zero;

			transform.parent = parent;
			transform.localPosition = Vector3.zero;
		}
		else if(surface.tag == "Cell")
		{
			transform.parent = surface.transform;
			transform.localPosition = Vector3.zero;
		}
		else
		{
			transform.localPosition = Vector3.zero;
		}
		
	}
}
