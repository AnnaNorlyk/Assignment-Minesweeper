using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAssignment
{
    public interface ITimeManager
    {
        void StartTimer();
        void StopTimer();
        void ResetTimer();
        TimeSpan GetElapsedTime();
    }
}
