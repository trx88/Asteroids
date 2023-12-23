# RovioAsteroids
## Windows build of the game
## 3rd party packages
* Zenject
* Newtonsoft Json
* AddressablesManager
# Code design & decisions
This section describes the most important elements used in implementing the game.
## Zenject
Zenject framework is used to provide dependency injection capabilies. It's used throughout the project to inject services/controllers/signals/factories/repositories and ease the usage of mentioned entities.

## Chain Of Responsibility
The idea behind the Chain Of Responsibility's implementation in this project is to deal with game logic prior to enemy destruction. Since game has multiple enemies, enemies' game object is passed through the chain, and each handler has a specific logic that will be executed. Most obvious example is when player destroys a large asteroid when three small asteroids should be spawned. AsteroidLargeHandler removes the GameObject from the collection of all spawned enemies, removes it from InMemoryRepository (more on that later), updates the score using the ScoringService, uses EnemySpawnerService to spawn three small asteroids and finally destroys the GameObject. If we look at the AsteroidSmallHandler, it does almost everything the prior handler does with the exception of spawning more asteroids. 
All enemy handlers are registered inside EnemyHandlerService.

## Signals & Controllers
Since EnemyHandlerService is mentioned above, it's a good time to introduce EnemyCollisionController and Zenject Signals. They idea behind using Signals is to complement the usage of abovementioned handlers. Whenever collision detection is triggered inside a GameObject script, a Signal with colliding GameObject is sent. EnemyCollisionController subscribes to this Signal, and when it arrives it passes the GameObject through the described Chain Of Responsibility. 

## GameObject factories & Addressables
Since Zenject is used for injecting, might as well use Zenject for instantiating prefabs. Two factories where created for this purpose - EnemyFactory and LaserFactory. Both classes are loading prefabs stored in Addressables using the AddressablesManager (3rd party package). Factories have a separate Create method for each GameObject variant. Factories are injected into services/controllers that need them.

## Services
A couple of services were implemented to help with the separation of concerns:
EnemyHandlerService - used for Chain Of Responsibility
EnemySpawnerService - all the logic for spawning enemies in the game
ScoringService - updates the player's score by using Repository

## Repository
### Base Repository
Repository was implemented as well, but with twist - separating data by storage type (InMemory & PlayerPrefs). Although an overkill for this scope, I'm used to using Repository since it can be used not just for storage, but for data change callback as well. Also, it ties perfectly into UI's MVVM.

Base Repository is implemented in a standard way (with CRUD methods) with addition of data change Actions other classes subscribe to. Specific repositories are InMemoryRepository (data is stored in-memory and disappear when game is closed) and PlayerPrefsRepository that is storing data in PlayerPrefs. Each repository can initialize using an initialization action for specific data model (basically set default values and/or load stored data in case of PlayerPrefsRepository).

### Repository Factories
RepositoryFactory (and each of it's specific variants) is used to create or get a repository for specific data model. In order to get or create a repository for a specific data model, RepositoryConfig must be created for that specific data model that consists of a type and an optional InitializationAction.
All created repositories are stored in the IServiceContainer (System.ComponentModel.Design), since it stores services using types (repositories are read by type as well). Both factories are installed in the Zenject installer, and their configurations added along with initialization actions.
By using this approach, each specific RepositoryFactory can be injected where needed and RepositoryOf<> method used get access to a desired repository. 

### Initialization Actions
Like it was mentioned above, an InitializationAction can be used to initialize a repository. This boils down to using Repository's Create method to set default values for the data model, and by doing so create a repository for that data model. It makes sense to always use InitializationAction with PlayerPrefsRepository, since PlayerPrefs data should have a default value when the game starts. InMemoryRepository repositories can be initialized during gameplay, since data can come from variety of places (in-game logic, API calls, etc.)

## UI & MVVM
Speaking of overkills, here's another - MVVM. Couldn't resist, since Repository was already implemented. It really complements the Repository, since one can rely on notifying other classes that data has changed. And that's perfect for the UI.
### Model
Model layer is already solved by using Repository. 
### View
View was implemented very simple, since there was no need (nor time) to integrate some 3rd party UI plugin with full UI features (animated Show/Hide, view components, etc.). It's a simple class that holds references to four TextMeshPro's that are updated when repository ItemChanged Action is invoked. 
The goal is to keep the View as oblivious as possible. It is a MonoBehavior, since some Unity-specific code might be implemented (like animations).
### ViewModel
ViewModel is where all the logic should live. ViewModel is tasked with getting the data, do any logic, and trigger the View to show the needed data. To help with this, a Bindable<T> was implemented. A Bindable binds to an Action provided by the View. When the Bindable data is set, provided Action is invoked triggering the View and notifying it what has changed. Actual UI elements are updated inside that callback method. MVVM has a clean separation of concerns. Although sometimes it's tempting to go around it, MVVM has no point then.
## Private constructors
There a lot of private constructors (usually for services and similar classes). This was done to prevent instantiating those classes by accident. The idea is to inject them, not create instances. Of course, that can be changed if the need arises.
# Challenges
I've never  using Zenject with unit testing in Unity, so I had to figure out what I need to include to able to use it correctly (game depends on dependency injection after all ba-dum-tsss). 
I resorted to creating a BaseUnitTest class that inherits ZenjectUnitTestFixture. That way I was able to install everything like in the MainInstaller in the actual game. Not sure if this is a good practice, but it enabled me to write tests that use Zenject.

![image](https://github.com/trx88/RovioAsteroids/assets/10126815/1d7eb7a7-eede-4161-8729-5a591f4f4fb3)

