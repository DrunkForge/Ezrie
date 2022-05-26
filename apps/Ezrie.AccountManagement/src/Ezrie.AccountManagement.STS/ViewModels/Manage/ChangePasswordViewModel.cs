using System.ComponentModel.DataAnnotations;

namespace Ezrie.AccountManagement.STS.ViewModels.Manage;

public class ChangePasswordViewModel
{
	[Required]
	[DataType(DataType.Password)]
	public String OldPassword { get; set; } = String.Empty;

	[Required]
	[DataType(DataType.Password)]
	public String NewPassword { get; set; } = String.Empty;

	[DataType(DataType.Password)]
	[Compare("NewPassword")]
	public String ConfirmPassword { get; set; } = String.Empty;

	public String StatusMessage { get; set; } = String.Empty;
}

