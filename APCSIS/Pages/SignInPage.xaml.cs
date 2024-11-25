using APCSIS.Models;
using CommunityToolkit.Maui.Alerts;
using static Microsoft.Maui.Controls.Application;
using static APCSIS.Includes.PublicVariables;

namespace APCSIS.Pages;

public partial class SignInPage : ContentPage
{
    private Users _users = new();
	public SignInPage()
	{
		InitializeComponent();
        Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
	}

    private async void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
    {
        switch (e.NetworkAccess)
        {
            case NetworkAccess.None:
            case NetworkAccess.Unknown:
            case NetworkAccess.ConstrainedInternet:
            case NetworkAccess.Local:
            {
                const string text = "You're currently offline.";
                var duration = TimeSpan.FromSeconds(3);
                var snackbar = Snackbar.Make(text, null, "Got it!",
                    duration, SnackBarError);
                await snackbar.Show(cancellationTokenSource.Token);
                break;
            }
            case NetworkAccess.Internet:
            {
                const string text = "Your internet connection was restored.";
                var duration = TimeSpan.FromSeconds(3);
                var snackbar = Snackbar.Make(text, null, "Got it!",
                    duration, SnackBarSuccess);
                await snackbar.Show(cancellationTokenSource.Token);
                break;
            }
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private async void btnsignin_Clicked(object sender, EventArgs e)
    {
        if (txtemail.Text == null || txtpassword.Text == null)
        {
            await DisplayAlert("Sign In", "Please enter your email and password!", "Got it!");
        }

        var login = await _users.Login(txtemail.Text, txtpassword.Text);

        if (login != null)
        {
            Current!.MainPage = new AppShell();
        }
        else
        {
            await DisplayAlert("Login", "Email and Password does not match!", "Got it!");
        }





        //Navigate to new Page or screen
        //await Navigation.PushModalAsync(new SignUp());


        //Close newly opened modal page
        //await Navigation.PopModalAsync();




        //This will navigate to new application page instance
        //Current!.MainPage = new AppShell();
    }

    private async void btnsignup_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new SignUp());
    }
}