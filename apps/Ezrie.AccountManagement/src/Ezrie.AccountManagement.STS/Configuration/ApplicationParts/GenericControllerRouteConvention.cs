using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Ezrie.AccountManagement.STS.Configuration.ApplicationParts;

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

