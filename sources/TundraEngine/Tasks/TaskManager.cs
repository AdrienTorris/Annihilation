using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TundraEngine
{
    public class TaskManager
    {
        private Queue<Action> _taskQueue = new Queue<Action>();
        private List<Action> _waitingTasks = new List<Action>();

        public void ExecuteTasks()
        {

        }
    }
}