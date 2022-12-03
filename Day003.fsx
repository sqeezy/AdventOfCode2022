// https://adventofcode.com/2022/day/1

open System

let prioFromLower (c: char) = c |> int |> (+) (-96)
let prioFromUpper (c: char) = c |> int |> (+) (-38)

let prio c =
    if Char.IsLower c then
        prioFromLower c
    else
        prioFromUpper c

let splitLine (s: string) =
    let mid = s.Length / 2
    s |> Seq.toArray |> Array.splitAt mid

let overlap (left, right) =
    Set.intersect (Set.ofSeq left) (Set.ofSeq right)
    |> Set.toList
    |> List.head


let path = $@"{__SOURCE_DIRECTORY__}\Day003.txt"
let lines = System.IO.File.ReadAllLines path

let partOne =
    Array.map splitLine
    >> Array.map overlap
    >> Array.map prio
    >> Array.sum

let testLines =  @"vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw".Split(Environment.NewLine)

printfn "Part One Test Result: %A" (partOne testLines)
printfn "Part One Result: %A" (partOne lines)
