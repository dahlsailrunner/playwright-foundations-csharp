using Microsoft.Playwright;

namespace CarvedRock.End2End.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class AdminUnauthorizedTests : PageTest
{
    [Test]
    public async Task AdminIsGuardedAndRedirectsAnonymousUsersToLogin()
    {
        await Page.GotoAsync("https://localhost:7224/admin");        
        await Expect(Page).ToHaveURLAsync(new Regex ("https://demo.duendesoftware.com/Account/Login.*"));
    }

    [Test]
    public async Task AdminIsNotAvailableForAlice()
    {
        await Page.GotoAsync("https://localhost:7224/");
        await Page.GetByRole(AriaRole.Link, new() { Name = "Sign in" }).ClickAsync();
        await Page.GetByPlaceholder("Username").FillAsync("alice");
        await Page.GetByPlaceholder("Username").PressAsync("Tab");
        await Page.GetByPlaceholder("Password").FillAsync("alice");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();

        // ToBeHidden will succeed if the element exists but is not visible OR if it doesn't exist at all
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Admin" })).ToBeHiddenAsync();

        // hand-coded the next two lines
        await Page.GotoAsync("https://localhost:7224/admin");
        await Expect(Page).ToHaveURLAsync("https://localhost:7224/AccessDenied?ReturnUrl=%2Fadmin");

        // record a single line if you want!
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Access Denied" })).ToBeVisibleAsync();
    }
}