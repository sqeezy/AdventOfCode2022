// https://adventofcode.com/2022/day/4
// https://fsharpforfunandprofit.com/posts/recursive-types-and-folds/#basic-recursive-type
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


type Tree =
    | File of (string * int)
    | Folder of (string * Tree seq)

module Tree =
    let rec cata fLeaf fNode tree =
        let recurse = cata fLeaf fNode
        match tree with
        | File leafInfo ->
            fLeaf leafInfo
        | Folder (nodeInfo,subtrees) ->
            fNode nodeInfo (subtrees |> Seq.map recurse)

    let rec fold fLeaf fNode acc tree =
        let recurse = fold fLeaf fNode
        match tree with
        | File leafInfo ->
            fLeaf acc leafInfo
        | Folder (nodeInfo,subtrees) ->
            // determine the local accumulator at this level
            let localAccum = fNode acc nodeInfo
            // thread the local accumulator through all the subitems using Seq.fold
            let finalAccum = subtrees |> Seq.fold recurse localAccum
            // ... and return it
            finalAccum

let smallTree = Folder("/", seq {File("boot.txt", 100); Folder("usr", seq {File("mng.txt", 150)})})

Tree.cata 
    (fun (name, size) -> (name, size))
    (fun name sub -> (name, Seq.sum (sub|>Seq.map snd)))
     smallTree

let partOne = ignore
let partTwo = ignore
