// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open System.IO
open System.Linq

let ToAbsolutelyPath parent relativePath = Path.Combine(parent, relativePath)

let ToRelativePath (absolutelyPath: string) =
    match absolutelyPath |> File.GetAttributes with
    | _ when Directory.Exists absolutelyPath -> absolutelyPath
    | _ -> Path.GetFileName absolutelyPath

let ToSmallPath (path: string) parent =
    let a =
        (path |> ToRelativePath).ToLower()
        |> ToAbsolutelyPath parent

    a

let ToSmallFileAndDirectory parent path =
    try
        match path |> File.GetAttributes with
        | _ when Directory.Exists path -> Directory.Move(path, ToSmallPath path parent)
        | _ -> File.Move(path, ToSmallPath path parent)
    with :? IOException -> ()

let rec Main parent =
    let files = Directory.GetFiles(parent)

    for file in files do
        file |> ToSmallFileAndDirectory parent

    let dirs = Directory.GetDirectories(parent)

    for dir in dirs do
        dir |> ToSmallFileAndDirectory parent
        Main dir

Main(Console.ReadLine()) |> ignore
