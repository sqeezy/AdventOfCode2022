// https://adventofcode.com/2022/day/4
// https://fsharpforfunandprofit.com/posts/recursive-types-and-folds-3b/

open System

let testLines = 
    @"$ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k".Split('\n')

let path = $@"{__SOURCE_DIRECTORY__}\Day007.txt"
let lines = System.IO.File.ReadAllLines path

type Node =
    | File of (string * int)
    | Folder of (string * Node seq)

let partOne = ignore
let partTwo = ignore
