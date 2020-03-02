using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/********************************************************************************

** 类名称：MedicineListInfo

** 描述：药物实体类

** 作者： hongzhenluo

** 创建时间：2019-10-7

*********************************************************************************/
public class MedicineListInfo
{
    private string levelAName;
    private string levelBName;
    private string type;
    private string medicine;
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
    public string LevelBName
        {
            get
            {
                return levelBName;
            }

            set
            {
                levelBName = value;
            }
        }

        public string LevelAName
        {
            get
            {
                return levelAName;
            }

            set
            {
                levelAName = value;
            }
        }

        public string Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public string Medicine
        {
            get
            {
                return medicine;
            }

            set
            {
                medicine = value;
            }
        }
    }

