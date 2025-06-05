# Final Game Showcase & Project Conclusion

After several months of development, I’m excited to present my GMD1 project, **Lil’ Survivors**! A fast-paced rouge-lite game designed for arcade style play. The game is fully functional and works smoothly on PC, as well as the VIA arcade machine.

## Core Gameplay and Features

The game revolves around defeating waves of enemies, earning XP, and leveling up various weapons. The game lasts a maximum of 5 minutes, to fit the arcade setting, but this duration can easily be adjusted through exposed values in the Unity editor.

## Weapons

Players can unlock and upgrade four distinct weapons during gameplay:

- **Bullet**: A standard projectile weapon with increased fire rate and projectile count at higher levels.  
- **Splatter**: An area-of-effect weapon that deals damage over time within a radius.  
- **Shield**: A rotating melee-style weapon that orbits the player, damaging enemies on contact.  
- **Speed Boost**: A utility ability that temporarily boosts player movement speed.

Each weapon uses a ScriptableObject to define how it evolves, including parameters like damage, range, duration, cooldown, and more. This modular setup allows each weapon to scale meaningfully over the course of a match.

## Enemies and Wave Evolution

The game features three main enemy types:

- **Normal Zombies**: Standard movement and health.  
- **Speed Zombies**: Move faster to quickly close the gap.  
- **Tough (Giant) Zombies**: Slower, but with significantly higher health.

Enemy waves evolve over time using a phase-based system defined in a `GamePhaseDataSO`. As each phase ends, the next begins with updated spawn intervals, increasing the game’s difficulty. This setup enables the challenge to scale smoothly across the session.

## Controls and Platform

The game is designed to run on an arcade machine, and while it functions as intended, the controls on the first level feel slightly weird and the enemies can be a little hard to hit. This is an area that could be finetuned in future with updates for improved player experience.

## Final Notes

The game includes all the features outlined in the GDD and is fully playable on the arcade machine. It’s complete, runs smoothly, and has been built in a way that makes it easy to tweak gameplay or add new content like weapons, enemies, or levels in the future.
