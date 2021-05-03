// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open System.IO
open System.Linq

let targetPath = Console.ReadLine()

let ToAbsolutelyPath relativePath = Path.Combine(targetPath, relativePath)

let ToRelativePath (absolutelyPath: string) =
    let parentPathLength = targetPath.Length

    match absolutelyPath |> File.GetAttributes with
    | _ when Directory.Exists absolutelyPath -> Path.GetDirectoryName absolutelyPath
    | _ -> Path.GetFileName absolutelyPath

let ToSmallPath (path: string) =
    let relativePath = path |> ToRelativePath
    let newPath = relativePath.ToLower()
    let newAbsolutelyPath = ToAbsolutelyPath newPath
    newAbsolutelyPath

let ToSmallFileAndDirectory path =
    match path |> File.GetAttributes with
    | _ when Directory.Exists path -> Directory.Move(path, ToSmallPath path)
    | _ -> File.Move(path, ToSmallPath path)

let Main () =
    let files = Directory.GetFiles(targetPath)

    for file in files do
        file |> ToSmallFileAndDirectory

Main() |> ignore
ignore (Console.ReadKey())
