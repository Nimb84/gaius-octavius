<div id="top"></div>

<h3>[BE CAREFUL]</h3>
<p>This application is in an early stage of development, use it at your own risk!</p>
<br />
<br />
<br />

<!-- PROJECT SHIELDS -->
[![LinkedIn][linkedin-shield]][linkedin-url]
[![MIT License][license-shield]][license-url]

<!-- PROJECT LOGO -->
<br />
<div align="center">
  <!-- <a href="https://github.com/Nimb84/GaiusOctavius">
    <img src="http://example.com/" alt="Logo" width="80" height="80">
  </a> -->

  <h3 align="center">Gaius Octavius Bot</h3>

  <p align="center">
    Just a multitasking bot
    <br />
    <br />
    <a href="https://t.me/GaiusOctaviusBot">Try</a>
    ·
    <a href="https://github.com/Nimb84/GaiusOctavius/issues">Report Bug</a>
    ·
    <a href="https://github.com/Nimb84/GaiusOctavius/issues">Request Feature</a>
  </p>
</div>

<!-- ABOUT THE PROJECT -->
## About The Project

Gaius Octavius is a bot serves me for solving my everyday problems I do not want to solve with third-party applications. If you want to try you are welcome!

Features list:
* Movies tracker

<p align="right">(<a href="#top">back to top</a>)</p>


### Built With

* [TelegramBot](https://github.com/TelegramBots/telegram.bot)
* [.NET 7.0](https://dotnet.microsoft.com/download/dotnet)
* [MediatR](https://github.com/jbogard/MediatR)
* [FluentValidation](https://fluentvalidation.net)
* [EF Core](https://learn.microsoft.com/en-us/ef/core)
* [MsSql](https://www.microsoft.com/en-us/sql-server)
* [Hangfire](https://www.hangfire.io)
* [Moq](https://github.com/moq/moq4)
* [IMDbApiLib](https://imdb-api.com)

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Getting Started

Use [ngrok](https://ngrok.com/) to use secure URL to your localhost server.


### Installation

1. Register a bot at [Telegram BotFather](https://telegram.me/BotFather)
2. Clone the repo
   ```sh
   git clone git@github.com:Nimb84/GaiusOctavius.git
   ```
3. Apply EF migrations
   ```sh
   update-database -Context UserDbContext
   update-database -Context MovieDbContext
   ```
4. Enter your API and Bot token in `appsettings.Development.json`
```json
{
  "Logging": {
    "TelegramBot": {
      "ChatId": "'ENTER TELEGRAM GROUP CHAT ID'"
    }
  },
  "AppConfiguration": {
    "Domain": "'ENTER NGROK HTTPS URL'"
  },
  "TelegramBotConfiguration": {
    "Token": "'ENTER TELEGRAM BOT TOKEN'",
    "AdminChatId": "'ENTER TELEGRAM GROUP CHAT ID'"
  }
}
```

<p align="right">(<a href="#top">back to top</a>)</p>


<!-- ROADMAP -->
## Roadmap

- [x] Setup project structure
  - [x] Base architecture
  - [x] Log errors into Telegram Log Chat
- [ ] Add Movies tracker
  - [x] Add search (IMDbApi)
  - [x] Add Watch Later/Watched
  - [ ] Add get lists
- [ ] Add time tracker support 
- [ ] Add Secret manager
- [ ] Add Viber Support
- [ ] Multi-language Support
  - [ ] Ua
  - [ ] En

See the [open issues](https://github.com/Nimb84/GaiusOctavius/issues) for a full list of proposed features (and known issues).

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- LICENSE -->
## License

Distributed under the [MIT License](https://choosealicense.com/licenses/mit/).

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- CONTACT -->
## Contact

Dmytro Chernov - [Linkedin](https://www.linkedin.com/in/dmytro-chernov-084) - chernov.dmtr@gmail.com

Project Link: [Git](https://github.com/Nimb84/GaiusOctavius)

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- ACKNOWLEDGMENTS -->
## Acknowledgments

* [Best README Template](https://github.com/othneildrew/Best-README-Template)

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- MARKDOWN LINKS & IMAGES -->
[license-shield]: https://img.shields.io/badge/license-MIT-green?style=for-the-badge
[license-url]: https://choosealicense.com/licenses/mit/
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/dmytro-chernov-084
