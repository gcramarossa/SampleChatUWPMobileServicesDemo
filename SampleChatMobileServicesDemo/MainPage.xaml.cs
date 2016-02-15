using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.WindowsAzure.MobileServices;
// Il modello di elemento per la pagina vuota è documentato all'indirizzo http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x410

namespace SampleChatMobileServicesDemo
{
    /// <summary>
    /// Pagina vuota che può essere utilizzata autonomamente oppure esplorata all'interno di un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MobileServiceClient _client = new MobileServiceClient("https://provachat.azurewebsites.net");
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void sendText_Click(object sender, RoutedEventArgs e)
        {
            Message message = new Message
            {
                User = username.Text,
                Text = textUser.Text
            };

            messages.Text += username.Text + " : " + textUser.Text + Environment.NewLine;

            _client.GetTable<Message>().InsertAsync(message);
        }

        private async void messages_Loaded(object sender, RoutedEventArgs e)
        {
            List<Message> getMessages = await _client.GetTable<Message>().ToListAsync();
            Message[] savedMessages = getMessages.ToArray();
            foreach (var messageItem in savedMessages)
            {
                messages.Text = messageItem.User + " : " + messageItem.Text + Environment.NewLine;
            }
        }
    }
}
