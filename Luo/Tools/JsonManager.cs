using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

public class UserInfo

{

    public string name;
    public int age;
    public address addr;

}

public class address
{
    public string city;
    public string province;
}

class JsonObj
{
    public string id { get; set; }
    public string username { get; set; }
    public string password { get; set; }
}
//Test
class Program
{
    class TempObj
    {
        private Boolean success;
        private DataObj data;

        public bool Success {
            get { return success; }
            set { success = value; }
        }
        public DataObj Data {
            get { return data; }
            set { data = value; }
        }
    }
    class DataObj
    {
        public Dictionary<string, string> PositionType { get; set; }
        public Dictionary<string, string> InjuredType { get; set; }
        public SecondDataObj BasicType { get; set; }
    }
    class SecondDataObj
    {
        public string _case;
        public string _patient;

        //public string Case { get => _case; set => _case = value; }
        //public string Patient { get => _patient; set => _patient = value; }
    }


    public static string GetStudent()
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        List<Student> lst = new List<Student>() {
                new Student() {Id=1,Name="张飞",Age=450,Address="涿郡人" },//处理日期需要在实体类中标记
                new Student() {Id=2,Name="赵云",Age=480,Address="常山真定"},
                new Student() {Id=3,Name="刘备",Age=500,Address="三国人"}
            };
        Dictionary<string, object> dic1 = new Dictionary<string, object>();
        dic1.Add("list", lst);
        dic.Add("status", 200);
        dic.Add("data", dic1);
        dic.Add("page", 8);
        dic.Add("total", 9);
        return JsonConvert.SerializeObject(dic, Formatting.Indented);
    }
    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int? Number { get; set; }
        public string Address { get; set; }

        public Dictionary<string, string> testDic { get; set; }

    }
    public static string generateStudentInfo()
    {
        Student stu = new Student();
        stu.Id = 123;
        stu.Name = "Tommy";
        stu.Age = 23;
        stu.Number = 10;
        stu.Address = "TianJin";
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("hello", "1"); dic.Add("world", "123"); dic.Add("today", "tomorrow");
        stu.testDic = dic;
        return JsonConvert.SerializeObject(stu);
    }

    public static string serializeObjToJson(System.Object obj)
    {
        string result = JsonConvert.SerializeObject(obj);
        return result;
    }
    public static T deserializeJsonToObj<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }
    public static JObject deserializeJsonToObj(string json)
    {
        JObject jObject = JObject.Parse(json);
        return jObject;
    }
    public static System.Object deserializeJsonToObj(string json, Type t)
    {
        return JsonConvert.DeserializeObject(json, t);
    }
    public static Boolean isJsonFormat(string json)
    {
        return false;
    }
}
