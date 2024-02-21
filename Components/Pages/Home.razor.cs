using LukeMauiFilePicker;
using Microsoft.AspNetCore.Components;

namespace MauiFilePickerMacOsBug.Components.Pages;
public partial class Home
{
    [Inject] private IFilePickerService LukeFilePickerService { get; set; } = default!;


    private static readonly PickOptions TextFileOptions = new()
    {
        PickerTitle = "Select a TXT file",
        FileTypes = TxtFile,
    };

    private static readonly Dictionary<DevicePlatform, IEnumerable<string>> TextFileTypes =
        new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.iOS, new[] { "public.text" } },
            { DevicePlatform.Android, new[] { "text/plain" } },
            { DevicePlatform.WinUI, new[] { ".txt" } },
            { DevicePlatform.Tizen, new[] { "*/*" } },
            { DevicePlatform.macOS, new[] { "public.text" } },
            { DevicePlatform.MacCatalyst, new[] { "public.text" } }
        };

    private static readonly FilePickerFileType TxtFile = new FilePickerFileType(TextFileTypes);

    private string? _result;

    private async Task PickTextFileNet()
    {
        var pickResult = await FilePicker.PickAsync(TextFileOptions);
        UpdateNetPickResult(pickResult);
    }

    private async Task PickTextFileNetDefault()
    {
        var pickResult = await FilePicker.Default.PickAsync(TextFileOptions);
        UpdateNetPickResult(pickResult);
    }

    private void UpdateNetPickResult(FileResult? fileResult)
    {
        if (fileResult == null)
        {
            _result = "FileResult is NULL";
            return;
        }

        _result = $""""
                   FileName: {fileResult.FileName} / ContentTtype: {fileResult.ContentType} / FullPath: {fileResult.FullPath}
                   """";
    }

    private async Task PickTextFileLuke()
    {
        var pickResult = await LukeFilePickerService.PickFileAsync("Select a text file", TextFileTypes);
        Update3RdPartyPickResult(pickResult);
    }

    private void Update3RdPartyPickResult(IPickFile? fileResult)
    {
        if (fileResult == null)
        {
            _result = "IPickFile is NULL";
            return;
        }

        if (fileResult?.FileResult == null)
        {
            _result = "IPickFile.FileResult is NULL";
            return;
        }

        _result = $""""
                   FileName: {fileResult.FileName} / ContentTtype: {fileResult.FileResult.ContentType} / FullPath: {fileResult.FileResult.FullPath}
                   """";
    }

    private async Task PickTextFileBrighterTools()
    {

    }

}


