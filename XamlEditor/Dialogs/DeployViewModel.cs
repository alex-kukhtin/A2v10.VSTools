// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;

namespace XamlEditor;

internal class DeployViewModel : ObservableNode
{
	private String _server;
	private String _database;
	private Boolean _migrations;
	private Boolean _isProgress;
	private Int32 _progress;
	public String Server { get => _server; set { _server = value; OnPropertyChanged(); } }
	public String Database { get => _database; set { _database = value; OnPropertyChanged(); } }
	public Boolean Migrations { get => _migrations; set { _migrations = value; OnPropertyChanged(); } }
	public Boolean IsProgress { get => _isProgress; set { _isProgress = value; OnPropertyChanged(); } }
	public Int32 Progress { get => _progress; set { _progress = value; OnPropertyChanged(); } }

	public String ConnectionString =>
		$"Server={Server};Database={Database};Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False";
}
