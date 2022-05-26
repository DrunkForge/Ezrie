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

using Skoruba.AuditLogging.Constants;
using Skoruba.AuditLogging.Events;
using System.Security.Claims;

namespace Ezrie.AccountManagement.Configuration.AuditLogging;

public class ApiAuditSubject : IAuditSubject
{
	public ApiAuditSubject(IHttpContextAccessor accessor, AuditLoggingConfiguration auditLoggingConfiguration)
	{
		var clientIdClaim = accessor.HttpContext?.User.FindFirst(auditLoggingConfiguration.ClientIdClaim)
			?? new Claim(String.Empty, "Uknown Subject");

		var subClaim = accessor.HttpContext?.User.FindFirst(auditLoggingConfiguration.SubjectIdentifierClaim);
		var nameClaim = accessor.HttpContext?.User.FindFirst(auditLoggingConfiguration.SubjectNameClaim);

		SubjectIdentifier = subClaim == null ? clientIdClaim.Value : subClaim.Value;
		SubjectName = nameClaim == null ? clientIdClaim.Value : nameClaim.Value;
		SubjectType = subClaim == null ? AuditSubjectTypes.Machine : AuditSubjectTypes.User;

		SubjectAdditionalData = new
		{
			RemoteIpAddress = accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
			LocalIpAddress = accessor.HttpContext?.Connection?.LocalIpAddress?.ToString(),
			Claims = accessor.HttpContext?.User.Claims?.Select(x => new { x.Type, x.Value })
		};
	}

	public String SubjectName { get; set; } = String.Empty;

	public String SubjectType { get; set; } = String.Empty;

	public Object SubjectAdditionalData { get; set; }

	public String SubjectIdentifier { get; set; } = String.Empty;
}
