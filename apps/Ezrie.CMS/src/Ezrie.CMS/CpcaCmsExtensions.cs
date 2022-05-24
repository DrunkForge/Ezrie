using CPCA.Application;
using CPCA.Application.Directory;
using CPCA.Application.Members;
using CPCA.Authorization;
using CPCA.Configuration;
using CPCA.Helpers;
using CPCA.Persistence;
using CPCA.Services;
using Flurl;
using Markdig;
using Microsoft.AspNetCore.Html;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using System;
using System.Text.RegularExpressions;

namespace CPCA;

public static class CpcaCmsExtensions
{
	public static void AddCpcaCms(this WebApplicationBuilder builder)
	{
		CpcaApplication.SetEnvironment(builder.Environment.EnvironmentName);
		var detailedErrors = builder.Configuration.GetSection("DetailedErrors").Get<Boolean>();

		builder.Logging.ConfigureCpcaLogging<CpcaCmsModule>(builder.Configuration);

		builder.Services.AddDbContext<ApplicationDbContext>(config
			=> config.UseSqlServer(builder.Configuration.GetNamedOrDefaultConnectionString(CpcaConsts.ApplicationDatabase),
					options => options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
				.EnableDetailedErrors()
				.EnableSensitiveDataLogging(detailedErrors));

		builder.Services.AddScoped<DbContext, ApplicationDbContext>();
		builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

		builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
		builder.Services.AddScoped<IDirectoryService, DirectoryService>();
		builder.Services.AddSingleton<IClock>(SystemClock.Instance);
		builder.Services.AddSingleton<IIdGenerator, TimeBasedIdGenerator>();
		builder.Services.AddSingleton<IUserAccessor, UserAccessor>();

		builder.Services.AddAutoMapper(CpcaConfiguration.GetSafeAssemblies());
	}

	public static HtmlString PhotoUrl(this FullMemberDto member)
		=> new(CpcaCms.CdnBaseUrl.AppendPathSegment(member.PhotoPath()));

	public static Boolean HasWebsite(this FullMemberDto member) => Url.IsValid(member.DirectoryListing.Website);

	public static HtmlString Website(this FullMemberDto member)
	{
		try
		{
			return new(Url.Parse(member.DirectoryListing.Website));
		}
		catch
		{
			return HtmlString.Empty;
		}
	}

	public static HtmlString WebsiteUrl(this FullMemberDto member)
	{
		try
		{
			var url = Url.Parse(member.DirectoryListing.Website);
			url.Scheme = "https";

			return new(url);
		}
		catch
		{
			return HtmlString.Empty;
		}
	}

	public static HtmlString EmailUrl(this FullMemberDto member)
		=> new($"mailto:{member.DirectoryListing.Email}?subject=An Inquiry via the CPCA Counsellor Directory");
	public static HtmlString TelephoneUrl(this FullMemberDto member)
		=> new($"tel:{member.DirectoryListing.Phone.FormatPhone()}");

	private static readonly Regex ReplaceNonAlphaNumeric = new("[^a-zA-Z]+", RegexOptions.Compiled);
	public static HtmlString ProfileUrl(this FullMemberDto member)
		=> new(CpcaCms.CmsBaseUrl
			.AppendPathSegment("Counsellor")
			.AppendPathSegment(member.MembershipNumber)
			.AppendPathSegment(ReplaceNonAlphaNumeric.Replace(member.FullName, "_"))
			);

	private static readonly MarkdownPipeline Pipeline = new MarkdownPipelineBuilder().Configure("Bootstrap").Build();
	/// <summary>
	/// Convert Markdown to HTML
	/// </summary>
	/// <param name="markdown"></param>
	/// <returns></returns>
	public static HtmlString RenderMarkdown(this String? markdown) => new(Markdown.ToHtml(markdown ?? String.Empty, Pipeline));
}
