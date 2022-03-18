// copyright dbosoft GmbH / licensed under MIT license
// based on sample code by Microsoft Windows SDK samples 
// https://github.com/microsoft/Windows-classic-samples/blob/master/Samples/Win7Samples/winbase/imapi/interop/IMAPIv2.cs


using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using Haipa.CloudInit.ConfigDrive.Interop;

namespace Dbosoft.CloudInit.ConfigDrive.Interop
{
    [ClassInterface(ClassInterfaceType.None)]
    // ReSharper disable once InconsistentNaming
    internal sealed class DiscFormat2Data_EventsProvider : DiscFormat2Data_Events, IDisposable
    {
        public DiscFormat2Data_EventsProvider(object pointContainer)
        {
            lock (this)
            {
                if (_mConnectionPoint != null) return;
                _mAEventSinkHelpers = new Hashtable();
                var eventsGuid = typeof(DDiscFormat2DataEvents).GUID;
                var connectionPointContainer = pointContainer as IConnectionPointContainer;

                connectionPointContainer?.FindConnectionPoint(ref eventsGuid, out _mConnectionPoint);
            }
        }

        public event DiscFormat2Data_EventsHandler Update
        {
            add
            {
                lock (this)
                {
                    var helper = new DiscFormat2Data_SinkHelper(value);

                    _mConnectionPoint.Advise(helper, out var cookie);
                    helper.Cookie = cookie;
                    _mAEventSinkHelpers.Add(helper.UpdateDelegate, helper);
                }
            }

            remove
            {
                lock (this)
                {
                    if (!(_mAEventSinkHelpers[value] is DiscFormat2Data_SinkHelper helper)) return;
                    _mConnectionPoint.Unadvise(helper.Cookie);
                    _mAEventSinkHelpers.Remove(helper.UpdateDelegate);
                }
            }
        }

        public void Dispose()
        {
            Cleanup();
            GC.SuppressFinalize(this);
        }

        ~DiscFormat2Data_EventsProvider()
        {
            Cleanup();
        }

        private void Cleanup()
        {
            Monitor.Enter(this);
            try
            {
                foreach (DiscFormat2Data_SinkHelper helper in _mAEventSinkHelpers)
                {
                    _mConnectionPoint.Unadvise(helper.Cookie);
                }

                _mAEventSinkHelpers.Clear();
                Marshal.ReleaseComObject(_mConnectionPoint);
            }
            catch (SynchronizationLockException)
            {
            }
            finally
            {
                Monitor.Exit(this);
            }
        }

        private readonly Hashtable _mAEventSinkHelpers;
        private static IConnectionPoint _mConnectionPoint = null;
    }
}