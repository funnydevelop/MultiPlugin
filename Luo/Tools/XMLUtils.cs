using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

/********************************************************************************

** 类名称： XMLUtils

** 描述：日志模块中间件接口

** 作者： hongzhenluo

** 创建时间：2019-09-20

*********************************************************************************/

public class XMLUtils
{
    private static string currentLogFile = "";

    public static void setCurrentLogFile(string path)
    {
        currentLogFile = path;
    }

    /*
    * 创建日志文件
    * **/
    public static void CreateLogListFile()
    {
        string username = GlobalData.username; //赋值全局的用户名
        if (username != null)
        {
            if (username.Trim() == "")
            {
                Debug.Log("[InitLogInfo] Username was Empty!");
                username = "Template";
            }
        }
        string fileName = username+ DateTime.Now.Ticks+".xml" ;
        Debug.Log("[文件名] "+fileName+"[路径名] "+ Application.streamingAssetsPath + "/LogCenter/" + username + "/");
        Debug.Log(Application.streamingAssetsPath + "/LogCenter/" + username + "/");
        CreateLogListFile(Application.streamingAssetsPath + "/LogCenter/" + username + "/", fileName);
        LogListInfo logInfo = new LogListInfo();
        CaseData  currentCase = GlobalData.selectCase;
        logInfo.CaseName = currentCase.CaseTitle;
        logInfo.Name = GlobalData.name;
        logInfo.Username = GlobalData.username;
        logInfo.StudentId = GlobalData.stuID;
        logInfo.DateTime = DateTime.Now;
        initListFileNode(logInfo);
    }
    /*
     * 日志列表操作API
     * @Param  path:目录<日志根目录+用户名 e.g.\Assets\StreamingAssets\LogCenter\username\>
     * @Param  filename: 文件名<用户名+当前时间 e.g. username201902162300.xml>
     **/
    public static void CreateLogListFile(string path, string filename)
    {
        Debug.Log("创建操作日志文件...");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        XDocument xmldoc = new XDocument(
            new XDeclaration("1.0", "utf-8", "yes"),
            new XElement(
                "logManager"
            )
        );
        string fullPath = path + filename;
        //当创建xml文件时 定位当前文件全路径
        currentLogFile = fullPath;
        xmldoc.Save(fullPath);
    }
   
    /*
     * 插入XML日志列表节点
     * @Param LogListInfo  用户操作日志基本信息
     * @return 如果文件不存在或者路径不正确返回false 
     **/
    public static bool initListFileNode(LogListInfo loginfo, string fullpath = "")
    {
        if (!File.Exists(currentLogFile))
        {
            return false;
        }
        loginfo.Id = UUID.generate();
        loginfo.Duration = 0;
        loginfo.Path = currentLogFile;
        string time = DateTime.Now.ToString("HH:mm:ss");
        XElement xml = XElement.Load(currentLogFile);
        XElement logDetails = new XElement("logDetails",
            new XAttribute("id", loginfo.Id),
            new XElement("username", loginfo.Username),
            new XElement("caseName", loginfo.CaseName),
            new XElement("beginTime", loginfo.DateTime.ToString("yyyy-MM-dd HH:mm")),
            new XElement("duration", loginfo.Duration),
            new XElement("name", loginfo.Name),
            new XElement("studentId", loginfo.StudentId),
            new XElement("path", loginfo.Path));
        XElement logs = new XElement("Logs",
            new XElement("Log",
                new XElement("type", 0),
                new XElement("time", time),
                new XElement("content", "开始"))
            );
        xml.Add(logDetails);
        xml.Add(logs);
        xml.Save(currentLogFile);
        return true;
    }

    /*
     * 读取日志列表XML文件接口,如果没有获取到用户名则返回所有
     * @Param  xmlRootPath: 项目日志存放根目录[\Assets\StreamingAssets\LogCenter\]
     * @Param  username: 默认为"",以便向后扩展单人Log统计
     **/
    public static List<LogListInfo> readXMLListFile(string xmlRootPath, string username = "")
    {
        List<LogListInfo> plist = new List<LogListInfo>(); 
        DirectoryInfo drInfo = new DirectoryInfo(xmlRootPath);
        DirectoryInfo[] subDr = drInfo.GetDirectories();
        if (username.Equals(""))
        { 
            username = GlobalData.username;
        }
        if (username == null)
        {
            plist = readXMLListFileAllUser(xmlRootPath);
        }
        else
        {
            if (username.Equals(""))
            {
                plist = readXMLListFileAllUser(xmlRootPath);
            }
            else
            {
                foreach (DirectoryInfo subDir in drInfo.GetDirectories())
                {
                    if (subDir.Name.Equals(username))
                    {
                        string subFile = subDir.FullName + @"\";
                        DirectoryInfo subDrInfo = new DirectoryInfo(subFile);
                        FileInfo[] fileInfo = subDrInfo.GetFiles();
                        foreach (FileInfo f in fileInfo)
                        {
                            List<LogDetailInfo> ldlist = new List<LogDetailInfo>();
                            if (f.Extension == ".xml" || f.Extension == ".XML")
                            {
                                XElement xlm = XElement.Load(f.FullName);
                                var elements = from e in xlm.Elements("logDetails") select e;
                                foreach (var element in elements)
                                {
                                    LogListInfo logListinfo = new LogListInfo();
                                    logListinfo.Id = element.Attribute("id").Value;
                                    logListinfo.Name = element.Element("name").Value;
                                    logListinfo.CaseName = element.Element("caseName").Value;
                                    logListinfo.StudentId = element.Element("studentId").Value;
                                    logListinfo.Path = element.Element("path").Value;
                                    logListinfo.Duration = int.Parse(element.Element("duration").Value);
                                    logListinfo.DateTime = Convert.ToDateTime(element.Element("beginTime").Value);
                                    logListinfo.Username = element.Element("username").Value;

                                    var logs = from e in xlm.Elements("Logs") select e;
                                    var logelements = from e in logs.First().Elements("Log") select e;
                                    foreach (var logelement in logelements)
                                    {
                                        LogDetailInfo logDetailInfo = new LogDetailInfo();
                                        logDetailInfo.Type = int.Parse(logelement.Element("type").Value);
                                        logDetailInfo.Time = logelement.Element("time").Value;
                                        logDetailInfo.Content = logelement.Element("content").Value;
                                        ldlist.Add(logDetailInfo);
                                    }
                                    logListinfo.LogOptlist = ldlist;
                                    plist.Add(logListinfo);
                                }
                            }
                        }
                    }
                }
            }
        }
        
        return plist;
    }
    /*
     * 获取所有用户名下的log信息
     * @return  List<LogListInfo>
     * **/
    public static List<LogListInfo> readXMLListFileAllUser(string xmlRootPath) {
        
        List<LogListInfo> plist = new List<LogListInfo>();
        DirectoryInfo drInfo = new DirectoryInfo(xmlRootPath);
        DirectoryInfo[] subDr = drInfo.GetDirectories();
        foreach (DirectoryInfo subDir in drInfo.GetDirectories())
        {
            string subFile = subDir.FullName + @"\";
            DirectoryInfo subDrInfo = new DirectoryInfo(subFile);
            FileInfo[] fileInfo = subDrInfo.GetFiles();
            foreach (FileInfo f in fileInfo)
            {
                List<LogDetailInfo> ldlist = new List<LogDetailInfo>();
                if (f.Extension == ".xml" || f.Extension == ".XML")
                {
                    XElement xlm = XElement.Load(f.FullName);
                    var elements = from e in xlm.Elements("logDetails") select e;
                    foreach (var element in elements)
                    {
                        LogListInfo logListinfo = new LogListInfo();
                        logListinfo.Id = element.Attribute("id").Value;
                        logListinfo.Name = element.Element("name").Value;
                        logListinfo.CaseName = element.Element("caseName").Value;
                        logListinfo.StudentId = element.Element("studentId").Value;
                        logListinfo.Path = element.Element("path").Value;
                        logListinfo.Duration = int.Parse(element.Element("duration").Value);
                        logListinfo.DateTime = Convert.ToDateTime(element.Element("beginTime").Value);
                        logListinfo.Username = element.Element("username").Value;

                        var logs = from e in xlm.Elements("Logs") select e;
                        var logelements = from e in logs.First().Elements("Log") select e;
                        foreach (var logelement in logelements)
                        {
                            LogDetailInfo logDetailInfo = new LogDetailInfo();
                            logDetailInfo.Type = int.Parse(logelement.Element("type").Value);
                            logDetailInfo.Time = logelement.Element("time").Value;
                            logDetailInfo.Content = logelement.Element("content").Value;
                            ldlist.Add(logDetailInfo);
                        }
                        logListinfo.LogOptlist = ldlist;
                        plist.Add(logListinfo);
                    }
                }
            }
        }
        return plist;
    }

    /*
     * 插入操作日志信息
     * @Param type 0-医疗操作日志 1-治疗日志
     * @Param operation 用户操作日志
     * @tempPath 默认为"" 如果currentLogFile为null可以手动设置自定义路径(扩展)
     * **/
    public static void insertLogOpertaion(int type, string operation, string tempPath = "")
    {
        string time = DateTime.Now.ToString("HH:mm:ss");
        XElement xml = XElement.Load(currentLogFile);
        IEnumerable<XElement> element = from e in xml.Elements("Logs") select e;
        if (element.Count() > 0)
        {
            XElement Logs = element.First();
            XElement log = new XElement("Log",
                            new XElement("type", type),
                            new XElement("time", time),
                            new XElement("content", operation)
            );
            Logs.Add(log);
        }
        xml.Save(currentLogFile);
    }
    /*
        * 模糊查询日志操作内容
        * @Param: 操作内容
        * **/
    public static List<LogDetailInfo> selectLogDetailInfoWithFuzzyQuery(string operation)
    {
        List<LogDetailInfo> plist = new List<LogDetailInfo>();
        XElement xml = XElement.Load(currentLogFile);
        var Logs = from e in xml.Elements("Logs") select e;
        var Log = from e in Logs.First().Elements("Log") where (e.Element("content").Value.Contains(operation)) select e;
        foreach (var element in Log)
        {
            LogDetailInfo logDetailInfo = new LogDetailInfo();
            logDetailInfo.Type = int.Parse(element.Element("type").Value);
            logDetailInfo.Time = element.Element("time").Value;
            logDetailInfo.Content = element.Element("content").Value;
            plist.Add(logDetailInfo);
        }
        return plist;
    }

    /*
        *  结束日志操作<更新日志列表>
        *  @return 成功true 没有获取到元素 false
        * **/
    public static bool finishOperation(string fullpath = "")
    {
        Debug.Log(currentLogFile);
        insertLogOpertaion(0, "结束");
        XElement xml = XElement.Load(currentLogFile);
        var element = from e in xml.Elements("logDetails") select e;
        if (element.Count() > 0)
        {
            DateTime begin = Convert.ToDateTime(element.First().Element("beginTime").Value);
            DateTime end = DateTime.Now;
            string diff = getTimeDifference(begin, end);
            element.First().Element("duration").SetValue(diff);
            xml.Save(currentLogFile);

            return true;
        }
        else
        {
            return false;
        }
    }
    /*
     * 求时间差
     * @Param begin:开始时间
     * @Param end:操作结束时间
     * **/
    private static string getTimeDifference(DateTime begin, DateTime end)
    {
        TimeSpan duration = end.Subtract(begin); 
        string hours = duration.Hours.ToString();
        string min = duration.Minutes.ToString();
        string diff = "";
        if (hours == "0")
        {
            diff = duration.Minutes.ToString() + "分钟";
        }
        else if (min == "0")
        {
            diff = duration.Hours.ToString() + "小时";
        }
        else
        {
            diff = duration.Hours.ToString() + "小时" + duration.Minutes.ToString() + "分钟";
        }
        return diff;
    }

    /*
        * 查看单个当前Log日志全部信息
        * @Param 如果当前log路径为NULL的话 ，可以采用手动传参路径,默认为""
     * **/
    public static LogListInfo getCurrentSingleLogDetailInfo(string currentFullPath = "") {

        XElement xlm;
        if (currentLogFile == null || currentLogFile == "")
        {
            xlm = XElement.Load(currentFullPath);
        }
        else
        {
            xlm = XElement.Load(currentLogFile);
            Debug.Log(currentLogFile);
        }
        var elements = from e in xlm.Elements("logDetails") select e;
        LogListInfo logListinfo = new LogListInfo();
        List<LogDetailInfo> ldlist = new List<LogDetailInfo>();
        foreach (var element in elements)
        {
            logListinfo.Id = element.Attribute("id").Value;
            logListinfo.Name = element.Element("name").Value;
            logListinfo.CaseName = element.Element("caseName").Value;
            logListinfo.StudentId = element.Element("studentId").Value;
            logListinfo.Path = element.Element("path").Value;
            logListinfo.Duration = int.Parse(element.Element("duration").Value);
            logListinfo.DateTime = Convert.ToDateTime(element.Element("beginTime").Value);
            logListinfo.Username = element.Element("username").Value;

            var logs = from e in xlm.Elements("Logs") select e;
            var logelements = from e in logs.First().Elements("Log") select e;
            foreach (var logelement in logelements)
            {
                LogDetailInfo logDetailInfo = new LogDetailInfo();
                logDetailInfo.Type = int.Parse(logelement.Element("type").Value);
                logDetailInfo.Time = logelement.Element("time").Value;
                logDetailInfo.Content = logelement.Element("content").Value;
                ldlist.Add(logDetailInfo);
            }
            logListinfo.LogOptlist = ldlist;
          
        }
        return logListinfo;
    }
    /*
        * 查看当前Log日志信息
        * @Param 如果当前log路径为NULL的话 ，可以采用手动传参路径,默认为""
        * **/
    public static List<LogDetailInfo> getCurrentDetailLogInfo(string currentFullPath = "")
    {
        List<LogDetailInfo> currentList = new List<LogDetailInfo>();
        XElement xml;
        if (currentLogFile == null || currentLogFile == "")
        {
            xml = XElement.Load(currentFullPath);
        }
        else
        {
            xml = XElement.Load(currentLogFile);
            Debug.Log(currentLogFile);
        }
        var Logs = from e in xml.Elements("Logs") select e;
        var Log = from e in Logs.First().Elements("Log") select e;
        foreach (var element in Log)
        {
            LogDetailInfo logInfo = new LogDetailInfo();
            logInfo.Type = int.Parse(element.Element("type").Value);
            logInfo.Time = element.Element("time").Value;
            logInfo.Content = element.Element("content").Value;
            currentList.Add(logInfo);
        }
        return currentList;
    }

    /* 批量导出文件 */
    public static bool bulkExport(string[] ids)
    {
        if (ids.Length == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

}