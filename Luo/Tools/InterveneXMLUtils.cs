using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Xml.Linq;
/********************************************************************************

** 类名称： InterveneXMLUtils

** 描述：干预措施功能接口

** 作者： hongzhenluo

** 创建时间：2019-10-09

*********************************************************************************/
public class InterveneXMLUtils
{
    private static string xmlPath = Application.streamingAssetsPath + "/Intervene/" + "InterveneList.xml";

    /*
     * 获取一级干预内容
     * @return 干预类的封装类
     * **/
    public static List<InterveneInfo> getLevelOne()
    {
        XElement xml = XElement.Load(xmlPath);
        var LevelOne = from e in xml.Elements("levelOne") select e;
        List<InterveneInfo> aList = new List<InterveneInfo>();
        foreach (var element in LevelOne) {
            InterveneInfo interveneInfo = new InterveneInfo();
            interveneInfo.Category = element.Attribute("name").Value;
            interveneInfo.Level = element.Attribute("level").Value;
            aList.Add(interveneInfo);
        }
        return aList;
    }
    /*
     * 根据类型获取干预详细内容
     * @Param: category 干预种类
     * @return: 干预类的封装类
     * **/
    public static List<InterveneInfo> getIntervenesContent(string category) {

        XElement xml = XElement.Load(xmlPath);
        List<InterveneInfo> list = new List<InterveneInfo>();
        var LevelOne = from e in xml.Elements("levelOne") where (e.Attribute("name").Value == category) select e;
        var contents = from e in LevelOne.First().Elements("content") select e;
        foreach (var element in contents) {
            //TODO:
            InterveneInfo interveneInfo = new InterveneInfo();
            interveneInfo.Category = category;
            interveneInfo.Content = element.Value;
            interveneInfo.Level = element.Attribute("level").Value;
            if (element.Attribute("equipment")!= null)
            {
                interveneInfo.EqupList = element.Attribute("equipment").Value;
            }
            else
            {
                Debug.Log(element.Attribute("equipment"));
            }
            list.Add(interveneInfo);
        }
        return list;
    }
    /*
     * 搜索干预措施
     * @Param text 搜索的关键字文本
     * @return  干预类的封装类
     * **/
    public static List<InterveneInfo> search(string text) {
        List<InterveneInfo> list = new List<InterveneInfo>();
        XElement xml = XElement.Load(xmlPath);
        var LevelOnes = from e in xml.Elements("levelOne") select e;
        foreach (var levelOne in LevelOnes) {
            if (levelOne.Attribute("name").Value.Contains(text)) {
                InterveneInfo obj = new InterveneInfo();
                obj.Level = levelOne.Attribute("level").Value;
                obj.Category = levelOne.Attribute("name").Value;
                list.Add(obj);
            }
            var contents = from e in levelOne.Elements("content") select e;
            foreach (var content in contents) {
                if (content.Value.Contains(text)) {
                    InterveneInfo obj = new InterveneInfo();
                    obj.Level = content.Attribute("level").Value;
                    obj.Category = levelOne.Attribute("name").Value;

                    obj.Content = content.Value;
                    if (content.Attribute("equipment") != null)
                    { 
                        obj.EqupList = content.Attribute("equipment").Value;
                    }
                    list.Add(obj);
                }
            }
        }
        return list;
    }
}

