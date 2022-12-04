// https://adventofcode.com/2022/day/4

open System

let path = $@"{__SOURCE_DIRECTORY__}\Day004.txt"
let lines = System.IO.File.ReadAllLines path
let splitAtNewLine (s:string) = s.Split(Environment.NewLine)
let splitAtComma (s:string) = s.Split(',')
let splitAtDash (s:string) = s.Split('-')

let rangePair (line: string array array) =
    let elfA = line.[0]
    let elfB = line.[1]
    let toInts = Array.map (Int32.Parse) >> (fun arr -> (arr.[0], arr.[1]))
    (toInts elfA, toInts elfB)

let includedInEachOther (a,b) =
    let rightIncludedInLeft left right =
        match left,right with
        | ((aLow, aHigh),(bLow, bHigh)) when (aLow <= bLow) && (aHigh >= bHigh) -> true
        | _ -> false
    rightIncludedInLeft a b || (rightIncludedInLeft b a)

let overlap (a,b) =
    let rightOverlappingLeft left right =
        match left,right with
        | ((aLow, aHigh),(bLow, bHigh)) when (aHigh >= bLow) && (aLow <= bHigh) -> true
        | _ -> false
    rightOverlappingLeft a b || (rightOverlappingLeft b a)

let part overlapCriteria =
    Array.map splitAtComma
    >> Array.map (Array.map splitAtDash)
    >> Array.map rangePair
    >> Array.map overlapCriteria
    >> Array.filter id
    >> Array.length

let partOne = part includedInEachOther
let partTwo = part overlap

let testInput =  @"2-4,6-8
2-3,4-5
5-7,7-9
2-8,3-7
6-6,4-6
2-6,4-8"

let testLines = testInput |> splitAtNewLine

printfn "Part One Test Result: %A" (partOne testLines)
printfn "Part One Result: %A" (partOne lines)

printfn "Part One Test Result: %A" (partTwo testLines)
printfn "Part Two Result: %A" (partTwo lines)