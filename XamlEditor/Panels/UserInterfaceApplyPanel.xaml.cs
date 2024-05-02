// Copyright © 2022-2024 Oleksandr Kukhtin. All rights reserved.

using System.Windows.Controls;

namespace XamlEditor;

public partial class UserInterfaceApplyPanel : UserControl
{
	private readonly EndpointNode _endpoint;
	public UserInterfaceApplyPanel(EndpointNode endpoint)
	{
		_endpoint = endpoint;	
		InitializeComponent();
	}
}
