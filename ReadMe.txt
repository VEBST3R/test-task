# Unity Developer Test Assignment

Hi! Here is my implementation of the test task. I've set up the basic gameplay mechanics and UI flow as requested, plus added a few extra polish details.

## What's included:
- **Gameplay:** Left/Right movement and jumping. I also implemented "Mario-style" jump physics (the character falls faster after reaching the peak of the jump or if the jump button is released early) to make the controls feel snappy.
- **Collectibles:** Coins rotate over time and play a sound effect when picked up. I used `AudioSource.PlayClipAtPoint` so the sound plays fully even though the coin GameObject is destroyed immediately.
- **UI:** A simple coin counter in the top-left corner using TextMeshPro.
- **Start Menu:** A clean menu with a background, title, Play, and Quit buttons. Pressing Play triggers a smooth fade transition using an Animator before loading the game scene.
- **Extra UI Flow:** In the game scene, there is a "Back" button. When clicked, it disables the camera follow script, smoothly lerps the camera back to the center (0,0), and triggers an exit transition animation before loading the menu.
- **Performance:** Set `Application.targetFrameRate = 160` for a smooth experience.

## Unique Feature Implementation
For the unique feature, I chose to implement a **Color change when jumping**, as well as audio feedback.

**How it works (Components used):**
- **Color Change:** In the `PlayerController.cs` script, I keep a reference to the player's `SpriteRenderer`. Inside the `Jump()` method, the script sets `spriteRenderer.color` to a designated `jumpColor`. Inside the `CheckGrounded()` logic (which uses `Rigidbody2D.GetContacts()` to accurately detect floor collisions), it resets the color back to `originalColor` as soon as the player lands. *(Note: This feature is fully implemented in the code, but the colors are currently left at their default white values in the Inspector because the game uses a custom sprite asset that doesn't need tinting).*
- **Audio:** I used a standard `AudioSource` on the player for the jump sound. For the collectibles, I used `AudioSource.PlayClipAtPoint()` — this is a great Unity feature that spawns a temporary audio object to play the clip, preventing the sound from being cut off when the coin's `Destroy(gameObject)` is called.
- **Death System:** Falling into a pit triggers a death animation, disables input, and automatically reloads the scene after 1.5 seconds.

Thanks for reviewing my project!
