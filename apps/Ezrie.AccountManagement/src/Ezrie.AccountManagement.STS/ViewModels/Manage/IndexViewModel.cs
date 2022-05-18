using System.ComponentModel.DataAnnotations;

namespace Ezrie.AccountManagement.STS.ViewModels.Manage;

public class IndexViewModel
{
	public String Username { get; set; }

	public Boolean IsEmailConfirmed { get; set; }

	[Required]
	[EmailAddress]
	public String Email { get; set; }

	[Phone]
	[Display(Name = "Phone number")]
	public String PhoneNumber { get; set; }

	public String StatusMessage { get; set; }
	[MaxLength(255)]
	[Display(Name = "Full Name")]
	public String Name { get; set; }

	[MaxLength(255)]
	[Display(Name = "Website")]
	public String Website { get; set; }

	[MaxLength(255)]
	[Display(Name = "Profile")]
	public String Profile { get; set; }

	[MaxLength(255)]
	[Display(Name = "Street Address")]
	public String StreetAddress { get; set; }

	[MaxLength(255)]
	[Display(Name = "City")]
	public String Locality { get; set; }

	[MaxLength(255)]
	[Display(Name = "Region")]
	public String Region { get; set; }

	[MaxLength(255)]
	[Display(Name = "Postal Code")]
	public String PostalCode { get; set; }

	[MaxLength(255)]
	[Display(Name = "Country")]
	public String Country { get; set; }
}

