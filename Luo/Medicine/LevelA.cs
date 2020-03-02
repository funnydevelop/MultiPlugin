using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/********************************************************************************

** 类名称： LevelA

** 描述：药物实体类

** 作者： hongzhenluo

** 创建时间：2019-09-20

*********************************************************************************/


class LevelA
{
    private string name;
    private string level;
    private List<LevelB> levelBList;

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
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

    internal List<LevelB> LevelBList
    {
        get
        {
            return levelBList;
        }

        set
        {
            levelBList = value;
        }
    }
}

