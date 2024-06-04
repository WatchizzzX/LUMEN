
![GPLv3 License](https://img.shields.io/badge/License-GPL%20v3-yellow.svg)
![GitHub last commit](https://img.shields.io/github/last-commit/l0nnes/LUMEN)
![Last release](https://img.shields.io/github/v/release/l0nnes/LUMEN)

![Logo](Assets/Art/UI/LumenLogo.png)

## Intro
This is an game in which you play as a robot and solve logic puzzles based on logical operators.

> [!WARNING]
> Currently, the project is still being developed and does not have a final form. If something does not work in the release, then it may be corrected and finalized in the following :)

## Tech Stack

- Unity 2023.2.0f1

## Features
- Interaction with all standard logical operators
- Platformer gameplay

## How to build
1. Download Unity of the version listed above
2. Also download the ILL2CPP compilation module
> [!NOTE]
> Please, download ILL2CPP module, according to your OS
3. Open the project in Unity
4. Find the Serialized object with Bootstrapper settings
> [!NOTE]
> It's located in Assets/Settings/Managers/BootstrapperSettings.asset by default
5. Select the main menu as the first boot level
6. Open the build settings
7. Add the scenes: Start, MainMenu, as well as the levels you need
8. Build a project
9. You are perfect!

> [!WARNING]
> There are no animations in this project, as they were purchased and cannot be distributed for free.
> When building the project, please note that you will need to add animations for the character yourself.
> Just open player animator in Assets/Art/AnimatorControllers/PlayerAnimator, and look at what animations are missing

> [!WARNING]
> I used payed asset Easy Perfomant Outline for selecting connected logical operators.
> If you don't have, just remove some lines of code from UIInteractableInfoHandler.cs, and remove scripts from main camera,
> logical operators and levers

> [!WARNING]
> SaveLoadSystem uses a password for protecting game data. File with password doesn't include in repo
> So, you need to add a SaveLoadConfig.cs file. And add a public const string with a password

## Process
I am creating this game as part of my thesis project. The theme I have selected is learning, as I am studying to become a programmer. The game will contribute to this area in a positive way.
Although the game is intended for people with no prior experience and who wish to expand their horizons in the area of programming and solving logical problems, it may be more suitable for those with less programming knowledge. Initially, the game was planned to be in first-person view, but the design has since shifted to isometric.
During the brainstorming process, we added features such as interacting with objects in order to solve puzzles based on physical principles (similar to Portal), as well as the ability to program small drones to perform various tasks using Lua, allowing even those without prior programming experience to complete the game.

## Authors

- [@l0nnes (Zahar Czembrowicz)](https://www.github.com/l0nnes)

## Usages

- [Tiny Character Controller](https://github.com/unity3d-jp/Project_TCC)
- [DOTween](https://dotween.demigiant.com/)
- [David-F Dev Console](https://github.com/DavidF-Dev/Unity-DeveloperConsole)
- [Dreamteck Splines](https://assetstore.unity.com/packages/tools/utilities/dreamteck-splines-61926)
- [Easy Transitions](https://assetstore.unity.com/packages/tools/gui/easy-transitions-225607)
- [Fast Script Reload](https://assetstore.unity.com/packages/tools/utilities/fast-script-reload-239351)
- [Jammo Character](https://assetstore.unity.com/packages/3d/characters/jammo-character-mix-and-jam-158456)
- [NaughtyAttributes](https://assetstore.unity.com/packages/tools/utilities/naughtyattributes-129996)
- [QuickSave](https://assetstore.unity.com/packages/tools/utilities/quick-save-107676)
- [Runtime Monitoring](https://assetstore.unity.com/packages/tools/integration/runtime-monitoring-222328)
- [Toolbar Extended](https://github.com/marijnz/unity-toolbar-extender.git)
- [Unity Dropdown](https://github.com/SolidAlloy/UnityDropdown)

## License

[GPL 3.0](https://choosealicense.com/licenses/gpl-3.0/)

