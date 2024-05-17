// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace XamlEditor;

internal class SqlDeploy
{
	private readonly ViewModel _vm;
	private readonly String _server;
	private readonly String _database;
	private readonly Action<Int32> _setProgress;
	private SqlDeploy(ViewModel vm, String server, String database, Action<Int32> setProgress)
	{
		_vm = vm;	
		_server = server;
		_database = database;
		_setProgress = setProgress;
	}
	public static async Task DeployDatabase(ViewModel vm, String server, String database, Action<Int32> setProgress)
	{
		var dbDeploy = new SqlDeploy(vm, server, database, setProgress);
		try
		{
			await dbDeploy.Deploy();
		} 
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	public String ConnectionStringMaster =>
		$"Server={_server};Database=master;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False";
	public String ConnectionString =>
		$"Server={_server};Database={_database};Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False";

	private async Task Deploy()
	{
		using (var cnnMaster = new SqlConnection(ConnectionStringMaster))
		{
			cnnMaster.Open();
			await CreateDatabase(cnnMaster);
		}
		var sqlBuilder = new SqlTextBuilder(_vm.AppNode);
		_setProgress(10);
		using var cnn = new SqlConnection(ConnectionString);
		cnn.Open();
		await RunScript(cnn, _vm.PlatformSqlScript());
		_setProgress(30);
		await RunScript(cnn, sqlBuilder.BuildSchemas());
		_setProgress(40);
		await RunScript(cnn, sqlBuilder.BuildTables());
		_setProgress(60);
		await RunScript(cnn, sqlBuilder.BuildTableTypes());
		_setProgress(80);
		await RunScript(cnn, sqlBuilder.BuildUI());
		_setProgress(100);
		await Task.Delay(100);
		cnn.Close();
	}

	Task CreateDatabase(SqlConnection cnn)
	{
		var cmd = cnn.CreateCommand();
		cmd.CommandText = $"""
		if not exists (select * from sys.databases where [name] = N'{_database}')
			create database {_database};
		""";
		return cmd.ExecuteNonQueryAsync();	
	}

	async Task RunScript(SqlConnection cnn, String commands)
	{
		var cmd = cnn.CreateCommand();
		var lines = commands.Split('\n').Select(s => s.Trim());
		var sb = new StringBuilder();
        foreach (var line in lines)
        {
            if (line.IndexOf("go", StringComparison.Ordinal) == 0) 
			{ 
				cmd.CommandText = sb.ToString();
				sb.Clear();
				await cmd.ExecuteNonQueryAsync();
			}
			else
				sb.AppendLine(line);
        }
	}
}

