using APCSIS.Models;

namespace APCSIS.Pages;

public partial class SignUp : ContentPage
{
	Users users = new();
	public SignUp()
	{
		InitializeComponent();
	}

    private async void btnregister_Clicked(object sender, EventArgs e)
    {

		await users.AddUser(txtfname.Text, txtlname.Text, txtmname.Text, txtemail.Text, txtpassword.Text);
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}