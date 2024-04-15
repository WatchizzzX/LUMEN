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
