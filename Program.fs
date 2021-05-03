// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open System.IO

let targetPath = Console.ReadLine()

let ToAbsolutelyPath relativePath = Path.Combine(targetPath, relativePath)

let ToRelativePath (absolutelyPath: string) =
    let parentPathLength = targetPath.Length

    let relativePath =
        absolutelyPath.[(parentPathLength + 1)..(absolutelyPath.Length - 1)]

    relativePath

let ToSmallPath (path: string) =
    let relativePath = path |> ToRelativePath
    let newPath = relativePath.ToLower()
    let newAbsolutelyPath = ToAbsolutelyPath newPath
    newAbsolutelyPath

let ToSmallFileAndDirectory path =
    match path |> File.GetAttributes with
    | _ when Directory.Exists path -> Directory.Move(path, ToSmallPath path)
    | _ -> File.Move(path, ToSmallPath path)

ToSmallFileAndDirectory(Console.ReadLine())
ignore (Console.ReadKey())
