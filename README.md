# `breacher`
A puzzle solver for the [Cyberpunk 2077 breacher minigame](https://www.rockpapershotgun.com/2020/12/14/cyberpunk-2077-hacking-minigame-breach-protocol-explained/).

![Dotnet Test](https://github.com/kyle-rader/breacher/workflows/Dotnet%20Test/badge.svg)

You can now use this CLI to solve Breacher puzzles!

# Install with `dotnet`
1. [Install the dotnet CLI (.NET SDK 5.0.101)](https://dotnet.microsoft.com/download/dotnet/5.0)
2. Install with `dotnet`
   ```
   dotnet tool install --global breacher
   ```
   which installs from https://www.nuget.org/packages/breacher/ 

# Using the tool

## Manual entry on the cli
Enter input **manually** by running the program with no arguments. e.g.
```
$ dist\breacher
You have opted to enter your puzzle manually. Enter your full puzzle input now.
End your input with a single q at the end to finish entry and begin solving.
4

1 2 3
1 2 3
1 2 3

1 1 2
1 2 2
q
Found solution with weight 3 length: 4
[ 1, 1 ] (1)
[ 2, 1 ] (1)
[ 2, 2 ] (2)
[ 1, 2 ] (2)
Solved in 22.69 ms
```

## Input File
Enter input **with an input file**. The input file _should not have_ a `q` at the end.
```
$ dist\breacher input.txt
Reading input from input.txt
Found solution with weight 3 length: 4
[ 1, 1 ] (1)
[ 2, 1 ] (1)
[ 2, 2 ] (2)
[ 1, 2 ] (2)
Solved in 22.69 ms
```

## Piping from `stdin`
Enter input by **pipeing from stdin**. The input file _should not have_ a `q` at the end.
```
$ type input.txt | dist\breacher
Reading input from standard in.
Found solution with weight 3 length: 4
[ 1, 1 ] (1)
[ 2, 1 ] (1)
[ 2, 2 ] (2)
[ 1, 2 ] (2)
Solved in 22.69 ms
```

(The `|` (vertical bar) is what we call the _pipe_ operator.)

# Puzzle Input Format
```html
<buffer-size: single digit>
<blank-line>
<puzzle: one row per line>
<blank-line>
<targets: one per line>
```

For example see the [demo puzzles](./puzzles/).

Puzzle `01.txt` is:
```
7

1c 55 ff bd e9
bd 1c e9 ff e9
55 bd ff 1c 1c
e9 bd 1c 55 55
55 e9 bd 55 ff

e9 55
55 bd e9
ff 1c bd e9
55 1c ff 55
```

# Solution Format
Right now the solution chain is just printed from start to finish, with the coordinates and values. (TBD print as matrix to make it easier to follow).

The coordinates (a.k.a _indeces_) work as follows (using [puzzle 40](./puzzles/40.txt))
```
    1  2  3  4  5  <-- columns
 1  1c bd 55 55 1c
 2  bd 55 1c bd 55
 3  1c 1c 1c e9 55
 4  bd bd 1c bd e9
 5  1c 55 bd 55 1c
 ^
  \
   Rows
```

The output (on Windows) for [puzzle 40](./puzzles/40.txt) looks like:
```
type puzzles\40.txt | dotnet run -p Breacher
Found solution with weight 5 length: 6
 [ 1, 3 ] (55)
 [ 2, 3 ] (1C)
 [ 2, 4 ] (BD)
 [ 1, 4 ] (55)
 [ 1, 2 ] (BD)
 [ 4, 2 ] (BD)
Solved in 26.64 ms
```

# Run/build from source
1. [Install the dotnet CLI (.NET SDK 5.0.101)](https://dotnet.microsoft.com/download/dotnet/5.0)
2. [Install Git](https://git-scm.com/) for your operating system.
3. [Open a terminal](https://www.google.com/search?rlz=1C1GCEA_enUS911US911&sxsrf=ALeKk01gg9j9o5joiNmR79cQ3YfaJC61Jw%3A1608280570266&ei=-mncX4fVD9fL-gSu4bKgBw&q=how+to+open+a+terminal&oq=how+to+open+a+terminal&gs_lcp=CgZwc3ktYWIQAzIECCMQJzIKCAAQyQMQFBCHAjICCAAyAggAMgIIADICCAAyAggAMgIIADICCAAyAggAOgQIABBHOggIABCxAxCDAToLCC4QsQMQxwEQowI6BAguEEM6BQgAELEDOgQIABBDOggILhCxAxCDAToHCAAQyQMQQzoCCC46CAgAEMkDEJECOgUIABCRAjoHCAAQFBCHAlDOYFidcmDndWgAcAJ4AYABUIgBygiSAQIyMpgBAKABAaoBB2d3cy13aXrIAQjAAQE&sclient=psy-ab&ved=0ahUKEwiHutuAkNftAhXXpZ4KHa6wDHQQ4dUDCA0&uact=5) and clone this repo.
   ```
   git clone https://github.com/kyle-rader/breacher.git
   ```
4. `cd` into the repo.
5. Compile the program
   ```
   publish.cmd
   ```
   This will make a folder called `dist` in which you will find the self-contained executable.
   You can then run it via
   ```
   dist\breacher.exe
   ```

# Future Work
* Print the solution visually to make it easier to follow.
* Build GUI to allow pasting screen shots and parse puzzle input using ML for ultimate laziness.

# Closing Notes
I found this to be one of the more interesting mini game puzzles in a video game. Programming a solution was simply out of interest and for fun to check myself. The mini-game is to emulate "hacking" so I don't feel bad about actually "hacking" the game ;)

Suggestions welcome. Feel free to open a pull request!
