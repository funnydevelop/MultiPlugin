using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class InterveneInfo
{
    private string category;
    private string content;
    private bool hasEqupment;
    private string level;
    private string equpList;
    private string id;
    public string Id
    {
        get
        {
            return id;
        }
        set
        {
            id = value;
        }
    }
    public string Category
    {
        get
        {
            return category;
        }

        set
        {
            category = value;
        }
    }

    public string Content
    {
        get
        {
            return content;
        }

        set
        {
            content = value;
        }
    }

    public bool HasEqupment
    {
        get
        {
            return hasEqupment;
        }

        set
        {
            hasEqupment = value;
        }
    }

    public string EqupList
    {
        get
        {
            return equpList;
        }

        set
        {
            equpList = value;
        }
    }

    public string Level
    {
        get
        {
            return level;
        }

        set
        {
            level = value;
        }
    }
}

