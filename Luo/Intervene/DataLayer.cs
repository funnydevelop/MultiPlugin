using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class DataLayer{

    //节点名字
    public string nodeName { get; set; }
    //节点的值
    public string value { get; set; }
    //节点的属性
    public Dictionary<string, string> attribute { get; set; }
    //父节点
    public DataLayer parent { get; set; }
    //层级
    public int level { get; set; }  


}

