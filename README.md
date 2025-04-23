# Arkanis Overlay

**A Next-Generation Overlay Companion for Star Citizen**  
Created by **[FatalMerlin](https://discord.com/users/174617873182883841)**,
co-founder of the in-game organization **[Arkanis Corporation](https://join.arkanis.cc/)**,
and **[TheKronnY](https://discord.com/users/224580858432978944)
**, [an engineer and IT PhD student](https://i.redd.it/dkrdm5jdb8ce1.jpeg).

---

## 🚀 What is Arkanis Overlay?

**Arkanis Overlay** is a usability-focused, in-game companion overlay for **Star Citizen**.
It aims to provide players with direct access to essential tools and information without having to leave the game or
juggle external websites.
Inspired by the *[RatScanner](https://ratscanner.com/)* project from the Tarkov community, this project is built with a
strong focus on reliability, extensibility, and community collaboration.

The overlay is currently in **active development** and driven by a clear mission:
> **Minimize friction, maximize immersion.**

---

## ✨ Key User Features (MVP)

- 🔍 **In-Game Search Tool**  
  Find general and commonly-needed game information fast — no more alt-tabbing to search websites.

- 📦 **Integrated Community Data**  
  Seamlessly connected with [UEX](https://uexcorp.space) and other community tools to enrich in-game decision-making.

## 🤝 Community Focus

- 🧪 **Community-Driven Development**  
  Built in collaboration with players and devs who actively use and shape the tools.

## 🖥️ Tech Stack

- ⚙️ **WPF-Hosted Blazor App**  
  Built in C# using modern UI technologies for smooth performance and extensibility.

---

## 🔮 Roadmap & Planned Features

We're dreaming big — here's what's ahead:

### 🧩 Core Enhancements

- [ ] Self-updater and installer support (in progress)
- [ ] Improved reliability and usability
- [ ] Configurable display: Overlay layout, themes, behavior
- [ ] Global keyboard shortcuts for quick access

### 🔎 Smarter Search

- [ ] Full-text **fuzzy search** across item databases
- [ ] Improved result ranking and filtering

### 🔗 Integrations & Tools

- [ ] [UEX CLI](https://github.com/UEXCorp/UEX-CLI) and MFD screen integration
- [ ] Embedded tools and
  services ([SPViewer](https://www.spviewer.eu/), [Erkul](https://www.erkul.games/), [Regolith Co.](https://regolith.rocks/),
  etc.) — permissions required
- [ ] API-driven data enrichment from the community (Wiki, JSON exports, etc.)

### 🔍 OCR-Driven Intelligence

- [ ] Stage 1: UEX data extraction (prices for commodities, items, fuel, ores, etc.)
- [ ] Stage 2: [Regolith Co.](https://regolith.rocks/) integration
- [ ] Stage 3: Player location-based map awareness and contextual recommendations

### 🧭 Long-Term Vision

- Gameplay enhancement tools:
    - Task & progress tracking
    - Party, squad, and org management utilities
    - Note-taking & planning aids
- **Plugin support** for third-party extensions (exploration stage)

---

## 💡 Philosophy

Star Citizen is vast — and at times, frustratingly so.  
**Arkanis Overlay** doesn't aim to change the game, but to make the *experience better*.
It’s your always-on wingman — helpful, seamless, and never in the way.

Key principles:

- **Reliable UX**: The overlay *must* be stable. The game has enough bugs already.
- **Incremental growth**: Release early, improve often.
- **No exploits**: This project will *never* include cheats, automation, or game-breaking features.

---

## 🧑‍💻 Development Status

> Project Lead: [FatalMerlin](https://github.com/FatalMerlin)  
> Language: C#  
> Frameworks: **WPF**, **Blazor (WebView2)**

Currently developed in bursts due to time constraints (because, you know… real life).  
We are now a small dev team — progress is slow but deliberate. A Minimum Viable Product (MVP) is nearing readiness!

---

## 🧪 Unit Testing

To ensure the reliability and stability of the project, **unit tests** are implemented using the `xUnit` testing
framework.
Developers can run the tests locally using the following commands:

### Running All Tests

```bash
dotnet test
```

### Filtering Tests by Traits

You can filter test cases based on specific traits (e.g., types, categories) using the `--filter` CLI option.
For example, to run only tests that do not go against live external APIs (either do not need external APIs at all or use
locally cached data):

```bash
dotnet test --filter "DataState!=Live"
```

### Adding Traits to Tests

To categorize tests, use the `TraitAttribute` in your test methods.
Place trait names and their corresponding values in the `TestConstants` or other project-appropriate class.

```csharp
[Trait(nameof(TestConstants.Traits.DataSource), TestConstants.Traits.DataSource.ExternalApi)]
[Trait(nameof(TestConstants.Traits.DataState), TestConstants.Traits.DataState.Cached)]
public class CachedUexItemSyncRepositoryTest(ITestOutputHelper testOutputHelper, CachedUexSyncRepositoryTestFixture fixture)
{
    [Fact]
    public void TestMethod()
    {
        // Your test code here
    }
}
```

This allows for better organization and selective execution of tests during development.

---

## 🙌 Community & Support

Big shout-out to:

- **UEX Dev Team** for their continued support, testing, and future collaboration.
- Everyone in the **Arkanis Corporation** and broader SC community who has provided feedback and ideas.

Want to contribute, suggest features, or test?  
[Open an issue](https://github.com/ArkanisCorporation/ArkanisOverlay/issues/new/choose) or
contact [@FatalMerlin](https://discord.com/users/174617873182883841) — we’d love your input!

---

## 🧭 Why "Arkanis"?

The name pays tribute to the **Arkanis Sector**, a frontier region of bold explorers — just like the users of this
tool.  
We aim to embody the same spirit: discovery, utility, and frontier innovation.

---

## 🛠️ Getting Started (Coming Soon)

> Detailed build and deployment instructions will be added after the MVP is finalized. Stay tuned.

---

## 📜 License

*To be defined.*  
Expected to be a community-friendly open-source license (likely MIT or similar).

---

## 🌌 Final Words

This is only the beginning.  
**Arkanis Overlay** is designed to grow with the game and the community.  
Let’s make Star Citizen a little smoother — together.

> *“A ship is only as good as its crew.”*  
> — Unknown

---

