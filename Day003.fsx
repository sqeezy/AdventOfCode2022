// https://adventofcode.com/2022/day/1

open System

let prioFromLower (c: char) = c |> int |> (+) (-96)
let prioFromUpper (c: char) = c |> int |> (+) (-38)

let prio c =
    if Char.IsLower c then
        prioFromLower c
    else
        prioFromUpper c

let intersect lists =
    match lists with
    | head :: tail -> 
        tail
        |> Set.ofSeq
        |> Seq.map Set.ofSeq
        |> Seq.fold (fun remain set -> Set.intersect remain set ) head
    | [] -> Set.empty

let splitLine (s: string) =
    let mid = s.Length / 2
    s 
    |> Seq.toArray 
    |> Array.splitAt mid
    |> fun (a, b) -> [a;b]

let path = $@"{__SOURCE_DIRECTORY__}\Day003.txt"
let lines = System.IO.File.ReadAllLines path

let partOne =
    List.ofArray
    >> List.map splitLine
    >> List.map (List.map Set.ofSeq)
    >> List.map intersect
    >> List.map List.ofSeq
    >> List.map List.head
    >> List.map prio
    >> List.sum

let partTwo =
    List.ofArray
    >> List.chunkBySize 3
    >> List.map (List.map Set.ofSeq)
    >> List.map intersect
    >> List.map(List.ofSeq >> List.head >> prio)
    >> List.sum




let testLines =  @"vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw".Split(Environment.NewLine)

printfn "Part One Test Result: %A" (partOne testLines)
printfn "Part One Result: %A" (partOne lines)

printfn "Part Two Result: %A" (partTwo lines)
