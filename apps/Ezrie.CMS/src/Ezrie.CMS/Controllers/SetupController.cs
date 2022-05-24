using CPCA.Presentation.CMS.Blocks;
using CPCA.Presentation.CMS.Models;
using CPCA.Presentation.CMS.Pages.DisplayTemplates;
using Microsoft.AspNetCore.Mvc;
using Piranha;
using Piranha.Extend;
using Piranha.Extend.Blocks;
using Piranha.Extend.Fields;
using Piranha.Models;

namespace CPCA.Presentation.CMS.Controllers;

/// <summary>
/// This controller is only used when the project is first started
/// and no pages has been added to the database. Feel free to remove it.
/// </summary>
[ApiExplorerSettings(IgnoreApi = true)]
public class SetupController : Controller
{
	private readonly IApi _api;
	private readonly Random _random = new Random();

	public SetupController(IApi api)
	{
		_api = api;
	}

	[Route("/")]
	public IActionResult Index()
	{
		return View();
	}

	[Route("/seed")]
	public async Task<IActionResult> Seed()
	{
		var images = new List<Guid>();

		// Get the default site
		var site = await _api.Sites.GetDefaultAsync();

		var nexaStockImages = new MediaFolder()
		{
			Name = "Nexa Stock Photos",
			Description = "You probably shouldn't use these."
		};
		await _api.Media.SaveFolderAsync(nexaStockImages);

		// Add media assets
		foreach (var image in Directory.GetFiles("seed"))
		{
			var info = new FileInfo(image);
			var id = Guid.NewGuid();
			images.Add(id);

			using (var stream = System.IO.File.OpenRead(image))
			{
				await _api.Media.SaveAsync(new StreamMediaContent()
				{
					Id = id,
					FolderId = nexaStockImages.Id,
					Filename = info.Name,
					Data = stream
				});
			}
		}

		var dirPage = await AddPage<DirectoryPage>(site, images, 10, "Counsellor Directory", "Directory", blocks: new HeadingBlock
		{
			Heading = new StringField() { Value = "Find a Counsellor" },
			Alignment = new SelectField<HorizontalAlignment> { Value = HorizontalAlignment.Center }
		});

		var counPage = await AddPage<CounsellorPage>(site, images, 11, "Counsellor Profile", "Counsellor", isHidden: true);

		var eduPage = await AddPage<StandardPage>(site, images, 20, "Education & Training",
			excerpt: "Praesent commodo cursus magna, vel scelerisque nisl consectetur et. Vestibulum id ligula porta felis euismod semper. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Nullam quis risus eget urna mollis ornare vel eu leo. Nullam id dolor id nibh ultricies vehicula ut id elit. Nullam quis risus eget urna mollis ornare vel eu leo. Aenean eu leo quam. Pellentesque ornare sem lacinia quam venenatis vestibulum.");
		await AddPage<StandardPage>(site, images, 21, "Approved Education Programs", parent: eduPage);
		await AddPage<StandardPage>(site, images, 22, "Professional Development", parent: eduPage);
		await AddPage<StandardPage>(site, images, 23, "CPCA Conferences", parent: eduPage);

		var memPage = await AddPage<StandardPage>(site, images, 30, "Membership");
		await AddPage<StandardPage>(site, images, 31, "Eligibility", parent: memPage);
		await AddPage<StandardPage>(site, images, 32, "Renewal & Good Standing", parent: memPage);
		await AddPage<StandardPage>(site, images, 33, "Membership Benefits", parent: memPage);
		await AddPage<StandardPage>(site, images, 34, "Membership Applications", parent: memPage);
		await AddPage<StandardPage>(site, images, 35, "Fees and Payments", parent: memPage);
		await AddPage<StandardPage>(site, images, 36, "Supporting Documents", parent: memPage);
		await AddPage<StandardPage>(site, images, 37, "Qualifying Examination", parent: memPage);
		await AddPage<StandardPage>(site, images, 38, "Upgrading Membership", parent: memPage);

		var partPage = await AddPage<StandardPage>(site, images, 40, "Partnerships");

		var resPage = await AddPage<StandardPage>(site, images, 50, "Resources");
		await AddPage<StandardPage>(site, images, 51, "International Affiliates", parent: resPage);
		await AddPage<StandardPage>(site, images, 52, "Member Publications", parent: resPage);
		await AddPage<StandardPage>(site, images, 53, "Member Articles", parent: resPage);
		await AddPage<StandardPage>(site, images, 54, "Frequently Asked Questions", parent: resPage);
		await AddPage<StandardPage>(site, images, 55, "CPCA Committees (Volunteer)", parent: resPage);
		await AddPage<StandardPage>(site, images, 56, "Job Opportunities", parent: resPage);
		await AddPage<StandardPage>(site, images, 57, "Regulatory College", parent: resPage);

		var abtPage = await AddPage<StandardPage>(site, images, 70, "About Us");
		await AddPage<StandardPage>(site, images, 71, "Who We Are", parent: abtPage, blocks: new HtmlBlock()
		{
			Body = "<p>The Canadian Professional Counsellors Association is a unique counselling association for several reasons. The most important reason is that the CPCA is the only counselling association that adheres to a Competency Based Model. You can learn more about the CPCA Competency Based Model by clicking on the linked text above.</p>"
			+ "<p>If you are unsure if your counselling program meets the required educational core competencies to become a Registered Professional Counsellor(RPC) or Registered Professional Counsellor Candidate(RPC - C) please go the Eligibility Tab for more direction.</p>"
			+ "<p>This list is evolving with programs evaluated every 3 years.Each approved program must notify the CPCA and keep the office of the Registrar informed of significant changes in curriculum or practicum to maintain this approval status.</p>"
			+ "<p>If you are an Educator Provider looking for academic approval of your counselling program, please email the National Registrar at registrar@theCPCA.ca.</p>"
		});
		await AddPage<StandardPage>(site, images, 72, "Vision", parent: abtPage);
		await AddPage<StandardPage>(site, images, 73, "Code of Ethics", parent: abtPage);
		await AddPage<StandardPage>(site, images, 74, "Disciplinary Procedures", parent: abtPage);
		await AddPage<StandardPage>(site, images, 75, "Competency Mandate", parent: abtPage);
		await AddPage<StandardPage>(site, images, 76, "Board of Directors", parent: abtPage);
		await AddPage<StandardPage>(site, images, 77, "Head Office", parent: abtPage);
		await AddPage<StandardPage>(site, images, 78, "Contact Us", parent: abtPage);

		var blogPage = await StandardArchive.CreateAsync(_api);
		blogPage.Id = Guid.NewGuid();
		blogPage.SiteId = site.Id;
		blogPage.Title = "Blog";
		blogPage.NavigationTitle = "Blog";
		blogPage.PrimaryImage = images[_random.Next(images.Count)];
		blogPage.SortOrder = 60;
		blogPage.Published = DateTime.Now;
		blogPage.IsHidden = true;
		blogPage.Excerpt = "Praesent commodo cursus magna, vel scelerisque nisl consectetur et. Vestibulum id ligula porta felis euismod semper. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Nullam quis risus eget urna mollis ornare vel eu leo. Nullam id dolor id nibh ultricies vehicula ut id elit. Nullam quis risus eget urna mollis ornare vel eu leo. Aenean eu leo quam. Pellentesque ornare sem lacinia quam venenatis vestibulum.";

		await _api.Pages.SaveAsync(blogPage);

		// Add blog posts
		var post1 = await StandardPost.CreateAsync(_api);
		post1.BlogId = blogPage.Id;
		post1.Category = "Magna";
		post1.Tags.Add("Euismod", "Ridiculus");
		post1.Title = "Tortor Magna Ultricies";
		post1.MetaKeywords = "Nibh, Vulputate, Venenatis, Ridiculus";
		post1.MetaDescription = "Integer posuere erat a ante venenatis dapibus posuere velit aliquet. Maecenas faucibus mollis interdum.";
		post1.PrimaryImage = images[_random.Next(images.Count)];
		post1.Excerpt = "Maecenas faucibus mollis interdum. Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec sed odio dui.";
		post1.Published = DateTime.Now;

		post1.Blocks.Add(new HtmlBlock
		{
			Body =
				"<p>Praesent commodo cursus magna, vel scelerisque nisl consectetur et. Vestibulum id ligula porta felis euismod semper. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Nullam quis risus eget urna mollis ornare vel eu leo. Nullam id dolor id nibh ultricies vehicula ut id elit. Nullam quis risus eget urna mollis ornare vel eu leo. Aenean eu leo quam. Pellentesque ornare sem lacinia quam venenatis vestibulum.</p>" +
				"<p>Aenean eu leo quam. Pellentesque ornare sem lacinia quam venenatis vestibulum. Praesent commodo cursus magna, vel scelerisque nisl consectetur et. Maecenas sed diam eget risus varius blandit sit amet non magna. Nullam id dolor id nibh ultricies vehicula ut id elit. Maecenas faucibus mollis interdum. Cras mattis consectetur purus sit amet fermentum. Donec ullamcorper nulla non metus auctor fringilla.</p>" +
				"<p>Sed posuere consectetur est at lobortis. Maecenas faucibus mollis interdum. Sed posuere consectetur est at lobortis. Morbi leo risus, porta ac consectetur ac, vestibulum at eros. Nullam id dolor id nibh ultricies vehicula ut id elit. Maecenas faucibus mollis interdum.</p>"
		});
		await _api.Posts.SaveAsync(post1);

		var post2 = await StandardPost.CreateAsync(_api);
		post2.BlogId = blogPage.Id;
		post2.Category = "Tristique";
		post2.Tags.Add("Euismod", "Ridiculus");
		post2.Title = "Sollicitudin Risus Dapibus";
		post2.MetaKeywords = "Nibh, Vulputate, Venenatis, Ridiculus";
		post2.MetaDescription = "Integer posuere erat a ante venenatis dapibus posuere velit aliquet. Maecenas faucibus mollis interdum.";
		post2.PrimaryImage = images[_random.Next(images.Count)];
		post2.Excerpt = "Donec sed odio dui. Maecenas faucibus mollis interdum. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.";
		post2.Published = DateTime.Now;

		post2.Blocks.Add(new HtmlBlock
		{
			Body =
				"<p>Praesent commodo cursus magna, vel scelerisque nisl consectetur et. Vestibulum id ligula porta felis euismod semper. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Nullam quis risus eget urna mollis ornare vel eu leo. Nullam id dolor id nibh ultricies vehicula ut id elit. Nullam quis risus eget urna mollis ornare vel eu leo. Aenean eu leo quam. Pellentesque ornare sem lacinia quam venenatis vestibulum.</p>" +
				"<p>Aenean eu leo quam. Pellentesque ornare sem lacinia quam venenatis vestibulum. Praesent commodo cursus magna, vel scelerisque nisl consectetur et. Maecenas sed diam eget risus varius blandit sit amet non magna. Nullam id dolor id nibh ultricies vehicula ut id elit. Maecenas faucibus mollis interdum. Cras mattis consectetur purus sit amet fermentum. Donec ullamcorper nulla non metus auctor fringilla.</p>" +
				"<p>Sed posuere consectetur est at lobortis. Maecenas faucibus mollis interdum. Sed posuere consectetur est at lobortis. Morbi leo risus, porta ac consectetur ac, vestibulum at eros. Nullam id dolor id nibh ultricies vehicula ut id elit. Maecenas faucibus mollis interdum.</p>"
		});
		await _api.Posts.SaveAsync(post2);

		var post3 = await StandardPost.CreateAsync(_api);
		post3.Id = Guid.NewGuid();
		post3.BlogId = blogPage.Id;
		post3.Category = "Piranha";
		post3.Tags.Add("Development", "Release Info");
		post3.Title = "What's New In 10.0";
		post3.Slug = "whats-new";
		post3.MetaKeywords = "Piranha, Version, Information";
		post3.MetaDescription = "Here you can find information about what's included in the current release.";
		post3.PrimaryImage = images[_random.Next(images.Count)];
		post3.Excerpt = "Here you can find information about what's included in the current release.";
		post3.Published = DateTime.Now;

		post3.Blocks.Add(new HtmlBlock
		{
			Body =
				"<p class=\"lead\">Big thanks to <a href=\"https://github.com/aatmmr\">@aatmmr</a>, <a href=\"https://github.com/brianpopow\">@brianpopow</a> and <a href=\"https://github.com/tedvanderveen\">@tedvanderveen</a> for their contributions and all of the people who has helped with translating the manager.</p>"
		});
		post3.Blocks.Add(new ColumnBlock
		{
			Items = new List<Block>
			{
				new MarkdownBlock
				{
					Body =
						"#### Core\n\n" +
						"* Remove the need to use MARS for SQL Server [#1417](https://github.com/piranhacms/piranha.core/issues/1417)\n" +
						"* Detect EXIF orientation on mobile pictures [#1442](https://github.com/piranhacms/piranha.core/issues/1442)\n" +
						"* Update BlobStorage to use Azure.Storage.Blobs [#1564](https://github.com/piranhacms/piranha.core/pull/1564)\n" +
						"* Update Pomelo.EntityFrameworkCore.MySql [#1646](https://github.com/piranhacms/piranha.core/pull/1646)\n" +
						"* Add sort order to fields [#1732](https://github.com/piranhacms/piranha.core/issues/1732)\n" +
						"* Update to .NET 6 [#1733](https://github.com/piranhacms/piranha.core/issues/1733)\n" +
						"* Use Identify to get image width and height [#1734](https://github.com/piranhacms/piranha.core/pull/1734)\n" +
						"* Clean up application startup [#1738](https://github.com/piranhacms/piranha.core/issues/1738)\n" +
						"* Add markdown block [#1744](https://github.com/piranhacms/piranha.core/issues/1744)\n" +
						"* Remove description attributes [#1747](https://github.com/piranhacms/piranha.core/issues/1747)\n\n" +
						"#### Manager\n\n" +
						"* Add content settings (with region support) [#1524](https://github.com/piranhacms/piranha.core/issues/1524)\n" +
						"* Update SummerNote package [#1730](https://github.com/piranhacms/piranha.core/issues/1730)\n" +
						"* Manager security update [#1741](https://github.com/piranhacms/piranha.core/issues/1741)\n" +
						"* Redesign Add page button in manager [#1748](https://github.com/piranhacms/piranha.core/issues/1748)\n\n" +
						"#### Bug Fixes\n\n" +
						"* Cannot access disposed object [#1701](https://github.com/piranhacms/piranha.core/issues/1701)\n" +
						"* Invalid PageField URL in Manager [#1705](https://github.com/piranhacms/piranha.core/issues/1705)\n"
				},
				new ImageBlock
				{
					Body = images[_random.Next(images.Count)]
				}
			}
		});

		await _api.Posts.SaveAsync(post3);

		var comment = new PostComment
		{
			Author = "Hari Seldon",
			Email = "hari@psychohistory.com",
			Url = "http://psychohistory.com",
			Body = "Awesome to see that the project is up and running! Now maybe it's time to start customizing it to your needs. You can find a lot of information in the official docs.",
			IsApproved = true
		};
		await _api.Posts.SaveCommentAsync(post3.Id, comment);

		// Add Home page
		var homePage = await AddPage<StandardPage>(site, images, 0, "Canadian Professional Counsellors Association", "CPCA", isHidden: true);

		homePage.Blocks.Add(new WindowBlock()
		{
			Body = "<h1>An Unexpected Party</h1> <p>In a hole in the ground there lived a hobbit. Not a nasty, dirty, wet hole, filled with the ends of worms and an oozy smell, nor yet a dry, bare, sandy hole with nothing in it to sit down on or to eat: it was a hobbit - hole, and that means comfort.</p>"
				 + "<p>It had a perfectly round door like a porthole, painted green, with a shiny yellow brass knob in the exact middle.The door opened on to a tube - shaped hall like a tunnel: a very comfortable tunnel without smoke, with panelled walls, and floors tiled and carpeted, provided with polished chairs, and lots and lots of pegs for hats and coats - the hobbit was fond of visitors.The tunnel wound on and on, going fairly but not quite straight into the side of the hill - The Hill, as all the people for many miles round called it - and many little round doors opened out of it, first on one side and then on another.No going upstairs for the hobbit: bedrooms, bathrooms, cellars, pantries(lots of these), wardrobes(he had whole rooms devoted to clothes), kitchens, dining - rooms, all were on the same floor, and indeed on the same passage.The best rooms were all on the left - hand side(going in), for these were the only ones to have windows, deep - set round windows looking over his garden and meadows beyond, sloping down to the river.</p>"
				 + "<p>This hobbit was a very well-to-do hobbit, and his name was Baggins.The Bagginses had lived in the neighbourhood of The Hill for time out of mind, and people considered them very respectable, not only because most of them were rich, but also because they never had any adventures or did anything unexpected: you could tell what a Baggins would say on any question without the bother of asking him.This is a story of how a Baggins had an adventure, found himself doing and saying things altogether unexpected.He may have lost the neighbours' respect, but he gained-well, you will see whether he gained anything in the end.</p>",
			Height = new() { Value = 300 },
			Image = images[_random.Next(images.Count)]
		});

		homePage.Blocks.Add(new AccordionBlock()
		{
			Items = new List<Block>() {
						new AccordionItemBlock(){
							Title = "The Hole",
							Body = "In a hole in the ground there lived a hobbit. Not a nasty, dirty, wet hole, filled with the ends of worms and an oozy smell, nor yet a dry, bare, sandy hole with nothing in it to sit down on or to eat: it was a hobbit-hole, and that means comfort."
						},
						new AccordionItemBlock(){
							Title = "The Hill",
							Body = "It had a perfectly round door like a porthole, painted green, with a shiny yellow brass knob in the exact middle. The door opened on to a tube-shaped hall like a tunnel: a very comfortable tunnel without smoke, with panelled walls, and floors tiled and carpeted, provided with polished chairs, and lots and lots of pegs for hats and coats - the hobbit was fond of visitors. The tunnel wound on and on, going fairly but not quite straight into the side of the hill - The Hill, as all the people for many miles round called it - and many little round doors opened out of it, first on one side and then on another. No going upstairs for the hobbit: bedrooms, bathrooms, cellars, pantries (lots of these), wardrobes (he had whole rooms devoted to clothes), kitchens, dining-rooms, all were on the same floor, and indeed on the same passage. The best rooms were all on the left-hand side (going in), for these were the only ones to have windows, deep-set round windows looking over his garden and meadows beyond, sloping down to the river."
						},
						new AccordionItemBlock(){
							Title = "The Hobbit",
							Body = "This hobbit was a very well-to-do hobbit, and his name was Baggins. The Bagginses had lived in the neighbourhood of The Hill for time out of mind, and people considered them very respectable, not only because most of them were rich, but also because they never had any adventures or did anything unexpected: you could tell what a Baggins would say on any question without the bother of asking him. This is a story of how a Baggins had an adventure, found himself doing and saying things altogether unexpected. He may have lost the neighbours' respect, but he gained-well, you will see whether he gained anything in the end."
						}
					}
		});

		homePage.Blocks.Add(new AccordionBlock()
		{
			Inverse = true,
			Alignment = new SelectField<HorizontalAlignment>() { Value = HorizontalAlignment.Left },
			Items = new List<Block>() {
						new AccordionItemBlock(){
							Title = "The Hole",
							Body = "In a hole in the ground there lived a hobbit. Not a nasty, dirty, wet hole, filled with the ends of worms and an oozy smell, nor yet a dry, bare, sandy hole with nothing in it to sit down on or to eat: it was a hobbit-hole, and that means comfort."
						},
						new AccordionItemBlock(){
							Title = "The Hill",
							Body = "It had a perfectly round door like a porthole, painted green, with a shiny yellow brass knob in the exact middle. The door opened on to a tube-shaped hall like a tunnel: a very comfortable tunnel without smoke, with panelled walls, and floors tiled and carpeted, provided with polished chairs, and lots and lots of pegs for hats and coats - the hobbit was fond of visitors. The tunnel wound on and on, going fairly but not quite straight into the side of the hill - The Hill, as all the people for many miles round called it - and many little round doors opened out of it, first on one side and then on another. No going upstairs for the hobbit: bedrooms, bathrooms, cellars, pantries (lots of these), wardrobes (he had whole rooms devoted to clothes), kitchens, dining-rooms, all were on the same floor, and indeed on the same passage. The best rooms were all on the left-hand side (going in), for these were the only ones to have windows, deep-set round windows looking over his garden and meadows beyond, sloping down to the river."
						},
						new AccordionItemBlock(){
							Title = "The Hobbit",
							Body = "This hobbit was a very well-to-do hobbit, and his name was Baggins. The Bagginses had lived in the neighbourhood of The Hill for time out of mind, and people considered them very respectable, not only because most of them were rich, but also because they never had any adventures or did anything unexpected: you could tell what a Baggins would say on any question without the bother of asking him. This is a story of how a Baggins had an adventure, found himself doing and saying things altogether unexpected. He may have lost the neighbours' respect, but he gained-well, you will see whether he gained anything in the end."
						}
					}
		});

		homePage.Blocks.Add(new ColumnBlock()
		{
			Items = new List<Block>() {
				new CardBlock()
				{
					Image = images[_random.Next(images.Count)],
					Title = blogPage.Title,
					SubTitle = "By Cary Grant",
					Content = "These cases are perfectly simple and easy to distinguish. In a free hour, when our power of choice is untrammelled and when nothing prevents our being able to do what we like best, every pleasure is to be welcomed and every pain avoided. But in certain circumstances and owing to the claims of duty or the obligations of business it will frequently occur that pleasures have to be repudiated and annoyances accepted. The wise man therefore always holds in these matters to this principle of selection: he rejects pleasures to secure other greater pleasures, or else he endures pains to avoid worse pains.",
					Link = blogPage,
					Button = "Vestibulum id ligula porta ..."
				},
				new CardBlock()
				{
					Image = images[_random.Next(images.Count)],
					Title = eduPage.Title,
					SubTitle = "Joan Fontaine",
					Content = "Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur?",
					Link = eduPage,
					Button = "Sed posuere consectetur est?"
				},
			}
		});

		homePage.Blocks.Add(new HeadingBlock
		{
			Heading = new StringField() { Value = "Welcome!" },
			Alignment = new SelectField<HorizontalAlignment> { Value = HorizontalAlignment.Center }
		});

		homePage.Blocks.Add(new HtmlBlock
		{
			Body = "<p>The Canadian Professional Counsellors Association (CPCA) is a nationally recognized, independent, self-regulated, non-profit organization registered in accordance with the National Corporate Registry of Canada. The Association is governed by a National Board of Directors which is elected by its members from all regions across Canada. </p>"
				 + "<p>The CPCA has an established regulatory framework for its members that is built on the foundations of competency and compliance in both registration and discipline. We are dedicated to the promotion of public confidence and trust in the counselling profession throughout Canada.</p>"
				 + "<p>The CPCA has been committed to the Competency Model and Voluntary Self-Regulation since 1990 from coast to coast to coast.</p>"
		});

		homePage.Blocks.Add(new HeadingBlock
		{
			Heading = new StringField() { Value = "Mission Statement" },
			Description = "Working together to promote and support competency in clinical counselling & psychotherapy.",
			Alignment = new SelectField<HorizontalAlignment> { Value = HorizontalAlignment.Center }
		});

		homePage.Blocks.Add(new HtmlBlock
		{
			Body = "<p>To foster public protection by regulating its members’ professional practice through a comprehensive Code of Ethics, competency-based assessment, ongoing evaluation, and clear expectation of compliance with professional Standards of Practice. </p>"
				 + "<p>To promote and support members to develop excellence in professional skills for competent clinical counselling/psychotherapy.</p>"
				 + "<p>The Association's leadership has shown that it will distinguish between the public interest and the professionals’ self-interest and in self-regulating will favour the former.</p>"
				 + "<p>In the event that an individual has been harmed or is dissatisfied with the actions or behaviours of a CPCA registered member they may submit their complaint in writing to:</p>"
				 + "<p class=\"text-center\">Canadian Professional Counsellors Association<br />"
				 + "3306 - 32nd Avenue, Suite 203<br />"
				 + "Vernon, BC  V1T 2M6<br />"
				 + "Canada<br />"
				 + "<p>All submitted complaints are delivered to the Complaints Committee and subsequently referred to the Discipline Committee when further action is merited.</p>"
		});

		homePage.Blocks.Add(new ColumnBlock
		{
			Items = new List<Block>
			{
				new PageBlock
				{
					Body = blogPage
				},
				new PageBlock
				{
					Body = eduPage
				}
			}
		});

		homePage.Blocks.Add(new HtmlBlock
		{
			Body =
				"<h2>Share Your Images</h2>" +
				"<p>An image says more that a thousand words. With our <strong>Image Gallery</strong> you can easily create a gallery or carousel and share anything you have available in your media library or download new images directly on your page by just dragging them to your browser.</p>"
		});

		homePage.Blocks.Add(new ImageGalleryBlock
		{
			Items = new List<Block>
			{
				new ImageBlock
				{
					Body = images[_random.Next(images.Count)]
				},
				new ImageBlock
				{
					Body = images[_random.Next(images.Count)]
				},
				new ImageBlock
				{
					Body = images[_random.Next(images.Count)]
				}
			}
		});

		await _api.Pages.SaveAsync(homePage);

		return Redirect("~/");
	}

	private async Task<T> AddPage<T>(Site site, List<Guid> images, Int32 uiOrder, String title, String? navTitle = null, String? excerpt = null, String? route = null, PageBase? parent = null, Boolean isHidden = false, params Block[] blocks)
		where T : GenericPage<T>
	{
		var page = await _api.Pages.CreateAsync<T>(null);
		page.Id = Guid.NewGuid();
		if (parent != null)
			page.ParentId = parent.Id;
		page.NavigationTitle = navTitle ?? title;
		page.PrimaryImage = images[_random.Next(images.Count)];
		page.Published = DateTime.Now;
		page.Route = route;
		page.SiteId = site.Id;
		page.SortOrder = uiOrder;
		page.Title = title;
		page.IsHidden = isHidden;
		page.Excerpt = excerpt;

		foreach (var block in blocks)
			page.Blocks.Add(block);

		await _api.Pages.SaveAsync(page);

		return page;
	}
}
