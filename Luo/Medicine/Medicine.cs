using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/********************************************************************************

** 类名称： Medicine

** 描述：药物实体类

** 作者： hongzhenluo

** 创建时间：2019-09-20

*********************************************************************************/
class Medicine
{
    private string id;
    private string name;
    private string level;
    public string Id {
        get
        {
            return id;
        }
        set
        {
            id = value;
        }
    }
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
}

