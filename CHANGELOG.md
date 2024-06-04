# [0.22.0](https://github.com/l0nnes/LUMEN/compare/v0.21.0...v0.22.0) (2024-06-04)


### Bug Fixes

* Fix extra jump from the same wall ([9312beb](https://github.com/l0nnes/LUMEN/commit/9312beb45914a7d86dfda0407303774e88fbd322))


### Features

* Add GameManager support for OnFinish signal ([4248e17](https://github.com/l0nnes/LUMEN/commit/4248e175e5f44aeb558f454eba76049db809fa59))
* Add GetCurrentSceneID and GetNextSceneID for SceneManager ([712a88b](https://github.com/l0nnes/LUMEN/commit/712a88b77134e36adac4793e6494ee740ae495b7))
* Add LevelSettings for storing records ([64cc020](https://github.com/l0nnes/LUMEN/commit/64cc020868cef607a767a06863482d06acad2914))
* Add new signal OnFinish ([d9ea118](https://github.com/l0nnes/LUMEN/commit/d9ea1185af903ae12d6f74f28050218ee6652f28))
* Add Reload option in pause menu ([42dbe94](https://github.com/l0nnes/LUMEN/commit/42dbe9408b0793b93829365e6b4c09ba69e47580))
* Add SaveLoadSystem ([7d3d4d7](https://github.com/l0nnes/LUMEN/commit/7d3d4d71cd8f5aa77f8b084d3990f40688aeec98))
* Add stopwatch ([dfabf41](https://github.com/l0nnes/LUMEN/commit/dfabf4137c6e012b801d2990393ca38b2402e762))
* Add support for saving progress ([b9eb606](https://github.com/l0nnes/LUMEN/commit/b9eb6068a2f1fea9341ea15b4fe240cce6b48539))
* Now OnFinish signal contains data ([fd93721](https://github.com/l0nnes/LUMEN/commit/fd93721409689bae4f32ff19b4a0f1231b216180))

# [0.21.0](https://github.com/l0nnes/LUMEN/compare/v0.20.0...v0.21.0) (2024-06-03)


### Bug Fixes

* Now camera can look more up ([8b11b15](https://github.com/l0nnes/LUMEN/commit/8b11b1591bea005dda16b82a290936a62a01031f))


### Features

* Add event when interactable found ([005a727](https://github.com/l0nnes/LUMEN/commit/005a7273bdc5f7bd08415ca499b5106b15be0977))
* Add info when player close to logical operator ([d56d877](https://github.com/l0nnes/LUMEN/commit/d56d8771fda66fedf6ecb550ee210028ad2a16a1))
* Add new interactable type ([baecd78](https://github.com/l0nnes/LUMEN/commit/baecd78f3793de71ae388a64842dc6d24f0f04a6))
* Add outline effect by EPO ([c3ce2e8](https://github.com/l0nnes/LUMEN/commit/c3ce2e803e558fb84d238086f68b9502f59baac7))
* Added displaying key when interacting ([6f0b893](https://github.com/l0nnes/LUMEN/commit/6f0b89312c36f8e96b5d908aa975184faa20d9ad))

# [0.20.0](https://github.com/l0nnes/LUMEN/compare/v0.19.0...v0.20.0) (2024-06-03)


### Bug Fixes

* Add permanent enabled dev-console in editor and dev build ([9403981](https://github.com/l0nnes/LUMEN/commit/9403981acf2167bb0cc60f42b29446702bd8cdab))
* Fix bug when player respawn with non-zero velocity ([48fcce0](https://github.com/l0nnes/LUMEN/commit/48fcce09ec52b5da9eda4ee06341e94f7fb7b514))
* Fix reset velocity on jump ([bd8cf0a](https://github.com/l0nnes/LUMEN/commit/bd8cf0adb7dd72fec7301f6032c24181543025fd))
* Fix RuntimeMonitoring draw ([1629b2d](https://github.com/l0nnes/LUMEN/commit/1629b2d01a5ff0faece029bf46f58f04ad471c1b))
* Fix sliding in PlayerController ([41366f2](https://github.com/l0nnes/LUMEN/commit/41366f2a176ed0aa515fca42bde6706bb45ec45c))
* Fix when input doesn't block when console open in devmode ([b96608f](https://github.com/l0nnes/LUMEN/commit/b96608f7217bbff9d2e045a183d54c3df81f7fd4))
* Increase player speed ([ffd282c](https://github.com/l0nnes/LUMEN/commit/ffd282c779c252a4d18ed15578825f4e68ff833b))
* Rework VectorExtensions ([c649964](https://github.com/l0nnes/LUMEN/commit/c6499646ab27966ff2a59510013a8e54b0ce77e9))
* Rework wall jumping ([be989f4](https://github.com/l0nnes/LUMEN/commit/be989f4b89bb33ac44c9101d353b8f99003b3744))
* Separate functionality in RaycastHelpers ([723eab6](https://github.com/l0nnes/LUMEN/commit/723eab673b5d8ef1d74c39e6d57d5e9288f74b0c))
* Some fixes in runtime monitoring ([5edf1da](https://github.com/l0nnes/LUMEN/commit/5edf1dac3dd1b3a49ddf27e8969599557475e00c))


### Features

* Add a little jump in end of sliding surface ([35b208d](https://github.com/l0nnes/LUMEN/commit/35b208d058e63ab06b1bf1a5c9a6b49b8c86d0d5))
* Add Build Version package ([45d7201](https://github.com/l0nnes/LUMEN/commit/45d7201f6a599cf9179208b97e3958e88ac7bc50))
* Add dev command Respawn ([5ab7fea](https://github.com/l0nnes/LUMEN/commit/5ab7fead2e1e12b35883c28cda3175f37265877d))
* Add devmode for debugging ([142e45f](https://github.com/l0nnes/LUMEN/commit/142e45fc5c9eb0078c4836933491f5989c537f85))
* Add jump animation on end of sliding ([22198f0](https://github.com/l0nnes/LUMEN/commit/22198f02af9ed4f9ef3b01a8ad941ecb358ef57e))
* Add new LineRenderer animator ([0df9ca7](https://github.com/l0nnes/LUMEN/commit/0df9ca790548fc5b260a42282e28adb3688daa57))
* Add propety of connected GO in LayerCheck ([e1b0224](https://github.com/l0nnes/LUMEN/commit/e1b0224dddd3189ef0225a5a440f1584330825b3))
* Add RaycastHelpers ([05fa4cd](https://github.com/l0nnes/LUMEN/commit/05fa4cd5abae25d164d8a99b78982ec38fc7102c))
* Add sliding setup feature ([8c3a522](https://github.com/l0nnes/LUMEN/commit/8c3a52261d86dfe2b0254f84f37e08babbb481fb))
* Add SlidingSurface for setup sliding ([1b5a8ec](https://github.com/l0nnes/LUMEN/commit/1b5a8ec96a30203bde136d92eb95838b0c578729))
* Add walljump setup feature ([c20be9b](https://github.com/l0nnes/LUMEN/commit/c20be9be1f05e52f5918adac2027cfdb6e720fcb))
* Add WallJumpSurface for setup WallJump ([efb90c9](https://github.com/l0nnes/LUMEN/commit/efb90c9dc9a6b0280f52e4fb3be1dfff52edcb8e))
* Now player can't wall jump up on only one wall ([0d91b19](https://github.com/l0nnes/LUMEN/commit/0d91b19fb66b7d3b0b58043efb0eac96d866f88a))
* Rework Player Controller. Added jump, double jump, wall hang, sliding ([3ebf9b8](https://github.com/l0nnes/LUMEN/commit/3ebf9b8283ab4913582c81fa1ad90f97cdee7630))
* Rework PlayerController for TCC ([26967f1](https://github.com/l0nnes/LUMEN/commit/26967f1b4453c1deb8011553a92fe05a648827ef))

# [0.19.0](https://github.com/l0nnes/LUMEN/compare/v0.18.0...v0.19.0) (2024-05-16)


### Bug Fixes

* Fix strange walking on stairs ([1bc0f56](https://github.com/l0nnes/LUMEN/commit/1bc0f5690ec6ffb4bc42fbaedb9552d950940561))
* Fix UI background translucency ([31e9c52](https://github.com/l0nnes/LUMEN/commit/31e9c523849c7ed281eb8ecdad69533adca17de0))


### Features

* Add custom editor script for BootstrapperSettings ([d1919db](https://github.com/l0nnes/LUMEN/commit/d1919db1731f1b2cdfb916b8cd0720ebcfd78804))
* Add range in InteractionController when it always work ([f6baf9f](https://github.com/l0nnes/LUMEN/commit/f6baf9fce0b87930abf4b145bce8c8f19ca72a6b))
* Add settings to Bootstrapper ([e85af08](https://github.com/l0nnes/LUMEN/commit/e85af089b36d062e2d407093b76b2504556e2266))

# [0.18.0](https://github.com/l0nnes/LUMEN/compare/v0.17.0...v0.18.0) (2024-05-05)


### Bug Fixes

* Fix coyote jump height ([ea10572](https://github.com/l0nnes/LUMEN/commit/ea10572a4b23538e2635ccd18cad6de87e68e0fd))
* Update SignalGenerator for new signals ([a5a20a4](https://github.com/l0nnes/LUMEN/commit/a5a20a4ac76952a60eac4aafef600592b01429d1))


### Features

* Add CameraManager handler for change level camera ([ada8257](https://github.com/l0nnes/LUMEN/commit/ada825761b84784d7279dd2eb8a5413e640eb6f2))
* Add new signal for change level camera ([3a26e6c](https://github.com/l0nnes/LUMEN/commit/3a26e6cdf573702236176d71782ee6c241033dfe))
* Add player jump from wall ([b7b2d99](https://github.com/l0nnes/LUMEN/commit/b7b2d990d96d6198c363b1449c724bf3aba198cd))

# [0.17.0](https://github.com/l0nnes/LUMEN/compare/v0.16.1...v0.17.0) (2024-05-04)


### Bug Fixes

* Add reset player velocity on finish trigger ([86dfa3d](https://github.com/l0nnes/LUMEN/commit/86dfa3d88548e518e441d909080abb3b338079c5))
* Fix bug when called ExitCutscene signal ([ff608b1](https://github.com/l0nnes/LUMEN/commit/ff608b1af3a0c4ba2df716f2b42799aac0e4d26e))
* Fix respawn in spawnpoint ([dadd6aa](https://github.com/l0nnes/LUMEN/commit/dadd6aae9b3b92207bdbd49f54ddc76ad6da55eb))


### Features

* Add new position animator ([88f2663](https://github.com/l0nnes/LUMEN/commit/88f2663f60aa1e16225bdccc90511ec0b53f2635))
* Rework exit camera work ([d9053c9](https://github.com/l0nnes/LUMEN/commit/d9053c9ba1a35a6a08ff7c5b4e79b25c3634a1f6))

## [0.16.1](https://github.com/l0nnes/LUMEN/compare/v0.16.0...v0.16.1) (2024-05-03)


### Bug Fixes

* Fix bug with main menu and devconsole ([a0ed022](https://github.com/l0nnes/LUMEN/commit/a0ed0222408d75ccb96c6dc88e194b3011122664))

# [0.16.0](https://github.com/l0nnes/LUMEN/compare/v0.15.0...v0.16.0) (2024-05-02)


### Bug Fixes

* Add cooldown to interactable button ([968335c](https://github.com/l0nnes/LUMEN/commit/968335c2001996f70a3110291986846c88bbed3e))
* Another fix for player animator) ([0b29476](https://github.com/l0nnes/LUMEN/commit/0b29476e8defa51abd95e5c5d3f5a29106c9e3bb))
* FIx activating object when player turned away ([6570e36](https://github.com/l0nnes/LUMEN/commit/6570e36fadb715bd67d1a3be555556040e6453ed))
* Fix bug when you can pause when transition started ([24436df](https://github.com/l0nnes/LUMEN/commit/24436dfdfb24063953d5a4c18956890461bc31b2))
* Fix camera damping when avoid obstacles ([12716ff](https://github.com/l0nnes/LUMEN/commit/12716ffd0128be7ee3ba93ceac0830976da0bfad))
* Fix flickering player animation ([c56d467](https://github.com/l0nnes/LUMEN/commit/c56d4670c974fd68bc134868d6cd571a74f96240))
* Fix player animator ([730dd3e](https://github.com/l0nnes/LUMEN/commit/730dd3e2e467895f6044558a237c621103194b1a))
* InputHandler now respond to cursor visibility ([d9803c3](https://github.com/l0nnes/LUMEN/commit/d9803c3ee074e06333158cc1d457c328e9886a9d))


### Features

* Add CheatCodeHandler and first cheat) ([c17a182](https://github.com/l0nnes/LUMEN/commit/c17a182a936b10e564a9bb125f8fb89eb3f25707))
* Add SpawnPoint ([c07a91f](https://github.com/l0nnes/LUMEN/commit/c07a91f3265ced367f6241b640457f031e4f0db4))
* Rework camera from fixed to orbit ([f6a4e90](https://github.com/l0nnes/LUMEN/commit/f6a4e906c768cbbd4ffe4090360d41a9288e6d96))

# [0.15.0](https://github.com/l0nnes/LUMEN/compare/v0.14.0...v0.15.0) (2024-04-30)


### Features

* Rework of EventBus ([0a9cdfe](https://github.com/l0nnes/LUMEN/commit/0a9cdfe00a040013670e2f52710046b0e0c1e41c))

# [0.14.0](https://github.com/WatchizzzX/LUMEN/compare/v0.13.0...v0.14.0) (2024-04-18)


### Bug Fixes

* Add SceneManager settings to ignore some levels to spawn player ([226b191](https://github.com/WatchizzzX/LUMEN/commit/226b191d9f0e09a5e68b479fc804d47961d927ba))
* Rename OnStartExitCutscene to OnExitCutsceneSignal ([53a0035](https://github.com/WatchizzzX/LUMEN/commit/53a0035d4709e18fdacae0f709b7cc6d4bdb505c))
* Separate enums to individual assembly definition ([46d503f](https://github.com/WatchizzzX/LUMEN/commit/46d503fdb28803c8eedb60d9d15fe7fb9f2a06f4))
* Separate scene utils from SceneManager ([bc6c1f5](https://github.com/WatchizzzX/LUMEN/commit/bc6c1f508d7c5103e364c752037641cd393090eb))
* Update DefaultSceneLoader (Editor script) ([bed4d7f](https://github.com/WatchizzzX/LUMEN/commit/bed4d7fd18d0200316bf7498ebde2b5a130825c9))
* Update logger ([1cbe047](https://github.com/WatchizzzX/LUMEN/commit/1cbe047c8d704da5586cf3ef7ab4f7e77746e3ac))


### Features

* Add camera blending settings ([e236cc7](https://github.com/WatchizzzX/LUMEN/commit/e236cc7d8f50ea0f6995f3ce360015791e79a68e))
* Add loader animations ([cd17a68](https://github.com/WatchizzzX/LUMEN/commit/cd17a6892ed458d295d11872e8e40118c339b2ea))
* Add main menu ([fe0021c](https://github.com/WatchizzzX/LUMEN/commit/fe0021c8a538e67ffd9e353a6d878d214ee1bd94))
* Add pause ability ([4d161da](https://github.com/WatchizzzX/LUMEN/commit/4d161da9255fbcd2d2a114706b9edad190cd3a0b))
* Add UI scripts to interact with game core ([b43322a](https://github.com/WatchizzzX/LUMEN/commit/b43322a1fc4a8fb1a5cb379090e6257780d3b0d7))
* Bootstraper can override default transition ([a47587c](https://github.com/WatchizzzX/LUMEN/commit/a47587c7d147cb8d5dc71ff35f55bcc42fed4ed7))

# [0.13.0](https://github.com/WatchizzzX/LUMEN/compare/v0.12.0...v0.13.0) (2024-04-15)


### Bug Fixes

* Add clamp in PlayerAnimator ([c9ab206](https://github.com/WatchizzzX/LUMEN/commit/c9ab206e5bdcf9b6466cf0db3b165cbf846f0c31))
* Fix bootstraper to new animation system ([5c0fe39](https://github.com/WatchizzzX/LUMEN/commit/5c0fe39b1a1e5603a61acd7dc282438b3dba5ea2))
* Fix eventbus call with priority ([f923ce3](https://github.com/WatchizzzX/LUMEN/commit/f923ce3d102180d5fa2b858cc0fd61c355e8760b))
* Fix flickering ([199981c](https://github.com/WatchizzzX/LUMEN/commit/199981cc3a66b4f65a05e39c7fcd217da5729fff))
* Fix levers on Level 1 ([143b1d0](https://github.com/WatchizzzX/LUMEN/commit/143b1d034e362cad60ab74f339486b8b2918104d))
* Fix logical component for new animation system ([f0d9a28](https://github.com/WatchizzzX/LUMEN/commit/f0d9a28b54421f643917d22508f6611bba73b3f7))
* Fix respawn trigger to new respawn signal ([f2540e6](https://github.com/WatchizzzX/LUMEN/commit/f2540e62b3948b1a0aeae8652dc51367b9d7352b))
* Fix some animations ([bd55933](https://github.com/WatchizzzX/LUMEN/commit/bd55933bb5e2bc10a12f15796c4b67c83bf420ce))
* Rework cable controller for new animation system ([1f19c2e](https://github.com/WatchizzzX/LUMEN/commit/1f19c2e451d1f92d3b1d8baab60d3e5a7789fcd7))


### Features

* Add delay before respawn player ([daaddb0](https://github.com/WatchizzzX/LUMEN/commit/daaddb0658c43d23a03c2758b58c15e41c84aa60))
* Add graphic changer script ([23d2abf](https://github.com/WatchizzzX/LUMEN/commit/23d2abfe61f7c28d5005dbde011d25d9004153f0))
* Add new animations system ([83da669](https://github.com/WatchizzzX/LUMEN/commit/83da6690242d606bf50e87231c1ca0dfd4a31e28))
* Add otion for logger to display only errors ([b53768b](https://github.com/WatchizzzX/LUMEN/commit/b53768b7c346f28254be6497da5eb62a8a68de55))
* Add some dev commands ([26ac535](https://github.com/WatchizzzX/LUMEN/commit/26ac53552015f5a3cf4ab62449485d4f14c8328b))
* Add TryGet method for ServiceLocator ([c3e4e4e](https://github.com/WatchizzzX/LUMEN/commit/c3e4e4e1da2cf19c57c096c41b3463a43f895f22))

# [0.12.0](https://github.com/WatchizzzX/LUMEN/compare/v0.11.0...v0.12.0) (2024-03-12)


### Bug Fixes

* Add check scene in SceneManager ([13ce4d2](https://github.com/WatchizzzX/LUMEN/commit/13ce4d235c480e24dbd9a9339be9b4d69156ca96))
* Add spawnCameraRotation to SpawnManager ([31741e7](https://github.com/WatchizzzX/LUMEN/commit/31741e701df47fac79317cff9a20639be3c121a1))
* FinishLevel trigger now take duration form gamemanager settings ([9f6ef08](https://github.com/WatchizzzX/LUMEN/commit/9f6ef08753ec3866dcdb139d3acdc8967abd39fc))
* Fix camera movement on spawn ([d3edf57](https://github.com/WatchizzzX/LUMEN/commit/d3edf571e2060a5e29f3dafbe07f7f7b13642f3e))
* Make shorter animation from moving to falling ([76c4552](https://github.com/WatchizzzX/LUMEN/commit/76c4552ed9bbfb39e333334a3c1503c647adcaa8))
* Now CableController work with emission ([14556e5](https://github.com/WatchizzzX/LUMEN/commit/14556e59b12d3dd47f2ea3d34109df46cb43d94a))
* Some fixes in managers ([98e68c6](https://github.com/WatchizzzX/LUMEN/commit/98e68c6dff7f352db72f103cf07477ee0b42dba9))
* Update bootstrapper to current managers ([6b6c8dc](https://github.com/WatchizzzX/LUMEN/commit/6b6c8dc6b9a3536d10a41601c7a195f8cd1b39a1))
* Update CameraManager to new exit cameras ([7703f8f](https://github.com/WatchizzzX/LUMEN/commit/7703f8f31005c89fdbee858daefb6cbd2af963e8))
* Update input handler to SpawnPlayer signal ([bdd9b60](https://github.com/WatchizzzX/LUMEN/commit/bdd9b603dfb4936c843fdf8852d7b060373b65ad))
* Update TransitionManager to new signal and respawn event ([70aa6fa](https://github.com/WatchizzzX/LUMEN/commit/70aa6fac01dc8d3ee0f63a033c7c21dc44538636))


### Features

* Add changing scene bool to transition signal ([ce78f9d](https://github.com/WatchizzzX/LUMEN/commit/ce78f9d1669d20809ceab90ceb5870f14e4c3f7c))
* Add custom exit time in FinishLevelTrigger ([898cce1](https://github.com/WatchizzzX/LUMEN/commit/898cce152e42cff1e2983f4464e184ee72580e25))
* Add EmmisionAnimator ([3f65795](https://github.com/WatchizzzX/LUMEN/commit/3f65795c80f543af1324060d5fc592d7b043e690))
* Add event for pickable objects ([815e500](https://github.com/WatchizzzX/LUMEN/commit/815e50097acf353371a9e8f0a44010868c0ba954))
* Add OnRespawnPlayer signal ([35c2857](https://github.com/WatchizzzX/LUMEN/commit/35c28579e85eac74159ada7e5c9b7f334e7d9dc3))
* Add OnSpawnPlayerSignal ([b94614c](https://github.com/WatchizzzX/LUMEN/commit/b94614c5a3be556ce58b07e7234ccb523cbd5e4b))
* Add PickupObject for containing start position ([3368d52](https://github.com/WatchizzzX/LUMEN/commit/3368d525c22316402ca1c782238eb540b287bef8))
* Add RespawnPlayerTrigger ([ea9f534](https://github.com/WatchizzzX/LUMEN/commit/ea9f5348e18a3f411eb0260f6d1641cf0bf15ff7))
* Add switch for walljump in PlayerController ([5dcc965](https://github.com/WatchizzzX/LUMEN/commit/5dcc965433ad2b64f8b2e0360f4d4d74ef5d2b32))
* Add two types of exit cameras in signal ([a72fcaa](https://github.com/WatchizzzX/LUMEN/commit/a72fcaa0605dcb68b48f9a62423d316436560416))
* Added extra acceleration to sliding when the player is on a slope ([da99185](https://github.com/WatchizzzX/LUMEN/commit/da991850fb374ca1b3ad33f4cfdea61399ef76f3))
* Now animators realize interface ([b81a92a](https://github.com/WatchizzzX/LUMEN/commit/b81a92a5361c2369f2a221f8c5013b126cbe7446))
* Now settings for managers are readonly in runtime ([549bb39](https://github.com/WatchizzzX/LUMEN/commit/549bb397aa5ca2b05be342d4ad28cf94a0484ccd))

# [0.11.0](https://github.com/WatchizzzX/LUMEN/compare/v0.10.0...v0.11.0) (2024-03-01)


### Bug Fixes

* Bootstraper now destroying when finished job ([17866b7](https://github.com/WatchizzzX/LUMEN/commit/17866b70c3a1bec00470a0eaa95689ba6f5d683e))
* Fix checking walls in PickupController ([3d9c071](https://github.com/WatchizzzX/LUMEN/commit/3d9c071ec83a5d3e7af65c3918c3ae1c8c206618))
* Separate pickup and interact event in input ([e00fd99](https://github.com/WatchizzzX/LUMEN/commit/e00fd990fce6651a33113ba7d6adb6c83e3f0f5c))
* Update Bootstrapper to new signals and managers ([1dc1b47](https://github.com/WatchizzzX/LUMEN/commit/1dc1b47e13f650b5368a36c76dc6b3d92419e7fc))
* Update GameManager to new signals ([e28b4c4](https://github.com/WatchizzzX/LUMEN/commit/e28b4c47a88a10fb7546668c1bb179a2e8b870c6))
* Update SpawnManager to new signals. Add camera caching ([bbd6dd6](https://github.com/WatchizzzX/LUMEN/commit/bbd6dd60b79ef1d75b0d3bf2f0f57590cbe19ff5))
* Update TransitionManager to new signals ([087f820](https://github.com/WatchizzzX/LUMEN/commit/087f8204f3a63129a97da67cf4b2513ce46e51d8))


### Features

* Add CameraManager ([2703e7e](https://github.com/WatchizzzX/LUMEN/commit/2703e7ee85a1f00feeb1882f00e3b34196e712fc))
* Add few signals to EventBus. Add interface to signals ([f2fd486](https://github.com/WatchizzzX/LUMEN/commit/f2fd48637d3f8ccf6d5ab1aad83eb51f6b6e623d))
* Add settings to managers ([1b39654](https://github.com/WatchizzzX/LUMEN/commit/1b396545e45ad36e48a84817d2434b81dffcc662))
* Add skybox shader ([4217537](https://github.com/WatchizzzX/LUMEN/commit/4217537ca0d1ac436522db325b437d75516bc0fb))
* Add Skybox, Material, Fog animators ([62c1269](https://github.com/WatchizzzX/LUMEN/commit/62c12693abbf0e5d02e1169fd8c174c24dab311c))
* Add triggers ([423f218](https://github.com/WatchizzzX/LUMEN/commit/423f218fb72bc9b66e99fe89adc00fd30b4e4435))

# [0.10.0](https://github.com/WatchizzzX/LUMEN/compare/v0.9.0...v0.10.0) (2024-02-28)


### Features

* Add Bootstrapper ([09f7335](https://github.com/WatchizzzX/LUMEN/commit/09f73352e657fc09ea2ec44d7d34db9d35cc3168))
* Add DDoL util script ([f0e9a50](https://github.com/WatchizzzX/LUMEN/commit/f0e9a50ab054653e04655f78b72b44d47910304c))
* Add EventBus and signals ([7e9d52e](https://github.com/WatchizzzX/LUMEN/commit/7e9d52e800a6e90eed345927afaf0d44affe3ad0))
* Add GameManager ([c485a37](https://github.com/WatchizzzX/LUMEN/commit/c485a37109ebcfe04f1b0f696397ed0105a2358d))
* Add SceneManager ([beb9917](https://github.com/WatchizzzX/LUMEN/commit/beb99179e852bca3234443a1d9bfa3fda840e7c9))
* Add ServiceLocator ([4d94fe9](https://github.com/WatchizzzX/LUMEN/commit/4d94fe95f24d88b911a0f43ee84c731b82375f99))
* Add SpawnManager ([35b1ef0](https://github.com/WatchizzzX/LUMEN/commit/35b1ef0838301d534127b62f879ccb95089886b0))
* Add SpawnManagerSettings ([e2c5620](https://github.com/WatchizzzX/LUMEN/commit/e2c562066c3920f37e77f4adc28755ebb1fdc8fb))
* Add TransitionManager ([fd108b8](https://github.com/WatchizzzX/LUMEN/commit/fd108b8c6d21cfd733585fa930b216ce880fde3b))
* Add TransitionManager settings ([698dd31](https://github.com/WatchizzzX/LUMEN/commit/698dd316380633604ecd2b255a1a49101b8d832e))

# [0.9.0](https://github.com/WatchizzzX/LUMEN/compare/v0.8.0...v0.9.0) (2024-02-27)


### Bug Fixes

* Add check for wall in PickupController ([cc770aa](https://github.com/WatchizzzX/LUMEN/commit/cc770aa8e3fe77fc5c54b79348c306e8cbc08a72))


### Features

* Add ability snapping to rigidbodies ([7e47d18](https://github.com/WatchizzzX/LUMEN/commit/7e47d18913e93519c5e50fdd145be836a8f0249e))
* Add ChangeAbilityToInteractive to InteractiveSwitch ([67f2e73](https://github.com/WatchizzzX/LUMEN/commit/67f2e73efd6a036c7a0d589d54ac8e2d626a066a))
* Add light animator ([f2833ee](https://github.com/WatchizzzX/LUMEN/commit/f2833eeedfdb2afb8d0e16bbde0fbed3dff3ffb1))
* Add Not element ([0c92605](https://github.com/WatchizzzX/LUMEN/commit/0c926053757ec607fcf3381372fc85a80ad2ab50))
* Add platform animator ([17ed94a](https://github.com/WatchizzzX/LUMEN/commit/17ed94aa03bb99a7e9652c0bf607358ac892e07f))


### Performance Improvements

* Add assembly definition to PickupSystem ([572c3ce](https://github.com/WatchizzzX/LUMEN/commit/572c3ce3c232db8507095852f1d4854ac45d47b7))

# [0.8.0](https://github.com/WatchizzzX/LUMEN/compare/v0.7.1...v0.8.0) (2024-02-25)


### Bug Fixes

* Fix calculating falling state ([7cd8002](https://github.com/WatchizzzX/LUMEN/commit/7cd8002d591eaa17ae9a08bfa2b7a1846c4b48b5))
* Make animator transitions faster ([23f993d](https://github.com/WatchizzzX/LUMEN/commit/23f993d05572febe5e594d96933868db4453a5e6))
* Remove wrong symbols ([9377aa1](https://github.com/WatchizzzX/LUMEN/commit/9377aa1f0a9432063268a5a9d5ef45d5fc4f3674))
* Rework interactables ([6fdd637](https://github.com/WatchizzzX/LUMEN/commit/6fdd637be84b740cea76827c9c1f8e07c90dd943))


### Features

* Add animators for props ([f72ee2d](https://github.com/WatchizzzX/LUMEN/commit/f72ee2d2f64af9c9ab4b7c49bbdc39062772473e))
* Rework input for SourceComponent ([efdd58d](https://github.com/WatchizzzX/LUMEN/commit/efdd58debda7a2dbf05c8f8a7643cf56daee2825))

## [0.7.1](https://github.com/WatchizzzX/LUMEN/compare/v0.7.0...v0.7.1) (2024-02-23)


### Bug Fixes

* Fix holding by adding rotation ([b8517c4](https://github.com/WatchizzzX/LUMEN/commit/b8517c4d5c8d22ad0810f590fed4a0280a849a20))

# [0.7.0](https://github.com/WatchizzzX/LUMEN/compare/v0.6.1...v0.7.0) (2024-02-23)


### Bug Fixes

* Fix LogicalComponent ([150b11e](https://github.com/WatchizzzX/LUMEN/commit/150b11ef3470ecbf06394746915eb9d52250a174))
* Fix LogicalComponentEditor ([4ba192a](https://github.com/WatchizzzX/LUMEN/commit/4ba192ae1ceb46a1da572ef173e101404f7c3c45))
* Fix OutputComponent ([7247510](https://github.com/WatchizzzX/LUMEN/commit/72475106516aeb9b9d0efe65a0e0efc04c7157f7))


### Features

* Add decreasing velocity when player on slope ([cd743bd](https://github.com/WatchizzzX/LUMEN/commit/cd743bd9b1627ae106d888186016cff49e20f4bb))
* Add features for PickupController ([1988d5c](https://github.com/WatchizzzX/LUMEN/commit/1988d5c195e9230c867b53b259b923717b89c176))
* Add isEnabledOnStart option ([b94839a](https://github.com/WatchizzzX/LUMEN/commit/b94839ae738f370990fd3ee6b538ef4eceb80652))

## [0.6.1](https://github.com/WatchizzzX/LUMEN/compare/v0.6.0...v0.6.1) (2024-02-22)


### Bug Fixes

* Fix SourceComponent. Revert deleted files ([f1f416d](https://github.com/WatchizzzX/LUMEN/commit/f1f416da253f1366bf0711f10c94fae1030a6522))

# [0.6.0](https://github.com/WatchizzzX/LUMEN/compare/v0.5.1...v0.6.0) (2024-02-21)


### Features

* Implemented PickupSystem ([2046900](https://github.com/WatchizzzX/LUMEN/commit/2046900458978b44f3bd50cae1f56167449091c1))


### Performance Improvements

* Optimize InteractionController ([0d3a329](https://github.com/WatchizzzX/LUMEN/commit/0d3a3298d0548a05a588c3f994dc19e90746224e))

## [0.5.1](https://github.com/WatchizzzX/LUMEN/compare/v0.5.0...v0.5.1) (2024-02-21)


### Bug Fixes

* Fix bug, when non-interactable object was on InteractableLayer ([646d950](https://github.com/WatchizzzX/LUMEN/commit/646d9501b04420cf96c0363d41bd1a83cf7d6f74))
* Rework pad for work with all physics bodies ([5a463f3](https://github.com/WatchizzzX/LUMEN/commit/5a463f367e96fdec535fa210ca78eacb83420923))

# [0.5.0](https://github.com/WatchizzzX/LUMEN/compare/v0.4.0...v0.5.0) (2024-02-21)


### Features

* Add CableControlller ([72df453](https://github.com/WatchizzzX/LUMEN/commit/72df45301437ab1949f89b9e05a13855d71b1d20))
* Add CleanedListFromNulls ([5189db7](https://github.com/WatchizzzX/LUMEN/commit/5189db7feccd912e7b0c67b7b32456b259f7425e))
* Implemeted InteractablePad (test) ([838d836](https://github.com/WatchizzzX/LUMEN/commit/838d836d63570fb94a71c6dfdd5ff5a0b7ea64a9))


### Performance Improvements

* Add editor assembly definition ([98ae2f6](https://github.com/WatchizzzX/LUMEN/commit/98ae2f6bb56f7e91d282295c574a3059f975e1c9))
* Add LogicalSystem assembly definition ([0418390](https://github.com/WatchizzzX/LUMEN/commit/0418390e2e4f0b66d4c8c468bc84e2d42e8f5300))

# [0.4.0](https://github.com/WatchizzzX/LUMEN/compare/v0.3.1...v0.4.0) (2024-02-21)


### Features

* Add ListExtensions and clear input nodes ([d11f03e](https://github.com/WatchizzzX/LUMEN/commit/d11f03eef488ec60f3eff89580fd22bb36dd4b52))
* Add LogicComponentEditor ([17f4cac](https://github.com/WatchizzzX/LUMEN/commit/17f4cacf1b07dc94f1388f976f32017fd64aab17))

## [0.3.1](https://github.com/WatchizzzX/LUMEN/compare/v0.3.0...v0.3.1) (2024-02-21)


### Bug Fixes

* Add recalculate call on start ([6916cb3](https://github.com/WatchizzzX/LUMEN/commit/6916cb34d4e5467b16fdafd01789a091c230907f))
* Fix logic in components ([7e45b98](https://github.com/WatchizzzX/LUMEN/commit/7e45b989ef67419b530792d71743937374a4dfd4))

# [0.3.0](https://github.com/WatchizzzX/LUMEN/compare/v0.2.0...v0.3.0) (2024-02-20)


### Bug Fixes

* Fix bug when no interactables around ([46517e7](https://github.com/WatchizzzX/LUMEN/commit/46517e74c7952548bdaac14dc53c7469924cac16))


### Features

* Add clear method Interact to work with events ([b3d735a](https://github.com/WatchizzzX/LUMEN/commit/b3d735a4b79d3b89cf51693089f7ccc9c8897c60))
* Add debug draw to LogicalComponent ([3d24d3e](https://github.com/WatchizzzX/LUMEN/commit/3d24d3e02af02f9f3e47328a55e86728d69dbdda))
* Add debug draw to SourceComponent ([535cdf5](https://github.com/WatchizzzX/LUMEN/commit/535cdf5a91ef15ae5b9b891a53d33feb8d49bad0))
* Add HandlesDrawer to debug draw LogicalSystem ([cea6dbb](https://github.com/WatchizzzX/LUMEN/commit/cea6dbbf32a2046dfb96eeee2bc09e757de42391))
* Add logger channel to LogicalSystem ([bda2b84](https://github.com/WatchizzzX/LUMEN/commit/bda2b845e7c5be9ac4e50d551e8a8134e50688a4))
* Add OutputComponent ([5414164](https://github.com/WatchizzzX/LUMEN/commit/54141643e141ddfc91ac28d29db57036ffe67d7d))
* Implemented LogicalSystem (testing) ([4033ea5](https://github.com/WatchizzzX/LUMEN/commit/4033ea555eca45081cafbe26514f8bc6481d7d79))


### Performance Improvements

* Add assembly definition to LogicalSystem ([c40183e](https://github.com/WatchizzzX/LUMEN/commit/c40183e5c1720144eb919aab14ffbcbb93b6cb1c))

# [0.2.0](https://github.com/WatchizzzX/LUMEN/compare/v0.1.0...v0.2.0) (2024-02-19)


### Bug Fixes

* Add new logger channel for interactable. Refactor ([ef58c48](https://github.com/WatchizzzX/LUMEN/commit/ef58c489d5604d0550c557366daff244245a0cae))
* Interact event now work as a trigger ([b78de2b](https://github.com/WatchizzzX/LUMEN/commit/b78de2b663928863dcf0811510c69d72e1bff770))


### Features

* Add InteractableSystem ([5728222](https://github.com/WatchizzzX/LUMEN/commit/5728222c6fbabd25d17c5f0e797c15abe026bf28))
* Add Logger system and implemented ([e228e52](https://github.com/WatchizzzX/LUMEN/commit/e228e528f69504d352b9d360b71083500d8e8b00))

# [0.1.0](https://github.com/WatchizzzX/LUMEN/compare/v0.0.2...v0.1.0) (2024-02-19)


### Bug Fixes

* Fix rotating and double coyote time ([c06b7c1](https://github.com/WatchizzzX/LUMEN/commit/c06b7c145380f4c7d648823bf710f1f186827900))
* jump event now is a trigger ([74c36f6](https://github.com/WatchizzzX/LUMEN/commit/74c36f6de8faf443179a438ca54506fd19892c0f))
* PlayerController realize IMovementController ([d323a0c](https://github.com/WatchizzzX/LUMEN/commit/d323a0cdc5283bcfe392e8b25c2dd9aaff959d65))


### Features

* Add IMovementController ([fdff1be](https://github.com/WatchizzzX/LUMEN/commit/fdff1bedec0f042e1354592935dc74031f9c6273))
* Add input handler ([899ab9f](https://github.com/WatchizzzX/LUMEN/commit/899ab9f0983cc7de78605edb6769e981edd28046))
* Add jump cooldown timer to PlayerController ([10c23dc](https://github.com/WatchizzzX/LUMEN/commit/10c23dc682f5eaf14af4ff42bd1e5f88edbac85b))
* Add player animator component ([4fbe217](https://github.com/WatchizzzX/LUMEN/commit/4fbe217938b3a304c5711c95118f08c755a605d3))
* Add player controller ([a07da3e](https://github.com/WatchizzzX/LUMEN/commit/a07da3e1e9c75b95a65ed9b225e71c47bc23ad7f))
* Add sprint control in air ([8323154](https://github.com/WatchizzzX/LUMEN/commit/8323154be0f499e5b5905cf28429e8d294b3c4fc))
* Now animator doesn't linked to Input ([195be04](https://github.com/WatchizzzX/LUMEN/commit/195be048777abeae8bb04fb5793258fcbeeecc2e))


### Performance Improvements

* Add assemble definition for input ([bc03390](https://github.com/WatchizzzX/LUMEN/commit/bc03390c93a1b1434d12e62f9a5fa3e8c147a432))
* Add assemble definition for Utils ([e21866c](https://github.com/WatchizzzX/LUMEN/commit/e21866c91759d460fbf9574ccb1abe5f8fda2046))
* Add extensions for vectors ([52666ab](https://github.com/WatchizzzX/LUMEN/commit/52666ab1d4fdffbe16fe8f42174d60fcc1efe8ba))
* Add Player assembly definition ([f9154c1](https://github.com/WatchizzzX/LUMEN/commit/f9154c18b3fa6c12c1d5c7a88690c247ae1f40f7))
