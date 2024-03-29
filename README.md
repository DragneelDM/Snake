# Snake Game Readme

Welcome to the Snake game! This project aims to recreate the classic Nokia Snake game with additional features and enhancements using Unity.

## Core Snake Game Functionality

- The snake moves in all four directions: up, down, left, and right.
- Screen wrapping is implemented for all directions, ensuring the snake can traverse seamlessly across the edges of the screen.

## Instructions

- The snake dies upon biting itself.
- The snake grows in length after consuming food.
- snake wins after reaching max size.

## Power-Ups

Three power-ups are implemented:

1. **Shield**: Provides temporary invincibility to the snake.
2. **Score Boost**: Doubles the score points earned for each mass-gainer food consumed.
3. **Speed Up**: Increases the snake's movement speed.

**Cooldown**: Each power-up has a 3-second cooldown period, with flexible cooldown times.

**Power-Up Spawning**: Random power-ups spawn at variable time intervals.

## Foods

Two types of food are available:

1. **Mass Gainer**: Increases the snake's length upon consumption.
2. **Mass Burner**: Decreases the snake's length upon consumption.

**Flexibility**: The increase/decrease in snake length is adjustable.

**Food Spawning**: Random foods spawn at variable time intervals. Mass Burner food is not placed if the snake's size is already low.

## Co-Op

A two-player mode is available:

- Player 1 controls the snake using WASD keys.
- Player 2 controls the snake using arrow keys.
- If one snake bites the other, the bitten snake dies.

## Scoring

- Mass Gainer food increases the score.
- Mass Burner food decreases the score.

## UI

Basic UI elements are included:

- Death, win, and score displays.
- Lobby UI for the game.
- Pause/resume, restart, and quit buttons.

Thank you for playing the Snake game! Enjoy the nostalgia with modern enhancements. 😊🐍
