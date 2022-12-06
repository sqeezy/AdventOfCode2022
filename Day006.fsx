// https://adventofcode.com/2022/day/4

open System

let path = $@"{__SOURCE_DIRECTORY__}\Day006.txt"
let line = System.IO.File.ReadAllLines path |> Seq.head
let firstUniqueSequenceOfLengthIndex (windowSize : int) window = 
    let lengthOfWindow = window |> Seq.map snd |> Seq.distinct |> Seq.length
    match lengthOfWindow with
    | x when x = windowSize  -> Some (window |> Seq.last |> fst |> (+) 1)
    | _ -> None

let lateIndexOfFirstMatch (windowSize : int) : seq<char> -> int = 
    let matcher = firstUniqueSequenceOfLengthIndex windowSize
    Seq.mapi (fun i c -> (i, c))
    >> Seq.windowed windowSize 
    >> Seq.pick matcher

let partOne = lateIndexOfFirstMatch 4
let partTwo = lateIndexOfFirstMatch 14

"bvwbjplbgvbhsrlpgdmjqwftvncz" |> partOne // should be 5
"nppdvjthqldpwncqszvftbrmjlhg" |> partOne // should be 6
line |> partOne // should be 1707

"mjqjpqmgbljsphdztnvjfqwrcgsmlb" |> partTwo // should be 19
"bvwbjplbgvbhsrlpgdmjqwftvncz" |> partTwo // should be 23
line |> partTwo // should be 3697
