using System;
using System.Runtime.InteropServices;

using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace A2v10.MetadataEditor
{
	public class MetadataEditorFactory : IVsEditorFactory, IDisposable
	{
		private ServiceProvider _vsServiceProvider;
		public int CreateEditorInstance(uint grfCreateDoc, string pszMkDocument, string pszPhysicalView, IVsHierarchy pvHier, uint itemid, IntPtr punkDocDataExisting, out IntPtr ppunkDocView, out IntPtr ppunkDocData, out string pbstrEditorCaption, out Guid pguidCmdUI, out int pgrfCDW)
		{
			// Initialize to null
			ppunkDocView = IntPtr.Zero;
			ppunkDocData = IntPtr.Zero;
			pguidCmdUI = Guids.guidEditorFactory;
			pgrfCDW = 0;
			pbstrEditorCaption = null;

			// Validate inputs
			if ((grfCreateDoc & (VSConstants.CEF_OPENFILE | VSConstants.CEF_SILENT)) == 0)
			{
				return VSConstants.E_INVALIDARG;
			}
			if (punkDocDataExisting != IntPtr.Zero)
			{
				return VSConstants.VS_E_INCOMPATIBLEDOCDATA;
			}

			// Create the Document (editor)
			MetadataEditorPane newEditor = new MetadataEditorPane();
			ppunkDocView = Marshal.GetIUnknownForObject(newEditor);
			ppunkDocData = Marshal.GetIUnknownForObject(newEditor);
			pbstrEditorCaption = "";

			return VSConstants.S_OK;
		}

		public int SetSite(Microsoft.VisualStudio.OLE.Interop.IServiceProvider psp)
		{
			_vsServiceProvider = new ServiceProvider(psp);
			return VSConstants.S_OK;
		}

		public int Close()
		{
			return VSConstants.S_OK;
		}

		public int MapLogicalView(ref Guid rguidLogicalView, out string pbstrPhysicalView)
		{
			pbstrPhysicalView = null;   // initialize out parameter

			// we support only a single physical view
			if (VSConstants.LOGVIEWID_Primary == rguidLogicalView)
			{
				// primary view uses NULL as pbstrPhysicalView
				return VSConstants.S_OK;
			}
			else
			{
				// you must return E_NOTIMPL for any unrecognized rguidLogicalView values
				return VSConstants.E_NOTIMPL;
			}
		}

		public void Dispose()
		{
			ThreadHelper.ThrowIfNotOnUIThread();
			Dispose(true);
		}
		private void Dispose(bool disposing)
		{
			ThreadHelper.ThrowIfNotOnUIThread();
			// If disposing equals true, dispose all managed and unmanaged resources
			if (disposing)
			{
				/// Since we create a ServiceProvider which implements IDisposable we
				/// also need to implement IDisposable to make sure that the ServiceProvider's
				/// Dispose method gets called.
				if (_vsServiceProvider != null)
				{
					_vsServiceProvider.Dispose();
					_vsServiceProvider = null;
				}
			}
		}
	}
}