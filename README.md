# Death by Nighttime

Death by Nighttime is a third-person survival action game developed in Unity, inspired by asymmetrical survival horror games such as Dead by Daylight. The game focuses on exploration, enemy encounters, item interaction and survival-based gameplay.

The player explores a dark environment, collects useful items, avoids or fights enemies, and tries to survive while completing the level objectives.

## Features

- Developed a third-person player controller with camera-relative movement and smooth character rotation.
- Implemented Rigidbody-based movement for physics-friendly player control.
- Created a terrain-based map using Unity Terrain, with placed assets and environment elements.
- Added reusable prefabs for gameplay objects, items and enemies.
- Implemented an enemy spawning system, with enemies appearing at random positions around the player.
- Created basic enemy AI behavior, where enemies detect the player and move toward them when the player enters their range.
- Added combat and survival elements, including enemy encounters and player interaction with usable items.
- Implemented a pickup object system for collecting items from the scene.
- Added interactive items such as an axe and a potion.
- Created an inventory system for managing collected items.
- Added item usage logic, allowing the player to use collected objects during gameplay.
- Implemented gameplay progression through objectives and survival-based level flow.
- Added win and lose conditions to complete the core gameplay loop.
- Used Unity prefabs, colliders, triggers and scripts to connect the main gameplay systems.
- Improved the scene setup, object placement and gameplay flow to create a complete playable experience.

## Gameplay Overview

The game is built around exploration and survival. The player controls a third-person character and moves through a dark environment while enemies spawn around the map. When the player gets too close, enemies start chasing them.

To survive, the player can collect items, use them through the inventory system and interact with objects placed in the level. The gameplay combines movement, enemy avoidance, item collection and simple combat mechanics.

## Technologies Used

- Unity
- C#
- Rigidbody-based movement
- Unity Terrain
- Prefabs
- Colliders and triggers
- Basic enemy AI
- Enemy spawning system
- Pickup and inventory system
- Gameplay scripting

## Main Systems Implemented

### Player Controller

The player movement is controlled through a third-person movement script. Movement is relative to the camera direction, and the character model rotates toward the direction of movement. The controller uses Rigidbody physics instead of directly modifying the transform.

### Enemy System

Enemies spawn in random positions near the player. Each enemy has a detection range and starts moving toward the player when the player is close enough. This creates pressure and encourages the player to keep moving, explore carefully and use available items.

### Item and Inventory System

The game includes collectible items such as an axe and a potion. Items can be picked up from the scene and stored in the inventory. The system was designed to support survival gameplay and player interaction with objects.

### Level and Environment

The map was created using Unity Terrain and populated with assets and prefabs. The level is designed to support exploration, enemy encounters and survival-based objectives.

## Status

The game is finalized as a playable student project. It includes the main gameplay loop, player movement, enemy spawning, enemy behavior, item pickup, inventory functionality, level environment and win/lose conditions.
