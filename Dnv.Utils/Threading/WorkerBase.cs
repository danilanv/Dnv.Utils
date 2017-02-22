using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dnv.Utils.Threading
{
    /// <summary>
    /// Base class for woker threads;
    /// </summary>
    public abstract class WorkerBase
    {
        /// <summary>
        /// Finished thread event
        /// </summary>
        protected readonly AutoResetEvent FinishedEvent;

        /// <summary>
        /// Признак того, что нужно завершить поток
        /// </summary>
        protected volatile bool CancelationPending = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="finishedEvent"></param>
        protected WorkerBase(AutoResetEvent finishedEvent)
        {
            FinishedEvent = finishedEvent;
        }

        public void RequestStop()
        {
            CancelationPending = true;
        }

        public abstract void DoWork();
    }
}
