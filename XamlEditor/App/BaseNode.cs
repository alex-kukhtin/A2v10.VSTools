// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Newtonsoft.Json;

namespace XamlEditor;
public class ObservableNode : INotifyPropertyChanged
{
	[JsonIgnore]
	internal AppNode _root;

	public event PropertyChangedEventHandler PropertyChanged;

	public void OnPropertyChanged([CallerMemberName] String prop = "")
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		if (_root != null)
			_root.IsDirty = true;	
	}
}

public class BaseNode : ObservableNode
{
	private String _name;

	[JsonProperty(Order = -5)]
	public String Name { get => _name; set { _name = value; OnPropertyChanged(); OnNameChanged(); } }

	[JsonIgnore]
	public String Image => $"/XamlEditor;Component/Images/{ImageName}.png";

	[JsonIgnore]
	protected virtual String ImageName => "FolderClosed";

	[JsonIgnore]
	public virtual IEnumerable<BaseNode> Children => null;

	[JsonIgnore]
	public static Boolean IsExpanded => true;

	private Boolean _isSelected;
	[JsonIgnore]
	public Boolean IsSelected{
		get
		{
			return _isSelected;
		}
		set
		{
			if (_isSelected == value)
				return;
			_isSelected = value;
			OnPropertyChanged();
		}
	}

	public virtual void OnNameChanged()
	{
	}

	internal virtual void OnInit()
	{
	}
}