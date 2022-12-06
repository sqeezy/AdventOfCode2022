// https://adventofcode.com/2022/day/4

open System
open System.Collections.Generic

type Command = { Amount: int; From: int; To: int }

let path = $@"{__SOURCE_DIRECTORY__}\Day005.txt"
let lines = System.IO.File.ReadAllLines path

let initialSetup (rawInput: string array) =
    let looseLastLine input =
        input |> Seq.truncate ((Seq.length input) - 1)

    let input = rawInput |> looseLastLine

    let onlyUpperCharsWithColumnIndex =
        Seq.mapi (fun i c -> (c, i))
        >> Seq.filter (fun (c: char, _) -> (int c) >= 65 && (int c) <= 90)

    input
    |> Seq.map onlyUpperCharsWithColumnIndex
    |> Seq.collect (id)
    |> Seq.groupBy snd
    |> Seq.sortBy fst
    |> Seq.map snd
    |> Seq.map (Seq.map (fst) >> Seq.rev >> Stack<char>)
    |> List

let commandList (rawInput: string array) =
    let parseCommandLine (s: string) =
        s.Split ' '
        |> fun (s: string []) ->
            { Amount = (s.[1] |> Int32.Parse)
              From = (s.[3] |> Int32.Parse)
              To = (s.[5] |> Int32.Parse) }

    rawInput
    |> Seq.skip 1
    |> Seq.map parseCommandLine
    |> List.ofSeq

let moveOneByOne (setup: List<Stack<char>>) command =
    if (command.From > setup.Count || command.To > setup.Count)
    then setup
    else
        let f = setup.[command.From - 1]
        let t = setup.[command.To - 1]
        for _ in [ 1 .. command.Amount ] do
            let (success,char) = f.TryPop()
            if success then t.Push char
        setup

let moveAllTogether (setup: List<Stack<char>>) command =
    if (command.From > setup.Count || command.To > setup.Count)
    then setup
    else
        let f = setup.[command.From - 1]
        let t = setup.[command.To - 1]
        let mutable toPush = []
        for _ in [ 1 .. command.Amount ] do
            let (success,char) = f.TryPop()
            if success then (toPush <- char :: toPush)
        toPush |> List.iter t.Push
        setup

let apply folder (initialSetup: List<Stack<char>>, commands: Command list) =
    commands |> List.fold folder initialSetup

let part lines folder =
    let (upper, lower) =
        lines
        |> Array.splitAt (lines |> Array.findIndex ((=) ""))

    (upper |> initialSetup, lower |> commandList)
    |> apply folder
    |> List.ofSeq
    |> List.map List.ofSeq
    |> List.map List.head

let partOne lines =
    part lines moveOneByOne

let partTwo lines =
    part lines moveAllTogether

let testInput =
    @"    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2"

let testLines = testInput.Split("\n")

lines |> partOne
testLines |> partOne

lines |> partTwo
testLines |> partTwo
