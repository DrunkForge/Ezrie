using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Volo.Abp.TenantManagement;

namespace Ezrie.CRM.Pages;

public partial class Tenants
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	[Inject] private ITenantAppService AppService { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

	private IReadOnlyList<TenantDto> TenantList { get; set; } = new List<TenantDto>();
	private Guid EditingTenantId { get; set; }
	private Boolean Loading { get; set; }
	private Boolean NewDialogOpen { get; set; }
	private Boolean EditingDialogOpen { get; set; }

	protected override async Task OnAfterRenderAsync(Boolean firstRender)
	{
		if (firstRender)
		{
			await GetTenantsAsync();
		}

		await base.OnAfterRenderAsync(firstRender);
	}

	private async Task GetTenantsAsync()
	{
		try
		{
			Loading = true;

			await InvokeAsync(() => StateHasChanged());

			var list = await AppService.GetListAsync(new GetTenantsInput());
			TenantList = list.Items.ToList();
		}
		finally
		{
			Loading = false;

			await InvokeAsync(() => StateHasChanged());
		}
	}

	private async Task DeleteTenantAsync(TenantDto book)
	{
		var confirmMessage = L["TenantDeletionConfirmationMessage", book.Name];
		if (!await Message.Confirm(confirmMessage))
		{
			return;
		}

		await AppService.DeleteAsync(book.Id);
		await GetTenantsAsync();
	}
}
