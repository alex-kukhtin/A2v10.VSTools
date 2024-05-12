using System;

using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

using XamlEditor;

namespace A2v10.MetadataEditor
{
	public class MetadataEditorPane : WindowPane, IOleComponent, IVsPersistDocData
	{
		private MetadataEditorControl _control;

		public MetadataEditorPane()
			: base(null)
		{
			PrivateInit();
		}
		void PrivateInit()
		{
		}
		protected override void Initialize()
		{
			_control = new MetadataEditorControl();
			_control.HideSave();
			Content = _control;
		}

		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (_control != null)
					{
						// _control;
						_control = null;
					}
					GC.SuppressFinalize(this);
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		public int FReserved1(uint dwReserved, uint message, IntPtr wParam, IntPtr lParam)
		{
			return VSConstants.S_OK;
		}

		public int FPreTranslateMessage(MSG[] pMsg)
		{
			return VSConstants.S_OK;
		}

		public void OnEnterState(uint uStateID, int fEnter)
		{
		}

		public void OnAppActivate(int fActive, uint dwOtherThreadID)
		{
		}

		public void OnLoseActivation()
		{
		}

		public void OnActivationChange(IOleComponent pic, int fSameComponent, OLECRINFO[] pcrinfo, int fHostIsActivating, OLECHOSTINFO[] pchostinfo, uint dwReserved)
		{
		}

		public int FDoIdle(uint grfidlef)
		{
			if (_control != null)
			{
				//_control.DoIdle();
			}
			return VSConstants.S_OK;
		}

		public int FContinueMessageLoop(uint uReason, IntPtr pvLoopData, MSG[] pMsgPeeked)
		{
			return VSConstants.S_OK;
		}

		public int FQueryTerminate(int fPromptUser)
		{
			return 1; //true
		}

		public void Terminate()
		{
		}

		public IntPtr HwndGetWindow(uint dwWhich, uint dwReserved)
		{
			return IntPtr.Zero;
		}

		int IVsPersistDocData.GetGuidEditorType(out Guid pClassID)
		{
			throw new NotImplementedException();
		}

		int IVsPersistDocData.IsDocDataDirty(out int pfDirty)
		{
			pfDirty = (_control != null && _control.IsDirty) ? 1 : 0;
			return VSConstants.S_OK;
		}

		int IVsPersistDocData.SetUntitledDocPath(string pszDocDataPath)
		{
			return VSConstants.S_OK;
		}

		int IVsPersistDocData.LoadDocData(string pszMkDocument)
		{
			// var text = System.IO.File.ReadAllText(pszMkDocument);
			_control.LoadDocument(pszMkDocument);
			return VSConstants.S_OK;
		}

		int IVsPersistDocData.SaveDocData(VSSAVEFLAGS dwSave, out string pbstrMkDocumentNew, out int pfSaveCanceled)
		{
			pbstrMkDocumentNew = null;
			pfSaveCanceled = 0;
			int hr = VSConstants.S_OK; 

			switch (dwSave)
			{
				case VSSAVEFLAGS.VSSAVE_Save:
				case VSSAVEFLAGS.VSSAVE_SilentSave:
					_control.SaveDocument();
					break;
				case VSSAVEFLAGS.VSSAVE_SaveAs:
				case VSSAVEFLAGS.VSSAVE_SaveCopyAs:
					break;
				default:
					throw new ArgumentException($"Invalid save flags: {dwSave}");
			}
			return hr;
		}

		int IVsPersistDocData.Close()
		{
			return VSConstants.S_OK;
		}

		int IVsPersistDocData.OnRegisterDocData(uint docCookie, IVsHierarchy pHierNew, uint itemidNew)
		{
			return VSConstants.S_OK;
		}

		int IVsPersistDocData.RenameDocData(uint grfAttribs, IVsHierarchy pHierNew, uint itemidNew, string pszMkDocumentNew)
		{
			return VSConstants.S_OK;
		}

		int IVsPersistDocData.IsDocDataReloadable(out int pfReloadable)
		{
			pfReloadable = 1;
			return VSConstants.S_OK;
		}

		int IVsPersistDocData.ReloadDocData(uint grfFlags)
		{
			return VSConstants.S_OK;
		}
	}
}
