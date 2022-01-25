// copyright dbosoft GmbH / licensed under MIT license
// based on sample code by Microsoft Windows SDK samples 
// https://github.com/microsoft/Windows-classic-samples/blob/master/Samples/Win7Samples/winbase/imapi/interop/IMAPIv2.cs


// ReSharper disable All

using System;
using System.Runtime.InteropServices;
using Dbosoft.CloudInit.ConfigDrive.Interop;

#pragma warning disable 1573
#pragma warning disable 1584,1711,1572,1581,1580
#pragma warning disable 1591

namespace Haipa.CloudInit.ConfigDrive.Interop
{
    // Interfaces


    // CoClass - Specifies the class identifier of a coclass 
    // imported from a type library.

    //Events

    [TypeLibType(TypeLibTypeFlags.FHidden), ClassInterface(ClassInterfaceType.None)]
    public sealed class DiscFormat2Data_SinkHelper : DDiscFormat2DataEvents
    {
        public DiscFormat2Data_SinkHelper(DiscFormat2Data_EventsHandler eventHandler)
        {
            if (eventHandler == null)
                throw new ArgumentNullException("Delegate (callback function) cannot be null");
            m_dwCookie = 0;
            m_UpdateDelegate = eventHandler;
        }

        public void Update([In, MarshalAs(UnmanagedType.IDispatch)] object sender, [In, MarshalAs(UnmanagedType.IDispatch)] object args)
        {
            m_UpdateDelegate(sender, args);
        }

        public int Cookie
        {
            get
            {
                return m_dwCookie;
            }
            set
            {
                m_dwCookie = value;
            }
        }

        public DiscFormat2Data_EventsHandler UpdateDelegate
        {
            get
            {
                return m_UpdateDelegate;
            }
            set
            {
                m_UpdateDelegate = value;
            }
        }

        private int m_dwCookie;
        private DiscFormat2Data_EventsHandler m_UpdateDelegate;
    }

    // Class

    // Enumerator
}