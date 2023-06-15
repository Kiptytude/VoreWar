using System.IO;

class SimpleFileLoader
{
    string extension;
    string directory;
    bool editor;
    LoaderType loaderType;

    FileLoaderUI UI;

    public enum LoaderType
    {
        MapEditor,
        PickMap,
        PickSaveForContentSettings
    }

    public SimpleFileLoader(string directory, string extension, FileLoaderUI uI, bool mapEditor, LoaderType loaderType)
    {
        this.loaderType = loaderType;
        this.extension = extension;
        this.directory = directory;
        editor = mapEditor;
        UI = uI;
        BuildFiles();
    }

    private void BuildFiles()
    {
        if (Directory.Exists(directory) == false)
            Directory.CreateDirectory(directory);
        string[] files = Directory.GetFiles(directory);

        foreach (string file in files)
        {
            if (!File.Exists(file)) return;

            if (CompatibleFileExtension(file))
            {
                switch (loaderType)
                {
                    case LoaderType.MapEditor:
                        UI.CreateMapLoadButton(file);
                        break;
                    case LoaderType.PickMap:
                        UI.CreateMapStrategicIntegrateButton(file);
                        break;
                    case LoaderType.PickSaveForContentSettings:
                        UI.CreateGrabContentSettingsButton(file);
                        break;
                }

            }
        }
    }

    public bool CompatibleFileExtension(string file)
    {
        if (extension.Length == 0)
        {
            return true;
        }

        if (file.EndsWith("." + extension))
        {
            return true;
        }

        // Not found, return not compatible
        return false;
    }
}

