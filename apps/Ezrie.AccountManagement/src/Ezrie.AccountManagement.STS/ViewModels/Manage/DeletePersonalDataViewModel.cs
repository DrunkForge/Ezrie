using System.ComponentModel.DataAnnotations;

namespace Ezrie.AccountManagement.STS.ViewModels.Manage;

public class DeletePersonalDataViewModel
{
	public Boolean RequirePassword { get; set; }

	[DataType(DataType.Password)]
	[Required]
	public String Password { get; set; } = String.Empty;
}

