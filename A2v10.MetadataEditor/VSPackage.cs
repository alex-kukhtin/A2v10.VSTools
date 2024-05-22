﻿// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using EnvDTE;

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Task = System.Threading.Tasks.Task;

using Microsoft.VisualStudio.Shell;


namespace A2v10.MetadataEditor;

/// <summary>
/// This is the class that implements the package exposed by this assembly.
/// </summary>
/// <remarks>
/// <para>
/// The minimum requirement for a class to be considered a valid package for Visual Studio
/// is to implement the IVsPackage interface and register itself with the shell.
/// This package uses the helper classes defined inside the Managed Package Framework (MPF)
/// to do it: it derives from the Package class that provides the implementation of the
/// IVsPackage interface and uses the registration attributes defined in the framework to
/// register itself and its components with the shell. These attributes tell the pkgdef creation
/// utility what data to put into .pkgdef file.
/// </para>
/// <para>
/// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
/// </para>
/// </remarks>
[PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
[Guid(VSPackage.PackageGuidString)]
[ProvideEditorExtension(typeof(MetadataEditorFactory), ".metadata", 32,
	ProjectGuid = "{FD222A78-06A4-4C67-BAFA-BE67E15E0D88}",
	TemplateDir = @".\NullPath",
	NameResourceID = 114
)]
[ProvideEditorLogicalView(typeof(MetadataEditorFactory), "{A090AEB5-0972-4C07-A534-A0825CEB4AD3}")]
public sealed class VSPackage : AsyncPackage
{
	/// <summary>
	/// VSPackage GUID string.
	/// </summary>
	public const string PackageGuidString = "8e861c64-84dc-4ec5-be25-fdde31536d87";

	MetadataEditorFactory _editorFactory;
	/// <summary>
	/// Initializes a new instance of the <see cref="VSPackage"/> class.
	/// </summary>
	public VSPackage()
	{
		// Inside this method you can place any initialization code that does not require
		// any Visual Studio service because at this point the package object is created but
		// not sited yet inside Visual Studio environment. The place to do all the other
		// initialization is the Initialize method.
	}

	#region Package Members

	/// <summary>
	/// Initialization of the package; this method is called right after the package is sited, so this is the place
	/// where you can put all the initialization code that rely on services provided by VisualStudio.
	/// </summary>
	/// <param name="cancellationToken">A cancellation token to monitor for initialization cancellation, which can occur when VS is shutting down.</param>
	/// <param name="progress">A provider for progress updates.</param>
	/// <returns>A task representing the async work of package initialization, or an already completed task if there is none. Do not return null from this method.</returns>
	protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
	{
		// When initialized asynchronously, the current thread may be a background thread at this point.
		// Do any initialization that requires the UI thread after switching to the UI thread.
		await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

		var solutionName = "Test1";

		var dteObj = await GetServiceAsync(typeof(DTE));
		if (dteObj != null)
		{
			var dte = (DTE)dteObj;
			var fullSolutionName = dte.Solution.FullName;
			solutionName = Path.GetFileNameWithoutExtension(fullSolutionName);
		}
		_editorFactory = new MetadataEditorFactory(solutionName);
		RegisterEditorFactory(_editorFactory);
	}
	#endregion
}
