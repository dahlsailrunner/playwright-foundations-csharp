# Playwright End-to-End Tests

This solution / project is a collection of end-to-end tests
using the [Playwright](https://playwright.dev/) framework and
the NUnit testing framework.

## Getting Started

To run these tests, the Aspire-based solution in
the root folder of this repository should be running.
Follow the instructions in [that readme](../readme.md).

You may also need to install the browsers needed
by Playwright.  

First build the solution.

Then run the following command from a terminal:

```bash
./CarvedRock.End2End.Tests/bin/Debug/net9.0/playwright.ps1 install
```

Then you should be able to run the tests!

## Using the Recorder

An easy way to create tests is using the Playwright recorder:

```bash
./CarvedRock.End2End.Tests/bin/Debug/net9.0/playwright.ps1 codegen https://localhost:7224
```
You can also install the [Plawright CRX](https://chromewebstore.google.com/detail/playwright-crx/jambeljnbnfbkcpnoiaedcabbgmnnlcd) extension from
the Chrome Web Store to have it "always available"
without needing to run the above command.
