# Thugs of Caribbean - Systems Documentation

## Table of Contents
1. [Executive Summary](#executive-summary)
2. [Architecture Overview](#architecture-overview)
3. [Project Structure](#project-structure)
4. [Core Systems](#core-systems)
5. [Player System](#player-system)
6. [Enemy System](#enemy-system)
7. [Game Flow & States](#game-flow--states)
8. [Utility Systems](#utility-systems)
9. [Data Flow Diagrams](#data-flow-diagrams)
10. [Development Guide](#development-guide)

---

## Executive Summary

**Thugs of Caribbean** is a 2D top-down pirate-themed ship combat game built with **Unity 2022.3.14f1**. The game features player-controlled ship combat against spawning enemy waves with configurable difficulty settings.

**Key Characteristics:**
- **State Machine Architecture** - Clean game flow via hierarchical state management
- **Component-Based Design** - Modular, reusable systems for health, damage, pooling, UI
- **Input System Integration** - Modern Input System for player controls
- **Physics-Based Movement** - Rigidbody2D for ships and projectiles
- **Event-Driven Communication** - UnityEvents for loose coupling between systems

**Target Platform:** WebGL (primary), expandable to other platforms

---

## Architecture Overview

### Design Patterns Used

| Pattern | Implementation | Purpose |
|---------|---|---|
| **State Machine** | `GameplayStateMachine` with concrete states | Orchestrate game flow (Start → Gameplay → Pause → Gameover) |
| **Component Pattern** | Health, Damage, Spawner, Audio components | Reusable, modular functionality |
| **Object Pool** | `CannonBallPool` | Efficient projectile management |
| **Observer Pattern** | UnityEvents (`OnDie`, `OnDamage`, etc.) | Decouple systems with event broadcasting |
| **Inheritance** | `GameplayConcreteState` base class | Standardize state implementation |
| **Strategy Pattern** | `Cannon` abstract class | Support different firing modes (Player vs Enemy) |
| **Dependency Injection** | Constructor-based state dependencies | Explicit dependency management, no FindObjectOfType |

### Key Principles

- **Separation of Concerns** - Each system handles one responsibility
- **Modularity** - Systems can be extended without affecting others
- **Reusability** - Core components (Health, Damage, Audio) shared across player/enemies
- **Data-Driven** - Game settings stored in PlayerPrefs for configuration

---

## Project Structure

```
Assets/Scripts/
│
├── GameManager/                    # Game orchestration & flow
│   ├── MainMenu/
│   │   └── GameManager.cs          # Main menu & settings UI
│   └── StateMachine/
│       ├── GameplayStateMachine.cs # State orchestrator
│       ├── States/
│       │   ├── StartState.cs       # Initialize game, reset data
│       │   ├── GameplayState.cs    # Main gameplay loop
│       │   ├── PauseState.cs       # Pause menu handling
│       │   └── GameOverState.cs    # Game over screen
│       └── GameplayConcreteState.cs # Abstract base for states
│
├── Player/                         # Player ship mechanics
│   ├── PlayerMovement.cs           # WASD movement + mouse rotation
│   ├── PlayerHealth.cs             # Health with death effects
│   ├── PlayerShotsUpdater.cs       # UI display for ammo/cannons
│   └── Cannon/
│       └── PlayerAttack.cs         # Shooting, ammo, reload, mode switching
│
├── Enemies/                        # Enemy implementations
│   └── Ship/
│       └── Minion/
│           ├── Chaser/             # Pursuing melee enemy
│           │   ├── ChaserMovement.cs
│           │   ├── ChaserHealth.cs
│           │   └── ChaserDetonation.cs
│           └── Shooter/            # Ranged enemy
│               ├── ShooterMovement.cs
│               ├── ShooterAttack.cs
│               └── ShooterCannon.cs
│
├── Core/                           # Shared systems & components
│   ├── Components/
│   │   ├── Health.cs               # Base health system
│   │   ├── Damage/
│   │   │   ├── DamageGateway.cs   # Route damage to health
│   │   │   └── VisualDamageFeedback.cs # Damage effects
│   │   ├── Cannon/
│   │   │   └── CannonBall.cs      # Projectile base
│   │   ├── Counters/
│   │   │   ├── TimerCounter.cs    # Game timer MM:SS
│   │   │   └── ScoreCounter.cs    # Enemy kill score
│   │   ├── Spawner/
│   │   │   ├── EnemySpawner.cs    # Enemy spawning controller
│   │   │   └── SpawnArea.cs       # Screen edge spawn points
│   │   ├── 2DComponents/
│   │   │   └── SpriteChangerByHealth.cs # Health-based sprite swapping
│   │   ├── HealthUIManager.cs      # Health bar UI display
│   │   └── Audio/
│   │       └── UIAudioManagerBase.cs # UI sound effects
│   ├── Enemies/
│   │   └── TargetNearbyDetector.cs # Detect player in range
│   └── Enums/
│       └── CannonTypes.cs          # Single vs Multiple cannon modes
│
└── Utils/                          # Utility systems
    ├── Pools/
    │   └── CannonBallPool.cs       # Projectile object pooling
    ├── SightRaycast/
    │   └── SightDetection.cs       # Raycast-based target detection
    └── Class Extensions/
        └── Vector extensions       # Helper methods
```

---

## Core Systems

### 1. State Machine System

**Purpose:** Orchestrates game flow through distinct game states with clean transitions.

**File:** `Assets/Scripts/GameManager/StateMachine/GameplayStateMachine.cs`

**States Overview:**

| State | Purpose | Entry Behavior | Update Behavior |
|-------|---------|---|---|
| **StartState** | Game initialization | Reset player health, reset score, start timer | Wait for transition to Gameplay |
| **GameplayState** | Active gameplay | Enable input, start enemy spawning | Detect pause input, check timer/health for end conditions |
| **PauseState** | Paused game | Disable input, set Time.timeScale = 0 | Display pause menu, wait for resume input |
| **GameOverState** | Game ended | Disable input, show game over screen | Display final score, wait for restart/exit |

**State Architecture:**

```csharp
public abstract class GameplayConcreteState
{
    // Lifecycle
    public abstract void OnStateEnter();
    public abstract void OnUpdateState();
    
    // Transition
    protected void SwitchState(GameplayConcreteState nextState)
    {
        _gameSM.SwitchState(nextState);
    }
}
```

**Key Components Managed by State Machine:**
- `TimerCounter` - Game time tracking
- `PlayerHealth` - Player status monitoring
- `PlayerInput` - Input system control
- `EnemySpawner` - Enemy wave management
- `HealthUIManager` - UI updates
- `ScoreCounter` - Score tracking

**State Transitions:**

```
    ┌─────────────────────────────────────────────┐
    │          StartState                         │
    │  • Reset player health                      │
    │  • Reset score                              │
    │  • Start game timer                         │
    └─────────────────────────────────────────────┘
                      │ OnStateEnter complete
                      ▼
    ┌─────────────────────────────────────────────┐
    │          GameplayState                      │
    │  • Enemy spawning active                    │
    │  • Player input enabled                     │
    │  • Listen for Pause (ESC key)               │
    │  • Monitor game end conditions              │
    └─────────────────────────────────────────────┘
        │                                   ▲
        │ ESC key pressed                   │ Resume game
        ▼                                   │ (ESC key)
    ┌─────────────────────────────────────────────┐
    │          PauseState                         │
    │  • Time.timeScale = 0                       │
    │  • Player input disabled                    │
    │  • Pause menu displayed                     │
    └─────────────────────────────────────────────┘
        │
        │ Timer reaches 0 OR Player dies
        ▼
    ┌─────────────────────────────────────────────┐
    │          GameOverState                      │
    │  • Game over screen shown                   │
    │  • Final score displayed                    │
    │  • Restart or Exit options                  │
    └─────────────────────────────────────────────┘
```

**Time Control:**
- `Time.timeScale = 1` - Normal gameplay
- `Time.timeScale = 0` - Pause state
- Affects all physics and animations automatically

---

### 2. Input System

**Framework:** Unity's new Input System (Input System 1.7.0 package)

**Player Actions:**

| Action | Input | Handler | Effect |
|--------|-------|---------|--------|
| **Move** | WASD | `PlayerMovement.MovePlayer()` | Move ship forward in facing direction |
| **Rotate** | Mouse | `PlayerMovement` Update | Rotate ship toward cursor |
| **Fire** | Left Mouse | `PlayerAttack.FireCannon()` | Shoot projectiles (with ammo/reload) |
| **Swap Cannon** | E | `PlayerAttack.ChangeCurrentCannonType()` | Toggle Single ↔ Multiple cannons |
| **Pause** | ESC | `GameplayState` | Toggle Pause/Gameplay states |

**Input Binding Flow:**

```
Input Asset (InputActions.inputactions)
    ↓
PlayerInput.cs (GetComponent by states)
    ↓
Callback registration: InputAction.started += Callback
    ↓
Handler method (e.g., MovePlayer) with InputAction.CallbackContext
```

**Context Callback Pattern:**
```csharp
private void MovePlayer(InputAction.CallbackContext context)
{
    Vector2 direction = context.ReadValue<Vector2>();
    // Apply movement
}
```

---

## Player System

### Player Movement

**File:** `Assets/Scripts/Player/PlayerMovement.cs`

**Movement Types:**

1. **Rotation** - Continuous rotation toward mouse cursor
   - Uses `Quaternion.Slerp` for smooth interpolation
   - `_rotateSpeed` = 2 (default)
   - Calculated angle via `Atan2(mousePos.y, mousePos.x)`

2. **Translation** - WASD-based forward movement
   - Applies force to Rigidbody2D in facing direction
   - `_moveSpeed` = 6 units/frame (configurable)
   - Velocity reset on collision with islands

**Physics Setup:**
- `Rigidbody2D` component required
- Gravity Scale = 0 (2D top-down)
- Collision detection for boundary/obstacle interaction

**Control Flow:**

```
PlayerMovement.Update()
    │
    ├─ GetDirectionWherePlayerFaces()
    │   └─ Rotate toward mouse
    │
    └─ MovePlayer(InputAction.CallbackContext)
        └─ Apply velocity to Rigidbody2D
```

---

### Player Attack System

**File:** `Assets/Scripts/Player/Cannon/PlayerAttack.cs`

**Core Mechanics:**

1. **Ammunition**
   - `ShootsRemaining` counter (starts at `_ammoPerRound`)
   - Auto-reload when empty
   - `ReloadTime` configurable (default: 2.5 seconds)

2. **Cannon Modes**
   - **Single**: Fires 1 projectile from primary cannon
   - **Multiple**: Fires from all cannons simultaneously
   - Switched via E key input

3. **Firing Process**
   ```
   FireCannon Input
       ↓
   Can shoot? (has ammo, not reloading)
       ├─ YES: Get pooled CannonBall
       │       └─ Spawn at cannon position
       │       └─ Set velocity to forward direction
       │       └─ Decrement ShootsRemaining
       │       └─ Play fire audio
       └─ NO: Ignore or play "empty" sound
   ```

4. **Reload Cycle**
   ```
   ShootsRemaining == 0
       ↓
   IsReloading = true
   Wait(_reloadTime)
       ↓
   ShootsRemaining = _ammoPerRound
   IsReloading = false
   ```

**Events:**
- `OnShoot` - Triggered when projectile fires (for audio)
- `OnSwapCannonType` - Triggered when cannon type changes (updates UI)
- `OnAmmoChange` - Ammo count update (syncs UI)
- `OnReloadStart`/`OnReloadEnd` - Reload state changes

**UI Synchronization:**
- Real-time ammo display via `PlayerShotsUpdater`
- Current cannon type icon
- Reload progress indicator

---

### Player Health & Death

**File:** `Assets/Scripts/Player/PlayerHealth.cs`

**Features:**
- Extends `Health` base class
- Damage via collision with enemy projectiles or charged enemies
- Death triggers:
  - `OnDie` UnityEvent
  - Visual death effect (red flash, explosion VFX)
  - State transition to `GameOverState`

**Death Flow:**
```
CannonBall hits Player
    ↓
DamageGateway.ApplyDamageOnHealth()
    ↓
PlayerHealth.DecreaseHealth(damageAmount)
    ↓
CurrentHealth <= 0
    ↓
OnDie.Invoke()
    ├─ VisualDamageFeedback plays death VFX
    └─ GameplayStateMachine switches to GameOverState
```

---

## Enemy System

### Enemy Types

#### Chaser Enemy

**Purpose:** Aggressive melee unit that pursues player and detonates on contact.

**Components:**
- `ChaserMovement` - Pursuit logic
- `ChaserHealth` - Health tracking
- `ChaserDetonation` - Collision damage dealing

**Mechanics:**

| Property | Value | Purpose |
|----------|-------|---------|
| Move Speed | 4 units/frame | Pursuit velocity |
| Rotation Speed | 2 | Smooth aim toward target |
| Damage | 25 | Detonation damage to player |
| Health | 30 | Shots to kill (5 with standard cannon) |

**Movement Algorithm:**
```csharp
// Each frame
Vector2 direction = (Target.position - transform.position).normalized;
Rigidbody2D.velocity = direction * _moveSpeed;

// Rotation
Vector3 direction = Target.position - transform.position;
Quaternion targetRotation = Quaternion.LookRotation(transform.forward, direction);
transform.rotation = Quaternion.Slerp(current, targetRotation, rotationSpeed * Time.deltaTime);
```

**Behavior Flow:**
```
Spawn at screen edge
    ↓
Detect player in range (TargetNearbyDetector)
    ├─ NO: Wander (minimal)
    ├─ YES: AllowPursue()
    │       ├─ Each frame: Move toward player
    │       └─ Rotation toward target
    │
    ▼
Player collision
    ↓
ChaserDetonation.OnTriggerEnter2D()
    ├─ Apply 25 damage via DamageGateway
    └─ Die (destroyed or despawned)
```

---

#### Shooter Enemy

**Purpose:** Ranged unit that fires projectiles when player is in sight.

**Components:**
- `ShooterMovement` - Base positioning (static/minimal movement)
- `ShooterAttack` - Target detection & firing control
- `ShooterCannon` - Projectile spawning

**Mechanics:**

| Property | Value | Purpose |
|----------|-------|---------|
| Sight Range | 15 units | Detection radius via raycast |
| Fire Interval | 0.5 sec (configurable) | Time between shots |
| Reload Time | 3 sec (configurable) | Rest between volleys |
| Ammo Per Volley | 3 (configurable) | Shots before reload |
| Health | 40 | Projectile resistance |

**Attack State Machine:**

```
Idle (no target in sight)
    │
    ├─ TargetNearbyDetector.OnTargetNearby
    │
    ▼
Firing Loop
    │
    ├─ Fire projectile
    ├─ Wait _intervalBetweenShoots
    ├─ Projectile count < _ammoAmount?
    │   ├─ YES: Loop to Fire
    │   └─ NO: Go to Reload
    │
    ▼
Reload
    │
    ├─ Wait _reloadTimeSeconds
    ├─ Target still in sight?
    │   ├─ YES: Go to Firing Loop
    │   └─ NO: Go to Idle
    │
    ▼
Back to Idle (when target lost)
```

**Firing Mechanism:**
```csharp
// ShooterCannon.Shoot()
for (int i = 0; i < ammoAmount; i++)
{
    SpawnProjectile();
    yield return new WaitForSeconds(intervalBetweenShoots);
}
yield return new WaitForSeconds(reloadTimeSeconds);
// Repeat if target still in sight
```

---

### Enemy Shared Systems

#### Target Detection

**File:** `Assets/Scripts/Core/Enemies/TargetNearbyDetector.cs`

**Purpose:** Distance-based target detection for enemy AI.

**Mechanism:**
```csharp
// Each frame
float distance = Vector2.Distance(transform.position, targetPosition);
if (distance < _rangeDetection && !IsTargetNearby)
{
    IsTargetNearby = true;
    OnTargetNearby.Invoke();
}
else if (distance >= _rangeDetection && IsTargetNearby)
{
    IsTargetNearby = false;
    OnTargetNotNearby.Invoke();
}
```

**Default Range:** 15 units (configurable)

**Integration:** Used by spawner to link player as target to newly spawned enemies

---

#### Enemy Spawning

**File:** `Assets/Scripts/Core/Components/Spawner/EnemySpawner.cs`

**Spawning Process:**

1. **Check Spawn Availability**
   ```
   For each SpawnArea:
       Is area off-screen (invisible)?
       └─ Mark as available spawn point
   ```

2. **Select Enemy Type**
   ```
   Random selection from available enemy prefabs:
   - 50% Chaser
   - 50% Shooter
   (Weights configurable via inspector)
   ```

3. **Instantiate Enemy**
   ```
   Spawn at random visible spawn area
   Set player as target reference
   Add to active enemy list
   ```

4. **Schedule Next Spawn**
   ```
   Wait for spawn interval (from PlayerPrefs: "ENEMEIS_SPAWN_INTERVAL")
   Default: every 2 seconds
   Range: 1-10 seconds (configurable in menu)
   ```

**Spawn Areas:**
- Top edge (off-screen)
- Bottom edge (off-screen)
- Left edge (off-screen)
- Right edge (off-screen)

**Start/Stop Control:**
```csharp
// Called by GameplayState
EnemySpawner.StartSpawning()   // Begin coroutine
EnemySpawner.StopSpawning()    // Stop coroutine (pause/gameover)
```

---

#### Enemy Movement Base Class

**File:** `Assets/Scripts/Enemies/Ship/Minion/EnemyShipMovement.cs`

**Purpose:** Foundation for all enemy movement types.

**Public Interface:**
```csharp
public class EnemyShipMovement : MonoBehaviour
{
    public Transform Target { get; set; }
    
    public virtual void AllowPursue()    // Enable movement toward target
    public virtual void PreventPursue()  // Disable movement
}
```

**Derived Classes:**
- `ChaserMovement` - Override Update() with aggressive pursuit
- `ShooterMovement` - Minimal movement (static positioning)

---

## Game Flow & States

### Game Loop Overview

```
START
  ↓
[StartState]
  • Initialize player: reset health, reset ammo
  • Initialize score: set to 0
  • Initialize timer: countdown from limit (default 5 min)
  ↓
[GameplayState] ◄─────────────────────────┐
  • Spawn enemies continuously             │
  • Process player input (WASD, click, E)  │
  • Update physics (movement, collisions)  │
  • Check win/lose conditions              │
  • Listen for ESC (Pause)                 │
  │                                        │
  ├─ ESC pressed ──────────────┐           │
  │  or click Pause            │           │
  │                            ▼           │
  │                     [PauseState]       │
  │                      • Time.timeScale=0│
  │                      • Pause menu UI   │
  │                      • ESC to resume ──┤
  │                                        │
  │ Timer == 0 OR PlayerHealth <= 0        │
  │                            ▼           │
  ▼                      [GameOverState]   │
  └─────────────────────────────────────────
                         • Show final score
                         • Show game over UI
                         • Restart / Exit options
```

### Win/Lose Conditions

**LOSE:** Player health reaches 0
```
PlayerHealth.CurrentHealth <= 0
    ↓
OnDie event triggered
    ↓
GameplayStateMachine → GameOverState
    ↓
Display "GAME OVER" screen with final score
```

**TIME UP:** Timer reaches 0
```
TimerCounter.HasTimerReachedZero == true
    ↓
(Checked in GameplayState.OnUpdateState)
    ↓
GameplayStateMachine → GameOverState
    ↓
Display "TIME'S UP" or "GAME OVER" with score
```

**Victory:** Complete the time limit
```
Survive until timer reaches 0
    ↓
(Optional: Show "VICTORY" or continue to GameOverState)
    ↓
Display final score and statistics
```

---

### Game Settings & Configuration

**Storage:** PlayerPrefs (persisted across sessions)

| Setting | Key | Type | Default | Range |
|---------|-----|------|---------|-------|
| Enemy Spawn Interval | `ENEMEIS_SPAWN_INTERVAL` | float (seconds) | 2.0 | 1.0 - 10.0 |
| Game Time Limit | `TIMER_LIMIT` | float (minutes) | 5.0 | 1.0 - 15.0 |

**UI Settings Panel:**
- Accessed from Main Menu
- Sliders for each setting
- Real-time preview
- Audio feedback on changes

---

## Utility Systems

### 1. Health System

**Base Class:** `Assets/Scripts/Core/Components/Health.cs`

**Core Interface:**
```csharp
public class Health : MonoBehaviour
{
    [SerializeField] protected float _maxHealth = 100;
    public float CurrentHealth { get; protected set; }
    public bool IsAlive { get; protected set; }
    public UnityEvent OnDie { get; }
    
    public virtual void DecreaseHealth(float damageAmount)
    public float GetHealthPercentage()
    public void ResetStatus()
}
```

**Inheritance Chain:**
```
Health (base)
  ├─ PlayerHealth
  │   └─ Death effects (explosion VFX)
  │
  ├─ ChaserHealth
  │   └─ Custom death handling
  │
  └─ (Shooter uses Health via DamageGateway)
```

**Health Lifecycle:**
```
Spawn: CurrentHealth = MaxHealth, IsAlive = true
   ↓
Take Damage: DecreaseHealth() called
   ├─ CurrentHealth -= damage
   ├─ OnDamage event triggered (for feedback)
   ├─ CurrentHealth <= 0?
   │  └─ IsAlive = false
   │  └─ OnDie.Invoke()
   │
Done: Entity is dead, ready for cleanup/despawn
```

---

### 2. Damage System

**Flow Architecture:**

```
Projectile (CannonBall)
    ├─ Rigidbody2D.Velocity set
    ├─ Moves forward each frame
    │
    ▼
    OnTriggerEnter2D (hit detection)
    │
    ├─ Get collider's DamageGateway component
    ├─ Call ApplyDamageOnHealth(damageAmount)
    │
    ▼
    DamageGateway.ApplyDamageOnHealth()
    │
    ├─ Get Health component from same GameObject
    ├─ Call Health.DecreaseHealth(damageAmount)
    ├─ Trigger OnDamage UnityEvent
    │
    ▼
    Health.DecreaseHealth()
    │
    ├─ CurrentHealth -= damageAmount
    ├─ CurrentHealth <= 0?
    │  ├─ YES: IsAlive = false, OnDie.Invoke()
    │  └─ NO: Continue
    │
    ▼
    VisualDamageFeedback
    ├─ Flash sprite red
    ├─ Play particle effects (if death)
    └─ Play impact sound
```

**Components:**

| Component | File | Purpose |
|-----------|------|---------|
| DamageGateway | `Core/Components/Damage/DamageGateway.cs` | Routes damage to Health component |
| VisualDamageFeedback | `Core/Components/Damage/VisualDamageFeedback.cs` | Visual/audio feedback on damage |

**DamageGateway:**
```csharp
public void ApplyDamageOnHealth(float damageAmount)
{
    _health.DecreaseHealth(damageAmount);
    OnDamage.Invoke();
}
```

**VisualDamageFeedback Features:**
- **Damage Flash:** Lerps sprite color to red on hit, lerps back to original
- **Death VFX:** Explosion particle system + sprite swap
- **Duration:** `_flashDuration` (default 0.3 seconds)

---

### 3. Projectile System

**File:** `Assets/Scripts/Core/Components/Cannon/CannonBall.cs`

**Lifecycle:**

```
Spawn (from pool)
  │
  ├─ Position set to cannon
  ├─ Velocity set to _speed * forward direction
  ├─ Visual & collision enabled
  │
  ▼
  Move forward (every frame)
  │
  ├─ Velocity = _speed * transform.forward
  ├─ Check for collision
  │
  ├─ Hit detected? OnTriggerEnter2D()
  │  ├─ Apply damage via DamageGateway
  │  ├─ Play impact audio
  │  ├─ Show explosion sprite
  │  ├─ Disable visual & collision
  │  └─ Start despawn coroutine
  │
  ├─ Out of bounds?
  │  └─ Despawn (after _timeToBackPool)
  │
  ▼
  Return to pool (BackToPool())
    ├─ Deactivate GameObject
    ├─ Return to CannonBallPool
    └─ Ready for reuse
```

**Properties:**
- `_speed` - Projectile velocity (units/frame)
- `_damage` - Damage amount per hit
- `_timeToBackPool` - Seconds before despawn if no hit (default 1.5)

---

### 4. Object Pooling

**File:** `Assets/Scripts/Utils/Pools/CannonBallPool.cs`

**Purpose:** Reuse projectile objects instead of instantiate/destroy for performance.

**Configuration:**
- **Initial Size:** 10 projectiles pre-allocated
- **Max Size:** 20 total (can't grow beyond this)
- **Factory:** Creates instances on demand up to max

**Pool Lifecycle:**

```
Initialize()
  └─ Create 10 CannonBall instances, deactivate

GetProjectileFromPool()
  └─ Activate and return instance from pool

// Usage
CannonBall proj = pool.Get();
proj.transform.position = cannon.position;
proj.Fire();

OnProjectileHit() or OnTimeExpired()
  └─ proj.SetPoolIfItHasNone(pool)
  └─ pool.Release(proj);  // Returns to pool

// Later...
// Pool.Release() deactivates the projectile
// Project is available for reuse
```

**Methods:**
- `Get()` - Retrieve from pool (activate/instantiate)
- `Release(projectile)` - Return to pool (deactivate)
- `Get<T>()` - Generic version

---

### 5. Game Counters

#### TimerCounter
**File:** `Assets/Scripts/Core/Components/Counters/TimerCounter.cs`

**Features:**
- Countdown timer from limit (from PlayerPrefs `TIMER_LIMIT`)
- Displays MM:SS format (e.g., "05:30")
- `HasTimerReachedZero` flag for game over check
- Coroutine-based tick every frame

**Lifecycle:**
```
Start()
  └─ Read TIMER_LIMIT from PlayerPrefs
  └─ Initialize CurrentTime = limit

StartTimer()
  └─ Begin coroutine countdown

OnUpdateState (GameplayState)
  ├─ If HasTimerReachedZero == true
  └─ Transition to GameOverState

ResetTimer()
  └─ Set CurrentTime back to limit (used in StartState)
```

**Time Format:**
```csharp
string timeStr = $"{minutes:00}:{seconds:00}";  // "05:30"
```

---

#### ScoreCounter
**File:** `Assets/Scripts/Core/Components/Counters/ScoreCounter.cs`

**Features:**
- Static Score property (global access)
- Increments on enemy death
- Resets on game start
- Updates UI TextMeshPro fields

**Increment Trigger:**
```
Enemy Dies (Health.OnDie)
  ↓
ScoreCounter.IncrementScore()
  ├─ Score += 10 (or configurable)
  └─ Update all UI text fields
```

---

### 6. Audio System

**File:** `Assets/Scripts/Core/Components/Audio/UIAudioManagerBase.cs`

**Sound Effects:**

| Sound | Trigger | Purpose |
|-------|---------|---------|
| ButtonClick | Menu button click | UI feedback |
| CloseSound | Close UI panel | Dismiss feedback |
| SliderSound | Slider adjustment | Settings feedback |

**Methods:**
```csharp
PlayButtonClick()     // Play button press sound
PlayCloseSound()      // Play close/dismiss sound
PlaySliderSound()     // Play adjustment sound
```

**Usage Pattern:**
```csharp
// In UI button click handler
UIAudioManagerBase.PlayButtonClick();
// Navigate or execute action
```

---

### 7. UI Management

#### HealthUIManager
**Purpose:** Display health as bar (Image.fillAmount).

**Mechanism:**
```csharp
Update()
  └─ fillAmount = _health.GetHealthPercentage() / 100f
  // Syncs bar width to health %
```

**Visual Feedback:**
- Bar color: Green (healthy) → Red (critical) [via SpriteChangerByHealth]
- Width: 0% (dead) to 100% (max health)

#### SpriteChangerByHealth
**File:** `Assets/Scripts/Core/Components/2DComponents/SpriteChangerByHealth.cs`

**Health State Sprites:**

| Health % | State | Sprite | Purpose |
|----------|-------|--------|---------|
| 80-100 | HEALTHY | Intact ship | No damage visible |
| 50-80 | DAMAGED | Cracked ship | Light damage |
| 1-49 | CRITICAL | Burning ship | Heavy damage |
| ≤0 | DESTROYED | Wreck | Dead/destroyed |

**Update Logic:**
```csharp
Update()
  ├─ healthPercent = health.GetHealthPercentage()
  ├─ Determine state (HEALTHY/DAMAGED/CRITICAL/DESTROYED)
  └─ _spriteRenderer.sprite = stateSpriteMap[state]
```

**Visual Progression:** Player/enemy ships show increasing damage as health drops.

---

## Data Flow Diagrams

### Combat Flow

```
┌─────────────────────────────────────────┐
│   Player Press Left Mouse (Fire)        │
└────────────────┬────────────────────────┘
                 │
                 ▼
         ┌───────────────┐
         │ PlayerAttack  │
         │ FireCannon()  │
         └───────┬───────┘
                 │
        ┌────────┴─────────┐
        │                  │
        ▼                  ▼
   Single Mode       Multiple Mode
   (1 Cannon)        (All Cannons)
        │                  │
        └────────┬─────────┘
                 │
                 ▼
    ┌─────────────────────────────┐
    │ CannonBallPool.Get()        │
    │ Retrieve/Create projectile  │
    └────────────┬────────────────┘
                 │
                 ▼
    ┌─────────────────────────────┐
    │ CannonBall.Fire()           │
    │ • Position at cannon        │
    │ • Velocity = speed forward  │
    │ • Enable collision          │
    └────────────┬────────────────┘
                 │
        ┌────────┴──────────────┐
        │ (Each Frame)          │
        ▼                       ▼
    Move Forward         Check Collision
    velocity * Time              │
                                 ├─ No hit
                                 │  └─ Continue
                                 │
                                 ├─ Hit enemy/obstacle
                                 │  ▼
                                 │  OnTriggerEnter2D()
                                 │  │
                                 │  ├─ Get DamageGateway
                                 │  ├─ ApplyDamageOnHealth()
                                 │  │
                                 │  ▼
                                 │  Health.DecreaseHealth()
                                 │  │
                                 │  ├─ CurrentHealth -= damage
                                 │  ├─ VisualDamageFeedback
                                 │  │  (Red flash, VFX)
                                 │  │
                                 │  ├─ CurrentHealth <= 0?
                                 │  │  ├─ YES: IsAlive=false
                                 │  │  │       OnDie.Invoke()
                                 │  │  └─ Death effects
                                 │  │
                                 │  └─ Despawn projectile
                                 │
                                 └─ Time expired
                                    └─ Auto-despawn
                                       └─ BackToPool()
```

### Game State Transitions

```
                    ┌──────────────┐
                    │  Start Game  │
                    │  (Main Menu) │
                    └──────┬───────┘
                           │
                           ▼
                    ┌──────────────────┐
                    │  StartState      │
                    │  • Reset Player  │
                    │  • Reset Score   │
                    │  • Start Timer   │
                    └──────┬───────────┘
                           │
                           ▼
                    ┌──────────────────────┐
        ┌──────────▶│  GameplayState       │
        │           │  • Spawn Enemies    │
        │           │  • Listen Input     │
        │           │  • Check End        │
        │           └────┬────────┬───────┘
        │                │        │
        │     ESC Pressed│        │ PlayerHealth≤0
        │                │        │ OR Timer=0
        │                ▼        ▼
        │           ┌──────────────────┐
        │           │  PauseState      │
        │           │  • Pause Menu    │
        │           │  • Time.scale=0  │
        │           └────┬─────────────┘
        │                │
        │         ESC to Resume
        └────────────────┘
                         │
                         ▼
                    ┌──────────────────┐
                    │  GameOverState   │
                    │  • Show Score    │
                    │  • Restart/Exit  │
                    └──────┬───────────┘
                           │
            ┌──────────────┴──────────────┐
            │                             │
     Restart Button              Exit Button
            │                             │
            ▼                             ▼
       Load Scene            Return to Main Menu
       (repeats cycle)
```

---

## Development Guide

### Adding a New Enemy Type

**Steps:**

1. **Create Movement Class** (inherit from `EnemyShipMovement`)
   ```csharp
   namespace Assets.Scripts.Enemies.Ship.Minion
   {
       public class NewEnemyMovement : EnemyShipMovement
       {
           private float _speed = 3f;
           
           public override void Update()
           {
               if (Target == null) return;
               // Custom movement logic
           }
       }
   }
   ```

2. **Create Health Class** (inherit from `Health`)
   ```csharp
   public class NewEnemyHealth : Health
   {
       protected override void DecreaseHealth(float damageAmount)
       {
           base.DecreaseHealth(damageAmount);
           // Custom death effects
       }
   }
   ```

3. **Create Prefab**
   - Add SpriteRenderer with enemy sprite
   - Add Rigidbody2D (gravity=0, collision=true)
   - Add CircleCollider2D (is trigger for detection)
   - Add TargetNearbyDetector
   - Add NewEnemyMovement
   - Add NewEnemyHealth

4. **Register with Spawner**
   - Add prefab to `EnemySpawner._targetDetectors` list in inspector
   - Spawner randomly selects from available types

5. **Configure AI (if needed)**
   - Attack behavior → Create custom attack script
   - Subscribe to `TargetNearbyDetector.OnTargetNearby` event
   - Trigger attacks in response

---

### Adding a New Game State

**Steps:**

1. **Create State Class** (inherit from `GameplayConcreteState`)
   ```csharp
   namespace Assets.Scripts.GameManager.StateMachine.States
   {
       public class NewGameState : GameplayConcreteState
       {
           public NewGameState(GameplayStateMachine gameSM, /*dependencies*/)
           {
               _gameSM = gameSM;
               // Store dependencies
           }
           
           public override void OnStateEnter()
           {
               // Initialize state
           }
           
           public override void OnUpdateState()
           {
               // State logic each frame
           }
       }
   }
   ```

2. **Register with State Machine**
   - Add property to `GameplayStateMachine`
   - Instantiate in `InitStates()` method
   - Pass all required dependencies

3. **Add Transitions**
   - Call `SwitchState()` from other states to transition
   - Update state diagram documentation

---

### Implementing a New Mechanic

**Common Pattern:**

1. **Define Component**
   ```csharp
   [RequireComponent(typeof(Rigidbody2D))]
   public class NewMechanic : MonoBehaviour
   {
       [SerializeField] private float _parameter = 1f;
       
       public UnityEvent<float> OnValueChanged;
       
       public void UpdateValue(float newValue)
       {
           _parameter = newValue;
           OnValueChanged.Invoke(_parameter);
       }
   }
   ```

2. **Connect to State Machine**
   - Pass component reference to required states
   - Call methods from `OnStateEnter()` or `OnUpdateState()`

3. **Integrate with UI**
   - Create UI Manager that listens to component events
   - Update TextMeshPro/Image fields on event

4. **Add Settings (if configurable)**
   - Store in PlayerPrefs with key
   - Read in initialization
   - Expose slider in settings menu

---

### Debugging Tips

**Common Issues:**

| Issue | Cause | Solution |
|-------|-------|----------|
| Enemies not spawning | EnemySpawner not started | Check StartState calls `StartSpawning()` |
| Player can't move | Input not bound | Verify InputActions.inputactions file exists |
| Projectiles don't damage | DamageGateway missing | Add to enemy prefab |
| UI not updating | Event not connected | Check UnityEvent subscriptions in inspector |
| Game time not counting | TimerCounter not started | Verify StartState initializes counter |

**Useful Console Commands:**
```csharp
// Set game timer limit (minutes)
PlayerPrefs.SetFloat("TIMER_LIMIT", 10f);

// Set enemy spawn interval (seconds)
PlayerPrefs.SetFloat("ENEMEIS_SPAWN_INTERVAL", 1.5f);

// View current score
Debug.Log(ScoreCounter.Score);
```

---

## Key Integration Points

### Player ↔ Enemy
- **Target Reference:** TargetNearbyDetector → Player detection → Enemy AI activation
- **Collision:** Rigidbody2D collision layer → DamageGateway → PlayerHealth
- **Visibility:** Renderer.isVisible check → Spawn triggers

### State Machine ↔ All Systems
- **Dependencies:** Passed via constructor to each state
- **Control:** States call methods on components (Start/Stop)
- **Events:** Listen to UnityEvents for status changes

### Projectile ↔ Targets
- **Pooling:** CannonBallPool manages lifecycle
- **Collision:** OnTriggerEnter2D → DamageGateway → Health.DecreaseHealth()
- **Cleanup:** Auto-despawn via coroutine or time limit

### UI ↔ Game Logic
- **Updates:** Listen to UnityEvents from Health, Counters, PlayerAttack
- **Input:** Settings saved to PlayerPrefs → Loaded on startup
- **Display:** Real-time sync with component state

---

## Performance Considerations

1. **Object Pooling**
   - Max 20 projectiles in pool
   - Reduces GC allocations during combat
   - Pool auto-grows up to max size

2. **Time.timeScale**
   - Pause state sets Time.timeScale = 0
   - Affects all physics & animations automatically
   - Resume sets back to 1

3. **Sprite Swapping**
   - SpriteChangerByHealth updates sprite based on health %
   - No instantiation, only reference swap
   - Cached state to avoid redundant updates

4. **Enemy Spawning**
   - Configurable interval (1-10 seconds)
   - Limits active enemies at any time
   - Despawn off-screen enemies (optional optimization)

---

## Conclusion

Thugs of Caribbean uses a clean, modular architecture centered on a **state machine** for game flow and **component-based design** for gameplay mechanics. The system is designed to be easily extended with new enemy types, game states, and mechanics while maintaining clear separation of concerns.

Key strengths:
- ✅ Explicit state transitions (no hidden dependencies)
- ✅ Reusable component system (Health, Damage, Audio, UI)
- ✅ Event-driven communication (loose coupling)
- ✅ Configuration-driven settings (PlayerPrefs)
- ✅ Efficient object pooling for performance

This documentation should serve as a reference for understanding, maintaining, and extending the game systems.
