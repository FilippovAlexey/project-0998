using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Navigation;

namespace AniTube.ViewModels
{
    public interface INavigationService
    {
        event NavigatingCancelEventHandler Navigating;
        void Navigate(Type type);
        void Navigate(Type type, object parameter);
        void Navigate(string type);
        void Navigate(string type, object parameter);
        void GoBack();
    }
}
