<h1 align="center">
<a href="https://arkanis.cc/overlay" target="_blank">Arkanis Overlay</a>
for Star Citizen
</h1>

<p align="center">
<img alt="GitHub dev branch commits since latest release" src="https://img.shields.io/github/commits-since/ArkanisCorporation/ArkanisOverlay/latest/dev?logo=github" />
<img alt="GitHub dev branch check runs" src="https://img.shields.io/github/check-runs/ArkanisCorporation/ArkanisOverlay/dev?logo=github" />
<a href="https://github.com/ArkanisCorporation/ArkanisOverlay/discussions" target="_blank">
 <img alt="GitHub Support" src="https://img.shields.io/badge/github-support?logo=github&label=support" />
</a>
<a href="https://arkanis.cc" target="_blank">
 <img alt="Arkanis Website" src="https://img.shields.io/badge/Arkanis.cc-website?logo=googlechrome&logoColor=white&label=website" />
</a>
<a href="https://join.arkanis.cc" target="_blank">
 <img alt="Arkanis Discord" src="https://img.shields.io/discord/1294685596991750277?logo=discord&logoColor=white" />
</a>
</p>

<h3 align="center">
A Next-Generation Overlay Companion for Star Citizen<br>
</h3>

<h6 align="center">
Created by <a href="https://discord.com/users/174617873182883841" target="_blank"><b>FatalMerlin</b></a>,
co-founder of the <a href="https://org.arkanis.cc" target="_blank">in-game organization</a> <a href="https://arkanis.cc" target="_blank"><b>Arkanis Corporation</b></a>,
<br/>and <a href="https://discord.com/users/224580858432978944" target="_blank"><b>TheKronnY</b></a>, <a href="https://i.redd.it/dkrdm5jdb8ce1.jpeg" target="_blank">an engineer and IT PhD student</a>.
</h6>

https://github.com/user-attachments/assets/a578b6bc-b815-47f9-8f02-c8a54d4125ef

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

- **In-Game Search Tool**
  — Find general and commonly-needed game information fast — no more alt-tabbing to search websites.

- **Integrated Community Data**
  — Seamlessly connected with [UEX](https://uexcorp.space) and other community tools to enrich in-game decision-making.

- **Automatic Startup**
  — Launches automatically when you start Windows and Star Citizen, so you’re always ready to go.

## 🤝 Community Focus

- 🧪 **Community-Driven Development**
  — Built in collaboration with players and devs who actively use and shape the tools.

## 🖥️ Tech Stack

- ⚙️ **WPF-Hosted Blazor App**
  — Built in C# using modern UI technologies for smooth performance and simple extensibility.

---

## 🔮 Roadmap & Planned Features

We're dreaming big — here's what's ahead:

### 🧩 Core Enhancements

- [ ] Self-updater and installer support (in progress)
- [ ] Improved reliability and usability
- [ ] Configurable display: Overlay layout, themes, behavior
- [x] Global keyboard shortcuts for quick access and navigation

### 🔎 Smarter Search

- [x] Full-text **fuzzy search** across item databases
- [ ] Improved result sorting and filtering

### 🔗 Integrations & Tools

- [x] Game entity search sourced from [UEX Corporation](https://uexcorp.space/)
- [ ] [UEX CLI](https://github.com/UEXCorp/UEX-CLI) and MFD screen integration
- [ ] Embedded tools and services — permissions required
    - [ ] [SPViewer](https://www.spviewer.eu/)
    - [ ] [Erkul](https://www.erkul.games/)
    - [ ] [Regolith Co.](https://regolith.rocks/)
    - etc.
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

> Project Lead: [FatalMerlin](https://github.com/FatalMerlin)<br>
> Language: C#<br>
> Frameworks: **WPF**, **Blazor (WebView2)**

Currently developed in bursts due to time constraints (because, you know… real life).
We are now a small dev team — progress is slow but deliberate. A Minimum Viable Product (MVP) is nearing readiness!

---

## 🙌 Community & Support

Big shout-out to:

- **UEX Dev Team** for their continued support, testing, and future collaboration.
- Everyone in the **Arkanis Corporation** and broader SC community who has provided feedback and ideas.

Want to contribute, suggest features, or participate in app testing?
[Open an issue](https://github.com/ArkanisCorporation/ArkanisOverlay/issues/new/choose) or
contact [@FatalMerlin](https://discord.com/users/174617873182883841) — we’d love your input!

---

## 🧭 Why "Arkanis"?

The name pays tribute to the **Arkanis Sector**, a frontier region of bold explorers
— just like the developers and the users of this tool.
We aim to embody the same spirit: discovery, utility, and frontier innovation.

---

## 🛠️ Getting Started (Coming Soon)

> [!IMPORTANT]
> Detailed build and deployment instructions will be added after the MVP is finalized. Stay tuned.

### Application Behavior

> [!TIP]
> It does not matter whether the game was started before or after the Arkanis Overlay application.

Once launched, the application runs in the **Windows system tray**.
It does not display a window by default.
From the tray icon, users can open the **preferences dialog** or exit the application entirely.

### Overlay Activation

> [!IMPORTANT]
> The in-game overlay can be opened using the default keyboard shortcut: `Left Alt + Left Shift + S`.

The shortcut can be customized through the **preferences dialog**, allowing you to set a key combination that best suits
your needs.

### Preferences and Configuration

The preferences dialog is accessible either from the **system tray icon** or directly within the **overlay interface**
itself.

Within the Preferences, you can:

- Enable or disable automatic launching of the application when Windows starts.
- Choose whether the overlay application should automatically exit when Star Citizen is closed.
- Customize the keyboard shortcut used to activate the overlay.
- Adjust other application-specific settings related to appearance and behavior.

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

