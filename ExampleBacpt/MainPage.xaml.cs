using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ExampleBacpt
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private string dialogResult;
        private async void BtnSearch_OnClick(object sender, RoutedEventArgs e)
        {
            string fileName = txtFileName.Text;
            string content = txtInputContent.Text;
            try
            {
                StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                if (file == null)
                {
                    dialogResult = "File not found";
                }
                else
                {
                    if (content.Length == 0)
                    {
                        dialogResult = "File found but text not found";
                    }
                    else
                    {
                        string txt = await FileIO.ReadTextAsync(file);
                        if (txt.Contains(content))
                        {
                            dialogResult = "File found and text found";
                        }
                        else
                        {
                            dialogResult = "File found but text not found";
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                dialogResult = "File not found";
            }

            MessageDialog();
        }

        private async void MessageDialog()
        {
            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = "",
                Content = dialogResult,
                CloseButtonText = "OK"
            };

            ContentDialogResult result = await noWifiDialog.ShowAsync();
        }

    }
}
