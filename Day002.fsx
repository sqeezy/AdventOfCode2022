// https://adventofcode.com/2022/day/1

open System
open System.IO

let path = $@"{__SOURCE_DIRECTORY__}\Day002.txt"
let lines = File.ReadAllLines path;

type ThemPlay = Rock | Paper | Scissors
type YouPlay = RockOrLoose | PaperOrDraw | ScissorsOrWin
type Outcome = Win | Draw | Loose

let parseLine (s:String) =
    let parts = s.Split(' ')
    let them = 
        match parts.[0] with
        | "A" -> Rock
        | "B" -> Paper
        | "C" -> Scissors
    let you = 
        match parts.[1] with
        | "X" -> RockOrLoose
        | "Y" -> PaperOrDraw
        | "Z" -> ScissorsOrWin
    (them, you)


let getBaseScore = 
    function
    | RockOrLoose -> 1
    | PaperOrDraw -> 2
    | ScissorsOrWin -> 3

let getScoreFromFight =
    function
    | Win -> 6
    | Draw -> 3
    | Loose -> 0

let fight them you =
    match them, you with
    | Rock, RockOrLoose -> Draw
    | Paper, RockOrLoose -> Loose
    | Scissors, RockOrLoose -> Win
    | Rock, PaperOrDraw -> Win
    | Paper, PaperOrDraw -> Draw
    | Scissors, PaperOrDraw -> Loose
    | Rock, ScissorsOrWin -> Loose
    | Paper, ScissorsOrWin -> Win
    | Scissors, ScissorsOrWin -> Draw

let score (them, you) =
    let baseScore = getBaseScore you
    let fightScore = fight them you |> getScoreFromFight
    fightScore + baseScore

let transformToMatch (them, you) =
    let fix =
        match (them, you) with
        | Rock, RockOrLoose -> ScissorsOrWin
        | Paper, RockOrLoose -> RockOrLoose
        | Scissors, RockOrLoose -> PaperOrDraw
        | Rock, PaperOrDraw -> RockOrLoose
        | Paper, PaperOrDraw -> PaperOrDraw
        | Scissors, PaperOrDraw -> ScissorsOrWin
        | Rock, ScissorsOrWin -> PaperOrDraw
        | Paper, ScissorsOrWin -> ScissorsOrWin
        | Scissors, ScissorsOrWin -> RockOrLoose
    (them, fix)


let partOneResult = lines |> Array.map parseLine |> Array.map score |> Array.sum
let partTwoResult = lines |> Array.map parseLine |> Array.map transformToMatch |>  Array.map score |> Array.sum

printfn $"{partOneResult}"
printfn $"{partTwoResult}"