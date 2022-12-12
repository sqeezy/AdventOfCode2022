// https://adventofcode.com/2022/day/8

open System
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

let createForest (lines : string array) : (Dictionary<(int * int), int>)= 
                   lines
                   |> Array.mapi (fun row line -> line |> Seq.mapi (fun column c -> ((row, column), charToInt c)))
                   |> Seq.concat
                   |> dict
                   |> Dictionary

let isVisible (map:Dictionary<(int * int), int>) (tree : KeyValuePair<(int * int), int>) =
    let maxRow = map.Keys |> Seq.map (fun (row, _) -> row) |> Seq.max
    let maxCol = map.Keys |> Seq.map (fun (_, col) -> col) |> Seq.max
    let height = tree.Value
    let position = tree.Key
    match position with
    | (0, _) | (_, 0)  -> true
    | (r, c) when r = maxRow || c = maxCol -> true
    | (row, col) -> 
        let allLeft = [0..row-1] |> List.map (fun r -> map.[(r, col)]) |> List.max
        let allRight = [row+1..maxRow] |> List.map (fun r -> map.[(r, col)]) |> List.max
        let allTop = [0..col-1] |> List.map (fun c -> map.[(row, c)]) |> List.max
        let allBottom = [col+1..maxCol] |> List.map (fun c -> map.[(row, c)]) |> List.max
        [allLeft; allRight; allTop; allBottom] |> List.max |> (<) height
        
let partOne lines =
    let forest = createForest lines
    forest
    |> Seq.map (isVisible forest)
    |> Seq.filter id
    |> Seq.length


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