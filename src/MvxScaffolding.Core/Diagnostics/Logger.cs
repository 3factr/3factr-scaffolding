//---------------------------------------------------------------------------------
// Copyright © 2018, Jonathan Froon, Plac3hold3r+github@outlook.com
// MvxScaffolding is licensed using the MIT License
//---------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using MvxScaffolding.Core.Diagnostics.Trackers;
using MvxScaffolding.Core.Diagnostics.Writers;

namespace MvxScaffolding.Core.Diagnostics
{
    public class Logger : IDisposable
    {
        public TraceTracker Verbose { get; private set; }

        public TraceTracker Info { get; private set; }

        public TraceTracker Warning { get; private set; }

        public TraceTracker Error { get; private set; }

        public ExceptionTracker Exception { get; private set; }

        public TelemetryTracker Telemetry { get; private set; }

        public static List<IWriter> Writers { get; } = new List<IWriter>();

        private static Logger _current;

        public static Logger Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new Logger();
                }

                return _current;
            }
            private set => _current = value;
        }

        private Logger()
        {
            InstanceDefaultWriters();

            Verbose = new TraceTracker(TraceEventType.Verbose);
            Info = new TraceTracker(TraceEventType.Information);
            Warning = new TraceTracker(TraceEventType.Warning);
            Error = new TraceTracker(TraceEventType.Error);
            Exception = new ExceptionTracker();
            Telemetry = new TelemetryTracker();
        }

        private void InstanceDefaultWriters()
        {
            Writers.Add(new ConsoleWriter());
            Writers.Add(RemoteWriter.Current);
        }

        ~Logger()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (IWriter writer in Writers)
                {
                    if (writer is IDisposable disposableWriter)
                    {
                        disposableWriter.Dispose();
                    }
                }
            }
        }
    }
}
