using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System.Collections.Generic;

public class ConfigMgr{
	
	private static Dictionary<string, string> menuDic = new Dictionary<string, string>();

	public void LoadXml(string fileName)
	{
		//创建xml文档
		XmlDocument xml = new XmlDocument();
		XmlReaderSettings set = new XmlReaderSettings();
		set.IgnoreComments = true;//这个设置是忽略xml注释文档的影响。有时候注释会影响到xml的读取
		xml.Load(XmlReader.Create((Application.dataPath + "/Resources/Configs/" + fileName + ".xml"), set));
		//得到objects节点下的所有子节点
		XmlNodeList xmlNodeList = xml.SelectSingleNode(fileName).ChildNodes;
		//遍历所有子节点
		string contains;
		foreach(XmlElement xl1 in xmlNodeList)
		{
			string name = xl1.GetAttribute("name");
			contains = "";
			//继续遍历id为1的节点下的子节点
			foreach(XmlElement xl2 in xl1.ChildNodes)
			{
				contains += xl2.GetAttribute("quantity") + ":" + xl2.InnerText + ",";
			}
			menuDic.Add(name, contains);
		}
	}

	public static Dictionary<string, string> GetMenuDic()
	{
		return menuDic;
	}
}

