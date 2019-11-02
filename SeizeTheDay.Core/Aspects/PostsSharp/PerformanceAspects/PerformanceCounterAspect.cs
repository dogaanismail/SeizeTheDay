using PostSharp.Aspects;
using System;
using System.Diagnostics;
using System.Reflection;

namespace SeizeTheDay.Core.Aspects.Postsharp.PerformanceAspects
{
    [Serializable]
    public class PerformanceCounterAspect : OnMethodBoundaryAspect
    {
        private int _interval;

        [NonSerialized]
        private Stopwatch _stopWatch;

        public PerformanceCounterAspect(int interval=5)
        {
            _interval = interval;
        }

        public override void RuntimeInitialize(MethodBase method)
        {
            _stopWatch = Activator.CreateInstance<Stopwatch>();
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            _stopWatch.Start();
            base.OnEntry(args);
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            _stopWatch.Stop();
            if (_stopWatch.Elapsed.TotalSeconds > _interval)
            {
                //You can send a mail to yourself.
                Debug.WriteLine("Performance: {0}.{1}->>{2}", args.Method.DeclaringType.FullName, args.Method.Name, _stopWatch.Elapsed.TotalSeconds);
            }
            _stopWatch.Reset();
            base.OnExit(args);
        }
    }
}
