# Platformer 2D System
A collision detection and movement system for 2D platformers, the goal of this project is to provide core functionality to help with the creation of custom character controllers.

![demo](https://github.com/samlletas/platformer-2d-system/assets/7089504/531f8023-75cb-47f8-90a4-f3b483c01684)

## Features

1. Supports moving platforms, slopes and one-way slopes/platforms.
1. The system handles all of the collision detection logic so the only thing you need to worry about is updating your character's velocity.
1. Collision detection is made via boxcasts for increased performance.
1. Includes `Runner` and `Jumper` components to speedup creation of character controllers.
1. Physics run in Update rather than FixedUpdate, this provides more responsive controls and removes the [issues introduced by rigidbody interpolation](https://www.zubspace.com/blog/smooth-movement-in-unity#about-rigidbody-interpola).

    > 💡 The disadvantage of this approach is that the variations in deltatime can cause physics to behave differently depending on the framerate, to reduce the impact of this problem the system handles gravity via [verlet integration](https://youtu.be/hG9SzQxaCm8?si=JYAEVKr-H6HCp65N&t=1314).

## Demo

A demo of the example scene can be downloaded from:
[demo-windows.zip](https://github.com/samlletas/platformer-2d-system/files/13049377/demo-windows.zip)

**Controls:**
- Move with arrow keys.
- Jump with space key.
- Fall through one way platforms (green ones) with space+down.

## Setup

1. Download or clone the source code and copy/paste the `Platformer2DSystem` folder into your `Assets` folder.
1. Go to `Edit -> Project Settings -> Physics 2D` and set `Simulation Mode` to `Update`.
1. Go to `Edit -> Project Settings -> Tags and Layers` and add the following tag: `OneWay`.
1. You can now start using the included components in your project. It's recommended to also check the included example scene.

## Usage

- **For characters:** Add the `Actor` component, set the collision mask and update the `velocity` field from your scripts.
    > 💡 Don't forget to set the rigidbody type to `Kinematic`.

- **For ground/walls:** Add a collider and set it's layer.
    > 💡 Tag them with `OneWay` if actors should only collide from above.

- **For moving platforms:** Add the `Solid` component and set the `passengers mask` field to the layers containing the actors you want the platform to interact with.
    > 💡 Tag them with `OneWay` if actors should only collide from above.

## Optional Components

These components are optional, but using them avoids the need to write boilerplate code for common movement logic in platformers.

- **Runner**: Handles horizontal movement and acceleration, you need to call the `Move` and `Stop` method from your scripts.

- **Jumper**: Handles vertical movement, you need to call the `Jump`, `CancelJump` and `JumpDown` methods from your scripts.

- **MotionStats**: Provides time information about an actor's movement (how much time it has been idling, moving, etc).

## Limitations

- Gravity direction is fixed downwards.

- Moving platforms only support box colliders.

- Sliding down from steep slopes is not implemented so it's recommended to avoid slopes above 45 degrees (use straight walls instead).

## References

- https://www.zubspace.com/blog/smooth-movement-in-unity
- https://docs.godotengine.org/en/3.4/classes/class_kinematicbody.html
- https://maddythorson.medium.com/celeste-and-towerfall-physics-d24bd2ae0fc5
- https://github.com/SebLague/2DPlatformer-Tutorial
- https://www.yoyogames.com/en/blog/flynn-advanced-jump-mechanics
- https://www.gamedeveloper.com/design/platformer-controls-how-to-avoid-limpness-and-rigidity-feelings
- https://www.emanueleferonato.com/2012/05/24/the-guide-to-implementing-2d-platformers/
- https://www.youtube.com/watch?v=hG9SzQxaCm8
