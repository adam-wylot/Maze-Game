# StudentEXE - Console RPG

*Currently in progress...*

### 📌 Project Overview
**StudentEXE** is a console-based RPG dungeon crawler written entirely in **C# (.NET 9.0)**. The game puts the player in the shoes of a Warsaw University of Technology student navigating a procedurally generated maze filled with academic-themed enemies, loot, and interactive mechanics.

This project was built to demonstrate a strong command of Object-Oriented Programming (OOP), C# features, and the practical application of multiple Software Design Patterns in a structured, maintainable architecture.

---

### 🛠️ Technical Stack & Architecture
From a technical perspective, this codebase heavily utilizes design patterns to ensure scalability and clean code separation:

*   **State / Scene Management**: Implemented a stack-based Scene Manager (`IGameScene`) within the main Game class to seamlessly transition between different game states (Map, Combat, Instructions, Game Over) without memory leaks or tight coupling.
*   **Builder & Director Patterns**: Used for procedural dungeon generation (`MapBuilder`, `DungeonDirector`). It supports building complex maps with randomized rooms, central halls, corridors, and scattered items/enemies dynamically.
*   **Visitor Pattern**: Applied to the combat system (`IAttackVisitor`). Different attack types (Normal, Stealth, Magic) calculate damage and defense differently based on the specific weapon type (Heavy, Light, Magic) and player stats, cleanly separating the combat logic from the entity classes.
*   **Decorator Pattern**: Used for the item and weapon modification system. Weapons and items can be dynamically wrapped with modifiers (e.g., `LuckyWeaponModifier`, `CursedWeaponModifier`, `SharpModifier`) that alter their stats and behavior at runtime.
*   **Chain of Responsibility**: Implemented for input handling (`ActionChainNode`). Complex player actions (like moving or picking up items) are evaluated through a chain of conditional checks (e.g., checking if the tile is free, if the inventory is full, or if an enemy is present) before execution.
*   **Custom UI Rendering**: Built a custom console rendering engine (`RendererBase`) that handles absolute positioning, colors, and dynamic grid updates (for stats, logs, and ASCII art) without constant screen flickering.

---

### ✨ Key Features
*   **Procedural Dungeons**: Randomized maps utilizing DFS (Depth-First Search) for corridor generation and dead-end removal.
*   **Turn-Based Combat**: A tactical combat system featuring distinct attack styles and enemies with custom ASCII art (e.g., "Academic Guard", "Hangover", "Homeless").
*   **Inventory Management**: Dual-wielding support (Left/Right Hand), two-handed weapons, and an expandable 9-slot inventory.
*   **Dynamic Stats**: Player attributes (Strength, Agility, Luck, Wisdom, Aggression) directly impact combat effectiveness and scale with equipped items.

---

### 🎮 How to Play

*   **Movement**: `W`, `A`, `S`, `D`
*   **Interaction**: 
    *   `E` - Pick up items
    *   `Q` - Drop items
    *   `I` / `F` - Equip / Dequip items to your hands
*   **Combat**: Automatically triggered by walking into an enemy tile. Choose between attacking, fleeing, and selecting your weapon/attack type.
