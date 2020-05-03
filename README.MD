## CoordinatorLayout.XamarinForms

A custom control for Xamarin.Forms, that shows an expanding top view and a scrollable bottom view. It is a reproduction of Android's CoordinatorLayout - https://developer.android.com/jetpack/androidx/releases/coordinatorlayout

Here's a quick rundown of the control's features:

- Use any view as top, bottom and action view.
- Precisely define how much space each of the view takes, as a proportion of their parent view.
- Enable snapping to the expanded or collapsed state. Define the snapping threshold.
- Overflowing content in the bottom view is automatically scrollable. Enable or disable kinetic scrolling and the drag coefficient. Choose if kinetic scrolling expands the top view or not.
- Receive exact information on the top view's expansion progress and on the bottom view's scrolling progress.

## Installation

| Package                        | Version                                                                                                                                      | Dependencies                                                                                                     |
| ------------------------------ | -------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------- |
| CoordinatorLayout.XamarinForms | [![Nuget](https://img.shields.io/nuget/vpre/CoordinatorLayout.XamarinForms)](https://www.nuget.org/packages/CoordinatorLayout.XamarinForms/) | [![Nuget](https://img.shields.io/badge/Xamarin.Forms-v4.5-green)](https://www.nuget.org/packages/Xamarin.Forms/) |

Add the NuGet package to the shared project (.Net Standard) and to the platform specific projects (e.g. Android and iOS).

## Property reference

| Property                                | What it does                                                                                                                                                                                                                          |
| --------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `ProportionalTopViewHeightMin`          | The minimum height of the top view, as a proportion of the entire coordinator layout. Coerced to [0.0, 1.0] where 0.0 means that the top view can be collapsed completely and 1.0 means that the top view cannot be collapsed at all. |
| `ProportionalTopViewHeightMax`          | The maximum height of the top view, as a proportion of the entire coordinator layout. Coerced to [0.0, 1.0] where 0.0 means that the top view can be collapsed completely and 1.0 means that the top view cannot be collapsed at all. |
| `ProportionalSnapHeight`                | The top view snaps to its collapsed position while smaller than this value or it snaps to its expanded position while it is larger than this value.                                                                                   |
| `ScrollProgress`                        | The bottom view scroll progress. Argument values range from 0.0 (collapsed) to 1.0 (expanded).                                                                                                                                        |
| `ExpansionProgress`                     | The top view expansion progress. Argument values range from 0.0 (collapsed) to 1.0 (expanded).                                                                                                                                        |
| `ShouldExpandFromKineticScroll`         | Whether or not the top view should expand when the bottom view is in a kinetic scroll and it reaches its end.                                                                                                                         |
| `ShouldKineticScroll`                   | Whether or not the bottom view should use kinetic scrolling.                                                                                                                                                                          |
| `ShouldSnap`                            | Whether or not the top view should snap to it collapsed or expanded height.                                                                                                                                                           |
| `KineticScrollDragCoefficient`          | A coefficient controlling the slowing down of a kinetic scroll.                                                                                                                                                                       |
| `ProportionalActionViewContainerHeight` | The proportional height of the ActionView's container.                                                                                                                                                                                |
| `AutohideActionView`                    | Whether or not the ActionView should be hidden automatically or not.                                                                                                                                                                  |
| `TopView`                               | The view to show at the top of the coordinator layout. This view will be expanded and collapsed, according to the properties ProportionalTopViewHeightMin and ProportionalTopViewHeightMax. This property is mandatory.               |
| `BottomView`                            | The view to show at the bottom of the coordinator layout. This view will be scrolled vertically, in case that it doesn't fit in the available space, below the TopView. This property is mandatory.                                   |
| `ActionView`                            | A view to be shown between the top view and bottom view. This view usually contains controls that trigger an action, e.g. a Button.                                                                                                   |
| `ExpansionEventHandler`                 | Event that is raised when the top view expands or collapses. Argument values range from 0.0 (collapsed) to 1.0 (expanded).                                                                                                            |
| `ScrollEventHandler`                    | Event that is raised when the bottom view is scrolled. Argument values range from 0.0 (not scrolled) to 1.0 (fully scrolled).                                                                                                         |

### Note: the action view is hosted in a parent container. In case that your ActionView isn't visible it is likely that the parent container is too small for it. Simply adjust the property ProportionalActionViewContainerHeight to a larger value.


## Sample

There's a nice sample application to get you going.

In the media folder there's a video of the sample project.

![Sample](media/CoordinatorLayout.m4v)

If YouTube is your thing: https://youtu.be/GIpSPpPFXtg

As usual you can do everything you want with my code. Have fun!

Have a question? Tweet at me: https://twitter.com/MunteanMarius
