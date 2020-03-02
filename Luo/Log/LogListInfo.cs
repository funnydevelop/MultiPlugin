using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/********************************************************************************

** 类名称： LogListInfo

** 描述：日志列表详情

** 作者： hongzhenluo

** 创建时间：2019-09-20

*********************************************************************************/

public class LogListInfo
{
    private string id;
    private string username; // Require
    private string caseName; // Require
    private DateTime dateTime;
    private int duration; 
    private string studentId; // Require
    private string name; //Require
    private string path; 
    private DateTime endTime;
    private List<LogDetailInfo> logOptlist;
    public string Id { get { return id; } set { id = value; } }
    public string Username { get { return username; } set { username = value; } }
    public string CaseName { get { return caseName; } set { caseName = value; } }
    public DateTime DateTime { get { return dateTime; } set { dateTime = value; } }
    public int Duration { get { return duration; } set { duration = value; } }
    public string StudentId { get { return studentId; } set { studentId = value; } }
    public string Name { get { return name; } set { name = value; } }
    public string Path { get { return path; } set { path = value; } }
    public DateTime EndTime { get { return endTime; } set { endTime = value; } }
    internal List<LogDetailInfo> LogOptlist { get { return logOptlist; } set { logOptlist = value; } }
}

