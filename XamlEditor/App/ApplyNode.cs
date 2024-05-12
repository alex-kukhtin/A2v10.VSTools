// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using Newtonsoft.Json;

namespace XamlEditor;
public class ApplyNode : BaseNode
{
	[JsonIgnore]
	internal EndpointNode _endpoint;

	private String _journal;

	[JsonProperty(Order = -5)]
	public String Journal
	{
		get => _journal;
		set { _journal = value; OnPropertyChanged();}
	}
	
	private Boolean _in;
	[JsonProperty(Order = -4)]
	public Boolean In { get => _in; set { _in = value; OnPropertyChanged(); } }

	private Boolean _out;
	[JsonProperty(Order = -3)]
	public Boolean Out { get => _out; set { _out = value; OnPropertyChanged(); } }

	private Boolean _storno;
	[JsonProperty(Order = -2)]
	public Boolean Storno { get => _storno; set { _storno = value ; OnPropertyChanged(); } }

	private String _source;
	[JsonProperty(Order = -1)]
	public String Source { get => _source; set { _source = value; OnPropertyChanged(); } }

	internal void SetParent(EndpointNode endpoint)
	{
		_endpoint = endpoint;
	}
}

