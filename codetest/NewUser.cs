
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace codetest
{
    [Activity(Label = "NewUser")]
    public class NewUser : Activity
    {
        EditText _username, _password;
        Button _btnSave;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.add_user);

            _username = (Android.Widget.EditText)FindViewById(Resource.Id.edittextUserName);
            _password = (Android.Widget.EditText)FindViewById(Resource.Id.edittextPassword);
            _btnSave = (Android.Widget.Button)FindViewById(Resource.Id.buttonSave);
            _btnSave.Click += _btnSave_Click;

        }

        void _btnSave_Click(object sender, EventArgs e)
        {
            if(ValidateInputs())
            {
                Intent returnIntent = new Intent();
                returnIntent.PutExtra("username", _username.Text);
                SetResult(Result.Ok, returnIntent);
                Finish();
            }
        }

        private bool ValidateInputs()
        {

            if(String.IsNullOrEmpty(_username.Text) || String.IsNullOrEmpty(_password.Text))
            {
                Toast.MakeText(this, "Username and Password cannot be blank!", ToastLength.Short).Show();
                return false;
            } 
            else if(_password.Text.Length < 5 || _password.Text.Length > 12)
            {
                Toast.MakeText(this, "Password should be between 5 and 12 characters in length!", ToastLength.Short).Show();
                return false;
            }
            else if(!Regex.IsMatch(_password.Text, @"^[a-zA-Z0-9]+$"))
            {
                Toast.MakeText(this, "Password must contain Letters and Numbers only!", ToastLength.Short).Show();
                return false;
            }
            else if(!Regex.IsMatch(_password.Text.ToLower(), @"[a-z].*\d|\d.*[a-z]"))
            {
                Toast.MakeText(this, "Password must contain atleast one Albhabet and one Numberic Digit!", ToastLength.Short).Show();
                return false;
            }
            else if(isRepeatExists(_password.Text.ToCharArray()))
            {
                Toast.MakeText(this, "Password contains repeated sequence of charecters!", ToastLength.Short).Show();
                return false;
            }
            return true;
        }

        private bool isRepeatExists(char[] str)
        {
            HashSet<char> h = new HashSet<char>();
  
            for (int i = 0; i <= str.Length - 1; i++)
            {
                char c = str[i];
 
                if (h.Contains(c))
                {
                    return true;
                }
                else  
                {
                    h.Add(c);
                }
            }

            return false;
        }
    }
}
