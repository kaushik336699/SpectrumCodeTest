using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace codetest
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        List<String> users;
        ListView listviewUsers;
        ArrayAdapter<String> adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            listviewUsers = (ListView)FindViewById(Resource.Id.listViewUsers);
            users = new List<String>() { "User1", "User2", "User3", "User4", "User5", "User6" };
            adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, users);
            listviewUsers.SetAdapter(adapter);
            adapter.NotifyDataSetChanged();

        }

       


        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_new)
            {
                Intent intent = new Intent(this, typeof(NewUser));
                StartActivityForResult(intent, 101);
            }

            return base.OnOptionsItemSelected(item);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 101)
            {
                if (resultCode == Result.Ok)
                {
                    //here is your result
                    String result = data.GetStringExtra("username");
                    users.Add(result);
                    //adapter.NotifyDataSetChanged();
                    adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, users);
                    listviewUsers.SetAdapter(adapter);
                }
                if (resultCode == Result.Canceled)
                {
                    //Write your code if there's no result
                }
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

