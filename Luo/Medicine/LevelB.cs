using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/********************************************************************************

** 类名称： LevelB

** 描述：药物实体类

** 作者： hongzhenluo

** 创建时间：2019-09-20

*********************************************************************************/

class LevelB
{
    private string name;
    private string level;
    private List<Medicine> medicineList;

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

    internal List<Medicine> MedicineList
    {
        get
        {
            return medicineList;
        }

        set
        {
            medicineList = value;
        }
    }
}

