using System.ComponentModel.DataAnnotations;

namespace Ezrie.AccountManagement.STS.ViewModels.Manage;

public class ChangePasswordViewModel
{
	[Required]
	[DataType(DataType.Password)]
	public String OldPassword { get; set; }

	[Required]
	[DataType(DataType.Password)]
	public String NewPassword { get; set; }

	[DataType(DataType.Password)]
	[Compare("NewPassword")]
	public String ConfirmPassword { get; set; }

	public String StatusMessage { get; set; }
}

