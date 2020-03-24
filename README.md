# Discord-Bot

This is the Discord Bot we used for our Hypixel Store.

Dependencies: .NET Core SDK, Discord.Net, DSharpPlus

# Installation Process: #

1. You need to install [.NET Core SDK](https://dotnet.microsoft.com/download)
  
2. Under Solution Explorer, Right click Dependencies then Manage NuGet Packages.
   - After that, make sure the Package sources is nuget.org (Package Source is located at the top right of the tab)
   - Search for Discord.Net and install it
    
3. Next click the Settings option right beside the Package sources
   - A window should appear, then click the + at the top right of the window (It should be Green in colour)
   - Add https://nuget.emzi0767.com/api/v3/index.json to the Source which you added in the above instruction ^
   - You can now close out of this window.
    
4. Change the Package sources to the one you've just added
   - Check Include prelease beside the Search Bar
   - After that Search for DSharpPlus, DSharpPlus.CommandsNext and DSharpPlus.Interactivity.
   - Install all 3 of these.
    
 NOTE: DO NOT INSTALL ANY OF THE DSharpPlus VERSIONS FROM NuGet Packages! SOME FUNCTIONS WILL NOT WORK!
