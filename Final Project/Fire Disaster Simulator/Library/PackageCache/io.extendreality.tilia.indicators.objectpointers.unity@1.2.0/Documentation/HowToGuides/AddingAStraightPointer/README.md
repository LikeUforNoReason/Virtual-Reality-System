# Adding A Straight Pointer

> * Level: Beginner
>
> * Reading Time: 5 minutes
>
> * Checked with: Unity 2018.3.14f1

## Introduction

The Straight Pointer prefab casts a straight line from an origin point to the maximum specified length or until the Pointer beam collides with a valid target.

When a Straight Pointer is colliding with a valid GameObject, a Selection Action can occur which emits an event containing data about the GameObject that the Straight Pointer is currently colliding with.

A Straight Pointer is useful for picking out a location with precision, especially when attached to a controller.

We’ll set up a Straight Pointer on a controller that can be activated and deactivated on a button press.

## Prerequisites

* [Add the Tilia.CameraRigs.TrackedAlias.Unity] prefab to the scene Hierarchy.
* [Add the Tilia.Input.UnityInputManager.ButtonAction] prefab to the scene Hierarchy.
* [Install the Tilia.Indicators.ObjectPointers.Unity] package dependency in to your [Unity] project.

## Let's Start

### Step 1

Expand the `Tilia Indicators ObjectPointers Unity` Package directory in the Unity Project window and select then `Packages -> Tilia Indicators ObjectPointers Unity -> Runtime -> Prefabs` directory then drag and drop the `Indicators.ObjectPointers.Straight` prefab into the Hierarchy window.

![Adding Prefab To Scene](assets/images/AddingPrefabToScene.png)

### Step 2

Select the `Indicators.ObjectPointers.Straight` prefab in the Unity Hierarchy and change the `Pointer Facade` component to configure the base functionality of the Pointer.

The `Follow Source` parameter determines what GameObject the Pointer should track in the scene, for example, if we want the Pointer to follow around the Right Controller GameObject then drag and drop the `CameraRigs.TrackedAlias -> Aliases -> RightControllerAlias` GameObject into the `Follow Source` parameter on the `Pointer Facade` component.

![Drag And Drop Right Controller Alias As Pointer Follow Source](assets/images/DragAndDropRightControllerAliasAsPointerFollowSource.png)

### Step 3

The Straight Pointer will be deactivated by default so we need a way of activating and deactivating the Pointer beam. This is done by hooking up a `BooleanAction`, when it emits `true` it will activate the Pointer and when it emits `false` it will deactivate the Pointer.

Any `BooleanAction` can be used to perform the activation/deactivation of the Pointer but in this instance we’re going to use the `Input.UnityInputManager.ButtonAction` that is already in the scene. The existing `BooleanAction` will emit `true` when the `Space` key is pressed and will emit `false` when the `Space` key is released.

Drag and drop the `Input.UnityInputManager.ButtonAction` GameObject into the `Activation Action` parameter on the `Pointer Facade` component.

![Drag And Drop Boolean Action Onto Activation Action](assets/images/DragAndDropBooleanActionOntoActivationAction.png)

### Done

We won’t use any of the other `Pointer Facade` parameters for now as we have all we need for a Straight Pointer to be activated when the `Space` key is pressed and it will point in a straight line in whatever direction the Right Controller is pointing.

Play the Unity scene and press the `Space` key and the Straight Pointer will emit a beam from the controller pointing in the forward direction of the controller. Notice how the Straight Pointer beam has two states when it is activated:

* Valid Collision - The Straight Pointer is colliding with a valid GameObject and displays as a green line.
* Invalid/No Collision - The Straight Pointer is not colliding with any valid GameObject and displays as a red line.

![Straight Pointer Activated In Scene](assets/images/StraightPointerActivatedInScene.png)

[Add the Tilia.CameraRigs.TrackedAlias.Unity]: https://github.com/ExtendRealityLtd/Tilia.CameraRigs.TrackedAlias.Unity/blob/master/Documentation/HowToGuides/AddingATrackedAlias/README.md
[Add the Tilia.Input.UnityInputManager.ButtonAction]: https://github.com/ExtendRealityLtd/Tilia.Input.UnityInputManager/blob/master/Documentation/HowToGuides/UsingTheUnityButtonAction/README.md
[Install the Tilia.Indicators.ObjectPointers.Unity]: ../Installation/README.md
[Unity]: https://unity3d.com/