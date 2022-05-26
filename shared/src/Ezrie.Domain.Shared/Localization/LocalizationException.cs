using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ezrie.Localization;

public class LocalizationException : EzrieException
{
	public LocalizationException(String? message) : base(message)
	{
	}

	public LocalizationException(String? message, Exception? innerException) : base(message, innerException)
	{
	}
}
