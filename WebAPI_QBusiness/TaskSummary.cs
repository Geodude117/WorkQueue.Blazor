using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI_QBusiness
{
    public static class TaskSummary
    {
        public static bool TaskBoolSummary(bool[] TaskList)
        {
            foreach (var xTask in TaskList)
            {
                if (!xTask)
                    return xTask;
            }
            return true;
        }
    }
}
