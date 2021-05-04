open System
open System.IO

let ToAbsolutelyPath parent relativePath = Path.Combine(parent, relativePath)

let ToRelativePath (absolutelyPath: string) = Path.GetFileName absolutelyPath

let ToSmallPath (path: string) parent =
    let large = (path |> ToRelativePath)
    let small = large.ToLower()

    if not (large.Equals(small)) then
        printfn "%s -> %s" large small

    small |> ToAbsolutelyPath parent

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
