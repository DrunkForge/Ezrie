using IdentityServer4.Events;
using IdentityServer4.Services;

namespace Ezrie.AccountManagement.STS.Services;

public class AuditEventSink : DefaultEventSink
{
	public AuditEventSink(ILogger<DefaultEventService> logger) : base(logger)
	{
	}

	public override Task PersistAsync(Event evt)
	{
		return base.PersistAsync(evt);
	}
}

