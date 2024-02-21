// See https://aka.ms/new-console-template for more information
using System;
using System.IO;



string dowloadsFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";

string pdfFolders = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads\\PDFs";
string ImagesFolders = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads\\Images";
string VideosFolders = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads\\Videos";
string ZipFolders = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads\\Zip";
string ExeFolders = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads\\Exe";
string OthersFolders = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads\\Others";



string[] organizeFolders = {pdfFolders, ImagesFolders, VideosFolders, ZipFolders, ExeFolders, OthersFolders};

string[] files = Directory.GetFiles(dowloadsFolder);


string[] allDirectories = Directory.GetDirectories(dowloadsFolder);

foreach (var item in allDirectories)
{
    if (!organizeFolders.Contains(item))
    {
       var dirItem = new DirectoryInfo(item);
       dirItem.Delete(true);
    }
}
Console.WriteLine("Initializing Folder structure creation");


foreach (var item in organizeFolders)
{
    if (!Directory.Exists(item))
    {
        Directory.CreateDirectory(item);
        Console.WriteLine("Folder Created: " + item);
    }
}
Console.WriteLine("Folder structure created ");


foreach (string filePath in files)
{
    switch (Path.GetExtension(filePath))
    {
        case ".pdf":
            File.Move(filePath, pdfFolders + "\\" + Path.GetFileName(filePath));
            break;
        case ".jpg":
        case ".png":
        case ".jpeg":
            File.Move(filePath, ImagesFolders + "\\" + Path.GetFileName(filePath));
            break;
        case ".mp4":
        case ".mkv":
        case ".avi":
            File.Move(filePath, VideosFolders + "\\" + Path.GetFileName(filePath));
            break;
        case ".zip":
        case ".rar":
            File.Move(filePath, ZipFolders + "\\" + Path.GetFileName(filePath));
            break;
        case ".exe":
            File.Move(filePath, ExeFolders + "\\" + Path.GetFileName(filePath));
            break;
        default:
            File.Move(filePath, OthersFolders + "\\" + Path.GetFileName(filePath));
            break;
    }
}

Console.WriteLine("Starting file cleansing from desktop");

foreach (var item in organizeFolders)
{
    string[] filesInFolder = Directory.GetFiles(item);
    foreach (var file in filesInFolder)
    {
        File.Move(file, dowloadsFolder + "\\" + Path.GetFileName(file));
    }
    Directory.Delete(item);
}

