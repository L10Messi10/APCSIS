using APCSIS.Includes;

namespace APCSIS.Pages;

public partial class UploadImagePage : ContentPage
{
    private readonly CloudinaryService _cloudinaryService = new();

    public UploadImagePage()
    {
        InitializeComponent();
    }

    private async void OnUploadClicked(object sender, EventArgs e)
    {
        try
        {
            var file = await FilePicker.Default.PickAsync(
                new PickOptions
                {
                    PickerTitle = "Select an image",
                    FileTypes = FilePickerFileType.Images,
                }
            );

            if (file == null)
                return;
            var imageUrl = await _cloudinaryService.UploadImage(file);
            UploadedImage.Source = ImageSource.FromUri(new Uri(imageUrl));
            ImageUrlLabel.Text = imageUrl;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}
