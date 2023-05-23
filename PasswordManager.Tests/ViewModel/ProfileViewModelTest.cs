using System.Windows.Input;
using AutoFixture.AutoNSubstitute;
using AutoFixture.NUnit3;
using Microsoft.Maui.Controls.Internals;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using PasswordManager.Model.DB.Schema;
using PasswordManager.Services;
using PasswordManager.Tests.TestAttributes;
using PasswordManager.ViewModel;

namespace PasswordManager.Tests.ViewModel
{
    [TestFixture]
    public class ProfileViewModelTest
    {
        [Test, AutoNSubstituteData]
        public void PageTitleMatchesProfileUsernameTest(IToastService toastService, IClipboard clipboard)
        {
            ProfileViewModel viewModel = new(toastService, clipboard);

            const string profileUsername = "TestUsername";

            ProfileInfo profile = new()
            {
                Username = profileUsername
            };

            viewModel.ProfileInfo = profile;

            string pageTitle = viewModel.PageTitle;
            
            Assert.That(pageTitle, Is.EqualTo($"Profile {profile.Username}"));
        }


        [Test, AutoNSubstituteData]
        public void CopyButtonShowsToastTest(IToastService toastService, IClipboard clipboard)
        {
            ProfileViewModel viewModel = new(toastService, clipboard);

            const string copiedText = "TestCopyText";

            ICommand command = viewModel.CopyToClipboardCommand;

            Assert.DoesNotThrow(() => command.Execute(copiedText));

            clipboard.Received(1).SetTextAsync(copiedText);
            toastService.Received(1).Make(Arg.Any<string>());
        }
    }
}
