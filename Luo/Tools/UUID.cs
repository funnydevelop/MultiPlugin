using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/********************************************************************************

** 类名称： UUID

** 描述：UUID 工具类

** 作者： hongzhenluo

** 创建时间：2019-09-20

*********************************************************************************/
public class UUID
    {
        public static string generate()
        {
            return System.Guid.NewGuid().ToString();
        }
    }

