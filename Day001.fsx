// https://adventofcode.com/2022/day/1

open System
open System.IO

let path = $@"{__SOURCE_DIRECTORY__}\Day001.txt"
let lines = File.ReadAllLines path;

type Calories = Calories of int
type ElfPackage = Calories

let initialFoldState = (List.empty,List.Empty)

let parseResult =
    Array.fold 
        (fun (packages : ElfPackage list, currentCalories : int list) line ->
            match line with
            | "" -> match currentCalories with
                    | [] -> (packages, List.empty)
                    | finishedPackage  -> ((Calories (finishedPackage |> List.sum)) :: packages, [])
            | caloriesString -> (packages,  Int32.Parse(caloriesString)::currentCalories))
        (initialFoldState)
        lines

let (packages, _) = parseResult

let elvesWithCalories = packages
                            |> List.sortByDescending (fun (calories) -> calories)

let partOneResult = elvesWithCalories |> List.head

let partTwoResult = elvesWithCalories |> List.take 3 |> List.sumBy (fun (Calories cals) -> cals)

// find the calorie richtest elf
printf $"{partOneResult}"
printf $"{partTwoResult}"