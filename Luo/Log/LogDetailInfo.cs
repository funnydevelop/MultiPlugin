using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/********************************************************************************

** 类名称： LogDetailInfo

** 描述：日志内容详情

** 作者： hongzhenluo

** 创建时间：2019-09-20

*********************************************************************************/

 
public enum LogDetailType
{
    OprationLog,
    TreatLog,
    All
}
public class LogDetailInfo
{

    private int type;
    private string time;  
    private string content;

    private LogDetailType logDetailType;

    public int Type {
        get { return type; }
        set { type = value; LogDetailType temp = (type == 0) ? (LogDetailType = LogDetailType.OprationLog) : (LogDetailType = LogDetailType.TreatLog); }
    }
    public string Time { get { return time; } set { time = value; } }
    public string Content { get { return content; } set { content = value; } }
    public LogDetailType LogDetailType { get { return logDetailType; } set { logDetailType = value; } }
}

