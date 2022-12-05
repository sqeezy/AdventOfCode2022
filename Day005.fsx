// https://adventofcode.com/2022/day/4

open System

let path = $@"{__SOURCE_DIRECTORY__}\Day005.txt"
let lines = System.IO.File.ReadAllLines path
let splitAtNewLine (s:string) = s.Split(Environment.NewLine)

let partOne lines =
    let (upper,lower) = lines |> Array.splitAt (lines |> Array.findIndex((=) ""))
    let initialSetup rawInput =
     let input = Array.truncate ((Array.length rawInput) - 1) rawInput 
     0
    0

let partTwo = ignore

let testInput =  @"    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2"

let testLines = testInput |> splitAtNewLine

printfn $"Part One Test Result: {partOne testLines}"
printfn $"Part One Result: {partOne lines}"

printfn $"Part One Test Result: {partTwo testLines}"
printfn $"Part Two Result: {partTwo lines}"