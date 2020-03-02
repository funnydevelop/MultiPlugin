
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Xml.Linq;
/********************************************************************************

** 类名称： MedicineXMLUtils

** 描述：药物列表接口

** 作者： hongzhenluo

** 创建时间：2019-10-08

*********************************************************************************/
public class MedicineXMLUtils
{
    private static string xmlPath = Application.streamingAssetsPath + "/Medicine/" + "MedicineList.xml";
    /*
    *  获取一级列表
    *  @Return 一级列表内容信息
    * **/
    public static List<MedicineListInfo> getLevelOneList()
    {
        XElement xml = XElement.Load(xmlPath);
        var LevelA = from e in xml.Elements("levelA") select e;
        List<MedicineListInfo> alist = new List<MedicineListInfo>();
        foreach (var element in LevelA)
        {
            MedicineListInfo obj = new MedicineListInfo();
            obj.LevelAName = element.Attribute("name").Value;
            obj.Type = element.Attribute("level").Value;
            alist.Add(obj);
        }
        return alist;
    }
    /*
     * 
     *  获取二级列表
     *  @Param levelOneText: 选中的一级列表的item内容
     *  @Return 二级列表内容信息
     * **/
    public static List<MedicineListInfo> getLevelTwoPageList(string levelOneText)
    {
        XElement xml = XElement.Load(xmlPath);
        var LevelA = from e in xml.Elements("levelA") where (e.Attribute("name").Value == levelOneText) select e;
        var LevelB = from e in LevelA.First().Elements("levelB") select e;
        List<MedicineListInfo> blist = new List<MedicineListInfo>();
        foreach (var element in LevelB)
        {
            MedicineListInfo obj = new MedicineListInfo();
            obj.LevelAName = levelOneText;
            obj.LevelBName = element.Attribute("name").Value;
            obj.Type = element.Attribute("level").Value;
            blist.Add(obj);
        }
        return blist;
    }
    /*
    *  获取药物列表
    *  @Param levelOneText: 选中的一级和二级列表的item内容
    *  @Return 药物列表内容信息
    * **/
    public static List<MedicineListInfo> getMedicinePageList(string levelOneText, string levelTwoText)
    {
        XElement xml = XElement.Load(xmlPath);
        var LevelA = from e in xml.Elements("levelA") where (e.Attribute("name").Value == levelOneText) select e;
        var LevelB = from e in LevelA.First().Elements("levelB") where(e.Attribute("name").Value == levelTwoText) select e;
        var medicines = from e in LevelB.First().Elements("medicine") select e;
        List<MedicineListInfo> medicineList = new List<MedicineListInfo>();
        foreach (var element in medicines) {
            MedicineListInfo obj = new MedicineListInfo();
            obj.LevelAName = levelOneText;
            obj.LevelBName = levelTwoText;
            obj.Type = element.Attribute("level").Value;
            obj.Medicine = element.Value;
            medicineList.Add(obj);
        }
        return medicineList;
    }
    /*
     * 全局搜索
     * @Param text 关键字
     * @Return 所有相关结果集合
     * **/
    public static List<MedicineListInfo> search(string text)
    {
        XElement xml = XElement.Load(xmlPath);
        List<MedicineListInfo> fullSearchList = new List<MedicineListInfo>();
        var LevelAS = from e in xml.Elements("levelA") select e;
        foreach (var levelA in LevelAS) { //level one
            if (levelA.Attribute("name").Value.Contains(text)) {
                MedicineListInfo obj = new MedicineListInfo();
                obj.Type = levelA.Attribute("level").Value;
                obj.LevelAName = levelA.Attribute("name").Value;
                fullSearchList.Add(obj);
            }
            var LevelBS = from e in levelA.Elements("levelB") select e;
            foreach(var levelB in LevelBS) { // level two
                if (levelB.Attribute("name").Value.Contains(text)) {
                    MedicineListInfo obj = new MedicineListInfo();
                    obj.Type = levelB.Attribute("level").Value;
                    obj.LevelAName = levelA.Attribute("name").Value;
                    obj.LevelBName = levelB.Attribute("name").Value;
                    fullSearchList.Add(obj);
                }
                var medicines = from e in levelB.Elements("medicine") select e;
                foreach (var element in medicines) { //medicine
                    if (element.Value.Contains(text)) {
                        MedicineListInfo obj = new MedicineListInfo();
                        obj.Type = element.Attribute("level").Value;
                        obj.LevelAName = levelA.Attribute("name").Value;
                        obj.LevelBName = levelB.Attribute("name").Value;
                        obj.Medicine = element.Value;
                        fullSearchList.Add(obj);
                    }
                }
            }
        }
        return fullSearchList;
    }
}

