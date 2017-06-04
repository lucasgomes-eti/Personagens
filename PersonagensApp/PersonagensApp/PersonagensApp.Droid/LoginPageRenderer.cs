using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content.PM;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PersonagensApp.Views;
using Xamarin.Auth;
using Xamarin.Forms.Platform.Android;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using PersonagensApp.Models;
using Newtonsoft.Json;

[assembly: ExportRenderer(typeof(LoginPage), typeof(PersonagensApp.Droid.LoginPageRenderer))]

namespace PersonagensApp.Droid
{
    public class LoginPageRenderer : PageRenderer
    {
        public LoginPageRenderer()
        {
            var activity = this.Context as Activity;

            var auth = new OAuth2Authenticator(
                clientId: "109789466280527", // your OAuth2 client id
                scope: "", // the scopes for the particular API you're accessing, delimited by "+" symbols
                authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
                redirectUrl: new Uri("http://www.facebook.com/connect/login_success.html"));

            auth.Completed += async (sender, eventArgs) =>
            {
                if (eventArgs.IsAuthenticated)
                {
                    var accessToken = eventArgs.Account.Properties["access_token"].ToString();
                    var expiresIn = Convert.ToDouble(eventArgs.Account.Properties["expires_in"]);
                    var expiryDate = DateTime.Now + TimeSpan.FromSeconds(expiresIn);

                    var request = new OAuth2Request("GET", new Uri("https://graph.facebook.com/me?fields=name,picture,cover,gender,email"), null, eventArgs.Account);
                    var response = await request.GetResponseAsync();
                    var FbUSer = JObject.Parse(response.GetResponseText());

                    var id = FbUSer["id"].ToString().Replace("\"", "");
                    var name = FbUSer["name"].ToString().Replace("\"", "");
                    var picture = FbUSer["picture"]["data"]["url"];
                    var cover = FbUSer["cover"]["source"];
                    //var birthday = FbUSer["birthday"].ToString();
                    var gender = FbUSer["gender"].ToString();
                    var email = FbUSer["email"].ToString();

                    //var _picture = JsonConvert.SerializeObject(picture);
                    //var _cover = JsonConvert.SerializeObject(cover);

                    Usuario user = new Usuario();

                    user.Nome = name;
                    user.ProfileFotoUrl = picture.ToString();
                    user.CoverFotoUrl = cover.ToString();
                    //user.Idade = birthday;
                    user.Genero = gender;
                    user.Email = email;

                    await App.NavigateToProfile(user);
                }

                else
                {
                    return;
                }

                //AccountStore.Create(Context).Save(eventArgs.Account, "Facebook");
            };

            activity.StartActivity(auth.GetUI(activity));
        }
    }
}