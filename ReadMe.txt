Unity Developer Test Assignment - Implementation Notes

Overview:
This project includes a basic gameplay scene with a 2D player controller, rotating collectibles, a score counter, and a main menu with Play and Quit buttons.

Unique Feature:
I implemented a color change on jumping for the player. When the player jumps, the SpriteRenderer color changes to a jump color (cyan by default), and when they land, it reverts to the original color. I used the following Unity components for this feature:
- SpriteRenderer: To change the visual color of the player character.
- Rigidbody2D / Collider2D: To detect when the player lands back on the ground using OnCollisionEnter2D and normal checks.

Setup Instructions for Reviewer:
1. Ensure your Gameplay Scene has a "Player" GameObject with the tag "Player", a Rigidbody2D, a Collider2D, and the PlayerController script attached.
2. Ensure your Collectibles have a Collider2D set to "Is Trigger" and the Collectible script attached.
3. Add a GameManager GameObject with the GameManager script to the scene and link a TextMeshPro UI text element for the score.
4. Set up your Main Menu Scene with buttons calling PlayGame and QuitGame methods from the MainMenu script, and assign the name of your gameplay scene in the inspector.
5. Add the FadeIn script to a UI Panel with a CanvasGroup to see the fade-in effect on the Main Menu.
