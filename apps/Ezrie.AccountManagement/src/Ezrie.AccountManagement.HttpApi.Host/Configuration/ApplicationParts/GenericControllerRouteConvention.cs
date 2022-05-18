/*********************************************************************************************
* EzrieCRM
* Copyright (C) 2022 Doug Wilson (info@dougwilson.ca)
* 
* This program is free software: you can redistribute it and/or modify it under the terms of
* the GNU Affero General Public License as published by the Free Software Foundation, either
* version 3 of the License, or (at your option) any later version.
* 
* This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY
* without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
* See the GNU Affero General Public License for more details.
* 
* You should have received a copy of the GNU Affero General Public License along with this
* program. If not, see <https://www.gnu.org/licenses/>.
*********************************************************************************************/

using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Ezrie.AccountManagement.Configuration.ApplicationParts;

public class GenericControllerRouteConvention : IControllerModelConvention
{
	public void Apply(ControllerModel controller)
	{
		if (controller.ControllerType.IsGenericType)
		{
			// this change is required because some of the controllers have generic parameters
			// and require resolution that will remove arity from the type 
			// as well as remove the 'Controller' at the end of string

			var name = controller.ControllerType.Name;
			var nameWithoutArity = name[..name.IndexOf('`', StringComparison.OrdinalIgnoreCase)];
			controller.ControllerName = nameWithoutArity[..nameWithoutArity.LastIndexOf("Controller", StringComparison.OrdinalIgnoreCase)];
		}
	}
}

