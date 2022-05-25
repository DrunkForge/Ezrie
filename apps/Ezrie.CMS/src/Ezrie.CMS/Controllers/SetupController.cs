using Ezrie.CMS.Blocks;
using Ezrie.CMS.Models;
using Ezrie.CMS.Pages.DisplayTemplates;
using Microsoft.AspNetCore.Mvc;
using Piranha;
using Piranha.Extend;
using Piranha.Extend.Blocks;
using Piranha.Extend.Fields;
using Piranha.Models;

namespace Ezrie.CMS.Controllers;

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
	[SuppressMessage("Security", "CA5394:Do not use insecure randomness", Justification = "It's good enough for this situation.")]
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

		var abtPage = await AddPage<StandardPage>(site, images, 70, "About Us");
		await AddPage<StandardPage>(site, images, 71, "Who We Are", parent: abtPage, blocks: new HtmlBlock()
		{
			Body = "<p>Far out in the uncharted backwaters of the unfashionable end  of  the western spiral arm of the Galaxy lies a small unregarded yellow sun.</p>"
				+ "<p>Orbiting this at a distance of roughly ninety-two million miles is an utterly insignificant little blue green  planet  whose  apedescended  life forms are so amazingly primitive that they still think digital watches are a pretty neat idea.</p>"
				+ "<p>This planet has - or rather had - a problem, which was this: most of the people on it were unhappy for pretty much of the time. Many solutions were suggested for this problem, but most of these were largely  concerned with the movements of small green pieces of paper, which is odd because on the whole it wasn't the small green pieces of paper that were unhappy. </p>"
				+ "<p>And so the problem remained; lots of the people were mean,  and  most of them were miserable, even the ones with digital watches.</p>"
				+ "<p>Many were increasingly of the opinion that  they'd  all  made  a  big mistake in coming down from the trees in the first place.  And  some  said that even the trees had been a bad move, and that no one should ever  have left the oceans.</p>"
				+ "<p>And then, one Thursday, nearly two thousand years after one  man  had been nailed to a tree for saying how great it  would  be  to  be  nice  to people for a change, one girl sitting on  her  own  in  a  small  cafe  in Rickmansworth suddenly realized what it was that had been going wrong  all this time, and she finally knew how the world could be  made  a  good  and happy place. This time it was right, it would work, and no one would  have to get nailed to anything.</p>"
				+ "<p>Sadly, however, before she could get to a phone to tell anyone  about it, a terribly stupid catastrophe occurred, and the idea was lost forever.</p>"
				+ "<p>This is not her story.</p>"
				+ "<p>But it is the story of that terrible stupid catastrophe and  some  of its consequences.</p>"
				+ "<p>It is also the story of a book, a book called The Hitch Hiker's Guide to the Galaxy - not an Earth book, never published on Earth, and until the terrible catastrophe occurred, never seen or heard of by any Earthman.</p>"
				+ "<p>Nevertheless, a wholly remarkable book.</p>"
				+ "<p>In fact it was probably the most remarkable book ever to come out  of the great publishing houses of Ursa Minor - of which no Earthman had  ever heard either.</p>"
				+ "<p>Not only is it  a  wholly  remarkable  book,  it  is  also  a  highly successful one - more popular than the Celestial Home Care Omnibus, better selling  than  Fifty  More  Things  to  do  in  Zero  Gravity,  and   more controversial than Oolon Colluphid's trilogy of philosophical blockbusters Where God Went Wrong, Some More of God's Greatest Mistakes and Who is this God Person Anyway?</p>"
				+ "<p>In many of the more relaxed civilizations on the Outer Eastern Rim of the Galaxy, the Hitch Hiker's  Guide  has  already  supplanted  the  great Encyclopedia Galactica as the standard repository  of  all  knowledge  and wisdom, for though it  has  many  omissions  and  contains  much  that  is apocryphal, or at least wildly inaccurate, it scores over the older,  more pedestrian work in two important respects.</p>"
				+ "<p>First, it is slightly cheaper; and secondly it has  the  words  Don't Panic inscribed in large friendly letters on its cover.</p>"
				+ "<p>But the story of this terrible, stupid Thursday,  the  story  of  its extraordinary consequences, and the story of how  these  consequences  are inextricably intertwined with this remarkable book begins very simply.</p>"
				+ "<p>It begins with a house.</p>"
	});

		// Add Home page
		var homePage = await AddPage<StandardPage>(site, images, 0, "Ezrie Software Solutions", "Ezrie", isHidden: true);

		homePage.Blocks.Add(new WindowBlock()
		{
			Body = "<p>Far out in the uncharted backwaters of the unfashionable end  of  the western spiral arm of the Galaxy lies a small unregarded yellow sun.</p>"
				+ "<p>Orbiting this at a distance of roughly ninety-two million miles is an utterly insignificant little blue green  planet  whose  apedescended  life forms are so amazingly primitive that they still think digital watches are a pretty neat idea.</p>"
				+ "<p>This planet has - or rather had - a problem, which was this: most of the people on it were unhappy for pretty much of the time. Many solutions were suggested for this problem, but most of these were largely  concerned with the movements of small green pieces of paper, which is odd because on the whole it wasn't the small green pieces of paper that were unhappy. </p>"
				+ "<p>And so the problem remained; lots of the people were mean,  and  most of them were miserable, even the ones with digital watches.</p>"
				+ "<p>Many were increasingly of the opinion that  they'd  all  made  a  big mistake in coming down from the trees in the first place.  And  some  said that even the trees had been a bad move, and that no one should ever  have left the oceans.</p>"
				+ "<p>And then, one Thursday, nearly two thousand years after one  man  had been nailed to a tree for saying how great it  would  be  to  be  nice  to people for a change, one girl sitting on  her  own  in  a  small  cafe  in Rickmansworth suddenly realized what it was that had been going wrong  all this time, and she finally knew how the world could be  made  a  good  and happy place. This time it was right, it would work, and no one would  have to get nailed to anything.</p>"
				+ "<p>Sadly, however, before she could get to a phone to tell anyone  about it, a terribly stupid catastrophe occurred, and the idea was lost forever.</p>"
				+ "<p>This is not her story.</p>"
				+ "<p>But it is the story of that terrible stupid catastrophe and  some  of its consequences.</p>"
				+ "<p>It is also the story of a book, a book called The Hitch Hiker's Guide to the Galaxy - not an Earth book, never published on Earth, and until the terrible catastrophe occurred, never seen or heard of by any Earthman.</p>"
				+ "<p>Nevertheless, a wholly remarkable book.</p>"
				+ "<p>In fact it was probably the most remarkable book ever to come out  of the great publishing houses of Ursa Minor - of which no Earthman had  ever heard either.</p>"
				+ "<p>Not only is it  a  wholly  remarkable  book,  it  is  also  a  highly successful one - more popular than the Celestial Home Care Omnibus, better selling  than  Fifty  More  Things  to  do  in  Zero  Gravity,  and   more controversial than Oolon Colluphid's trilogy of philosophical blockbusters Where God Went Wrong, Some More of God's Greatest Mistakes and Who is this God Person Anyway?</p>"
				+ "<p>In many of the more relaxed civilizations on the Outer Eastern Rim of the Galaxy, the Hitch Hiker's  Guide  has  already  supplanted  the  great Encyclopedia Galactica as the standard repository  of  all  knowledge  and wisdom, for though it  has  many  omissions  and  contains  much  that  is apocryphal, or at least wildly inaccurate, it scores over the older,  more pedestrian work in two important respects.</p>"
				+ "<p>First, it is slightly cheaper; and secondly it has  the  words  Don't Panic inscribed in large friendly letters on its cover.</p>"
				+ "<p>But the story of this terrible, stupid Thursday,  the  story  of  its extraordinary consequences, and the story of how  these  consequences  are inextricably intertwined with this remarkable book begins very simply.</p>"
				+ "<p>It begins with a house.</p>",
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

		homePage.Blocks.Add(new HeadingBlock
		{
			Heading = new StringField() { Value = "Welcome!" },
			Alignment = new SelectField<HorizontalAlignment> { Value = HorizontalAlignment.Center }
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
