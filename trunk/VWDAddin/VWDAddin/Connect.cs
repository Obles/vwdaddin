using Application = Microsoft.Office.Interop.Visio.Application;
using System.Diagnostics;
using System;
using Extensibility;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace VWDAddin
{
	
	#region Read me for Add-in installation and setup information.
	// When run, the Add-in wizard prepared the registry for the Add-in.
	// At a later time, if the Add-in becomes unavailable for reasons such as:
	//   1) You moved this project to a computer other than which is was originally created on.
	//   2) You chose 'Yes' when presented with a message asking if you wish to remove the Add-in.
	//   3) Registry corruption.
	// you will need to re-register the Add-in by building the VWDAddinSetup project, 
	// right click the project in the Solution Explorer, then choose install.
	#endregion
	
	/// <summary>
	///   The object for implementing an Add-in.
	/// </summary>
	/// <seealso class='IDTExtensibility2' />
	[GuidAttribute("87A4A9A4-BDC0-4362-B7D8-F62B242195D0"), ProgId("VWDAddin.Connect")]
	public class Connect : Object, Extensibility.IDTExtensibility2
	{
		/// <summary>
		///		Implements the constructor for the Add-in object.
		///		Place your initialization code within this method.
		/// </summary>
		public Connect()
		{
		}

		/// <summary>
		///      Implements the OnConnection method of the IDTExtensibility2 interface.
		///      Receives notification that the Add-in is being loaded.
		/// </summary>
		/// <param term='application'>
		///      Root object of the host application.
		/// </param>
		/// <param term='connectMode'>
		///      Describes how the Add-in is being loaded.
		/// </param>
		/// <param term='addInInst'>
		///      Object representing this Add-in.
		/// </param>
		/// <seealso class='IDTExtensibility2' />
        public void OnConnection(object application, Extensibility.ext_ConnectMode connectMode, object addInInst, ref System.Array custom)
        {
            if (Constants.LogFile != String.Empty)
            {
                DebugListener = new TextWriterTraceListener(
                    new System.IO.FileStream(Constants.LogFile, System.IO.FileMode.OpenOrCreate)
                );
                Debug.Listeners.Add(DebugListener);
                Debug.AutoFlush = true;
                Trace.AutoFlush = true;
            }

            Trace.WriteLine("-----------[ Add-In Connecting ]-----------");
            try
            {
                visApplication = (Application)application;
            }
            catch (Exception)
            {
                MessageBox.Show("TypeCast Error");
                return;
            }
           
            addInInstance = addInInst;

            Trace.WriteLine("Name: " + visApplication.Name + " Version: " + visApplication.Version);

            EventManager = new EventManager();
            EventManager.StartApplicationListener(visApplication);

            //visApplication.Documents.Add("e:\\Visual Studio 2005\\Projects\\vwdaddin\\Template\\Domain-Specific Modeling\\TestTemplate.vtx");
            //visApplication.Documents.Add("c:\\Documents and Settings\\user\\Мои документы\\Drawing2.vsd");
        }

		/// <summary>
		///     Implements the OnDisconnection method of the IDTExtensibility2 interface.
		///     Receives notification that the Add-in is being unloaded.
		/// </summary>
		/// <param term='disconnectMode'>
		///      Describes how the Add-in is being unloaded.
		/// </param>
		/// <param term='custom'>
		///      Array of parameters that are host application specific.
		/// </param>
		/// <seealso class='IDTExtensibility2' />
		public void OnDisconnection(Extensibility.ext_DisconnectMode disconnectMode, ref System.Array custom)
		{
            if (DebugListener != null)
            {
                Debug.Flush();
                Trace.Flush();
                DebugListener.Flush();
            }
		}

		/// <summary>
		///      Implements the OnAddInsUpdate method of the IDTExtensibility2 interface.
		///      Receives notification that the collection of Add-ins has changed.
		/// </summary>
		/// <param term='custom'>
		///      Array of parameters that are host application specific.
		/// </param>
		/// <seealso class='IDTExtensibility2' />
		public void OnAddInsUpdate(ref System.Array custom)
		{
		}

		/// <summary>
		///      Implements the OnStartupComplete method of the IDTExtensibility2 interface.
		///      Receives notification that the host application has completed loading.
		/// </summary>
		/// <param term='custom'>
		///      Array of parameters that are host application specific.
		/// </param>
		/// <seealso class='IDTExtensibility2' />
		public void OnStartupComplete(ref System.Array custom)
		{
		}

		/// <summary>
		///      Implements the OnBeginShutdown method of the IDTExtensibility2 interface.
		///      Receives notification that the host application is being unloaded.
		/// </summary>
		/// <param term='custom'>
		///      Array of parameters that are host application specific.
		/// </param>
		/// <seealso class='IDTExtensibility2' />
		public void OnBeginShutdown(ref System.Array custom)
		{
		}

        private EventManager EventManager;
        private Application visApplication;
        private object addInInstance;
        private TextWriterTraceListener DebugListener = null;
	}
}