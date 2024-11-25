using Firebase.Database;
using CommunityToolkit.Maui.Core;
using Font = Microsoft.Maui.Font;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APCSIS.Includes
{
    public class PublicVariables
    {
        public static FirebaseClient client = new FirebaseClient("https://apcsis-e5354-default-rtdb.asia-southeast1.firebasedatabase.app/");
        public static CancellationTokenSource cancellationTokenSource = new();
        public static string student_key, _id, first_name, last_name, address, con_num;



        public static SnackbarOptions SnackBarError = new()
        {
            BackgroundColor = Colors.DarkRed,
            TextColor = Colors.White,
            CornerRadius = new CornerRadius(10),
            Font = Font.SystemFontOfSize(12),
            ActionButtonFont = Font.SystemFontOfSize(12),
            CharacterSpacing = 0
        };

        public static SnackbarOptions SnackBarWarning = new()
        {
            BackgroundColor = Colors.DarkOrange,
            TextColor = Colors.White,
            CornerRadius = new CornerRadius(10),
            Font = Font.SystemFontOfSize(12),
            ActionButtonFont = Font.SystemFontOfSize(12),
            CharacterSpacing = 0
        };

        public static SnackbarOptions SnackBarSuccess = new()
        {
            BackgroundColor = Colors.ForestGreen,
            TextColor = Colors.White,
            CornerRadius = new CornerRadius(10),
            Font = Font.SystemFontOfSize(12),
            ActionButtonFont = Font.SystemFontOfSize(12),
            CharacterSpacing = 0
        };
    }
}
