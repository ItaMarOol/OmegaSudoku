# Itamar's Omega Sudoku solver
Welcome to Itamar's Omega Sudoku solver!  
This Sudoku solver is able to solve every 1x1 - 25x25 sudoku board (that is a perfect square) fast.  
It also able to detect invalid boards and unsolvable boards.

## Features
- Solve sudoku boards through CLI or file input
- Support for boards from 1x1 - 25x25 (perfect squares only)
- Fast solving using multiple solving strategies
- Detection of invalid and unsolvable boards



## Solving Algorithm

This solver uses a brute-force approach optimized with Sudoku heuristics to improve efficiency and get a solution faster.

### Heuristics
To optimize the board solving process, this solver is using a few basic but efficient sudoku heuristics (tactics):
- **Basic possibilities elimination:** If a number already appears in a row, column, or box, then that number cannot be a possibility for any empty cell in the same row, column, or box.
- **Naked singles:** If a cell has only one possible number, it must be that number.

- **Hidden singles:** If a number can only go in one specific cell within a row, column, or box, it must be placed there.

- **Naked pairs:** If two cells in a row, column, or box can only contain the same two numbers, other numbers in those cells are eliminated.

- **Hidden pairs:** If two numbers only appear in two specific cells within a row, column, or box, then other possibilities in those cells are eliminated.

- **Minimum possibilities selection:** When brute-forcing, select the empty cell with the minimum possible values to try.



## How to use the solver
#### **Download and open the project:**
Clone the project to your local machine:
```bash
  git clone https://github.com/ItaMarOol/OmegaSudoku
  ```
- Open visual studio (in versions 2019-2022) 
- Click `File` → `Open` → `Project/Solution...`
- Navigate to the cloned project folder and open the .sln (solution) file.
- Ensure that .NET 8.0 is selected as the target framework.

#### **Build and Run the program (in Visual Studio):**
- Make sure the project is set as the Startup Project
- Click `Run` or press `F5` to start in Debug mode (for better results, run on `Release` mode).

After running the program, you should see a menu with a few different options. Select your wanted action and follow the instructions.


![Image](https://github.com/user-attachments/assets/126b558a-9c74-49cd-9738-eabc95e865f9)

After attempting to solve, the program will show the initial board, the solved board (or an indicative error if the input is invalid or the board in unsolvable) and the solving attempt runtime.


![Image](https://github.com/user-attachments/assets/681669b8-7c57-41a9-b083-e9055ae109ea)



## Running Tests

There are many tests for: 
 - Regular sudoku puzzles in different sizes and difficulties
 - Special sudoku puzzles (empty / unsolvable)
 - Sudoku board validations
 - User input validations


To run the tests, click `Test` → `Run All Tests` 
or press `Ctrl+R, A`


![Image](https://github.com/user-attachments/assets/b10bd68b-2c6b-4bec-a469-05f123311ce6)








