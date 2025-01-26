using Microsoft.Playwright;

namespace CarvedRock.End2End.Tests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class Tests : PageTest
    {
        [Test]
        public async Task HomePageHasCorrectContent()
        {
            await Page.GotoAsync("https://localhost:7224");
            await Page.ScreenshotAsync(new() { Path = "screenshot.png" });

            // Expect a title "to contain" a substring.
            await Expect(Page).ToHaveTitleAsync("Carved Rock Fitness");

            await Expect(Page.GetByText("GET A GRIP")).ToBeVisibleAsync();
        }

        [Test]
        public async Task CanAddItemsToCartOnFootwearPage()
        {
            await Page.GotoAsync("https://localhost:7224");
            await Page.GetByRole(AriaRole.Link, new() { Name = "Footwear" }).ClickAsync();
            await Expect(Page.GetByRole(AriaRole.Img, new() { Name = "Trailblazer" })).ToBeVisibleAsync();
            await Expect(Page.Locator("#add-btn-1")).ToBeVisibleAsync();
            await Page.Locator("#add-btn-1").ClickAsync();
            await Expect(Page.Locator("#carvedrockcart")).ToContainTextAsync("Cart (1)");
        }
    }
}
