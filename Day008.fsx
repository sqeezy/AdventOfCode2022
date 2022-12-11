// https://adventofcode.com/2022/day/8

open System
open System.Collections.Generic
open System.Collections.Generic
open System.Collections.Generic


let path = $@"{__SOURCE_DIRECTORY__}\Day008.txt"
let lines = System.IO.File.ReadAllLines path

let testLines = 
    @"$30373
25512
65332
33549
35390".Split('\n')

let inline charToInt c = int c - int '0'

let map = Array.mapi (fun row line -> line |> Seq.mapi (fun column c -> ((row, column), charToInt c)))
          >> Seq.concat
          >> dict
          >> Dictionary

let isVisible map (tree : KeyValuePair<(int * int), int>) =
    let height = tree.Value
    let position = tree.Key
    match position with
    | (0, _) | (_, 0) -> true
    | _ -> false

let partOne = ignore

let partTwo = ignore

let testInput =
    @"    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2"

lines |> partOne
testLines |> partOne

lines |> partTwo
testLines |> partTwo
testLines |> map |> Seq.map isVisible