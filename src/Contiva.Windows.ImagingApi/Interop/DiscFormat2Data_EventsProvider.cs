using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;

namespace Contiva.Windows.ImagingApi.Interop
{
    [ClassInterface(ClassInterfaceType.None)]
    internal sealed class DiscFormat2Data_EventsProvider : DiscFormat2Data_Events, IDisposable
    {
        public DiscFormat2Data_EventsProvider(object pointContainer)
        {
            lock (this)
            {
                if (m_ConnectionPoint == null)
                {
                    m_aEventSinkHelpers = new Hashtable();
                    Guid eventsGuid = typeof(DDiscFormat2DataEvents).GUID;
                    IConnectionPointContainer connectionPointContainer = pointContainer as IConnectionPointContainer;

                    connectionPointContainer.FindConnectionPoint(ref eventsGuid, out m_ConnectionPoint);
                }
            }
        }

        public event DiscFormat2Data_EventsHandler Update
        {
            add
            {
                lock (this)
                {
                    DiscFormat2Data_SinkHelper helper = new DiscFormat2Data_SinkHelper(value);
                    int cookie = -1;

                    m_ConnectionPoint.Advise(helper, out cookie);
                    helper.Cookie = cookie;
                    m_aEventSinkHelpers.Add(helper.UpdateDelegate, helper);
                }
            }

            remove
            {
                lock (this)
                {
                    DiscFormat2Data_SinkHelper helper = m_aEventSinkHelpers[value] as DiscFormat2Data_SinkHelper;
                    if (helper != null)
                    {
                        m_ConnectionPoint.Unadvise(helper.Cookie);
                        m_aEventSinkHelpers.Remove(helper.UpdateDelegate);
                    }
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
                foreach (DiscFormat2Data_SinkHelper helper in m_aEventSinkHelpers)
                {
                    m_ConnectionPoint.Unadvise(helper.Cookie);
                }

                m_aEventSinkHelpers.Clear();
                Marshal.ReleaseComObject(m_ConnectionPoint);
            }
            catch (SynchronizationLockException)
            {
                return;
            }
            finally
            {
                Monitor.Exit(this);
            }
        }

        private Hashtable m_aEventSinkHelpers;
        static private IConnectionPoint m_ConnectionPoint = null;
    }
}