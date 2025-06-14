# Connect4 with Monte Carlo Tree Search (MCTS)

This project is a C# implementation of the classic game **Connect4**, enhanced with an AI opponent using the **Monte Carlo Tree Search (MCTS)** algorithm to make decisions. The difficulty of the AI can be adjusted by changing the number of MCTS iterations.

---

## Table of Contents
- [Project Description](#project-description)
- [Features](#features)
- [Screenshots & Demo](#screenshots--demo)
- [Technical Details](#technical-details)
- [Installation](#installation)
- [Usage](#usage)

---

## Project Description

The game implements Connect4 on a 6x7 board using C# and Windows Forms. The player competes against the computer, whose moves are calculated using Monte Carlo Tree Search (MCTS). The user can adjust the number of simulations per move, which effectively changes the AI's difficulty.

**Objective:** Be the first to connect 4 of your own discs either vertically, horizontally, or diagonally.

If the board is completely filled without any player connecting four discs, the game ends in a draw.

---

## Features

- Adjustable difficulty by changing the number of MCTS simulations.
- User-friendly Windows Forms interface.
- Randomized tie-breaks to avoid deterministic outcomes when multiple moves have the same evaluation.
- Ability to start a new game at any point.
- Visual cues for possible moves, player pieces, and computer pieces.

---

## Screenshots & Demo

### Game Board

![408828935-98877aba-5882-4a6b-8daa-2e0e27df6db0](https://github.com/user-attachments/assets/4d842755-3e9e-4e48-aeed-700264f83033)

### Difficulty Adjustment Menu

![image](https://github.com/user-attachments/assets/4129fcb5-1d4b-47f8-a10a-37ecbc378e41)

### Gameplay Demo

![2025-06-1420-44-32-ezgif com-optimize](https://github.com/user-attachments/assets/5c156728-d63f-43ad-aebb-406d504c15aa)

## Technical Details

### Technologies Used
- C#
- Windows Forms (.NET)

### Key Components
- **Board Class:** Manages the game board state, adding pieces, and checking for winning conditions.
- **MCTS Class:** Implements the Monte Carlo Tree Search algorithm.
- **MainForm & UI:** Handles user input and displays game progress.

---

## Installation

### Option 1: Download pre-built release (recommended)

You can download the latest release directly from GitHub:
➡ [Download Latest Release](https://github.com/AdrianGeorgita/Connect4/releases/latest)

The release includes:
- Executable file
- Required board assets (e.g. `board.png`)

Simply download, extract and run `Connect4.exe`.

### Option 2: Build from source

1. Clone the repository:
```bash
git clone https://github.com/AdrianGeorgita/Connect4.git
```
2. Open the solution in Visual Studio.
3. Build and run the project.

## Usage
- Start the game using the menu option Joc -> Joc Nou.
- Set the difficulty level (number of simulations) from the game menu.
- Click on a column to drop your piece.
- The AI will automatically respond after your move.

### Color Legend
- Yellow: Player pieces
- Red: Computer pieces
- Transparent green: Available moves
