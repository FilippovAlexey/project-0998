﻿

#pragma checksum "D:\Pract\AniTube\AniTube\AniTube.Windows\MainView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "128F79A3FE1BC4D80D29261EC1D0B3E6"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AniTube
{
    partial class MainView : global::Windows.UI.Xaml.Controls.Page
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::AniTube.ViewModels.ItemClickedConverter ItemClickedConverter; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.GridView gridViewPlaylists; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.Primitives.Popup popupPlaylist; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.Button buttonSearch; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.Primitives.Popup popupGenres; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.TextBlock textBlockTitle; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.Image image; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.TextBlock textBlockOtherNames; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.TextBlock textBlockdescription; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.Button buttonHD; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.Button buttonSD; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.Button buttonToFavorites; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.Button buttonOutFromFavorites; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private bool _contentLoaded;

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent()
        {
            if (_contentLoaded)
                return;

            _contentLoaded = true;
            global::Windows.UI.Xaml.Application.LoadComponent(this, new global::System.Uri("ms-appx:///MainView.xaml"), global::Windows.UI.Xaml.Controls.Primitives.ComponentResourceLocation.Application);
 
            ItemClickedConverter = (global::AniTube.ViewModels.ItemClickedConverter)this.FindName("ItemClickedConverter");
            gridViewPlaylists = (global::Windows.UI.Xaml.Controls.GridView)this.FindName("gridViewPlaylists");
            popupPlaylist = (global::Windows.UI.Xaml.Controls.Primitives.Popup)this.FindName("popupPlaylist");
            buttonSearch = (global::Windows.UI.Xaml.Controls.Button)this.FindName("buttonSearch");
            popupGenres = (global::Windows.UI.Xaml.Controls.Primitives.Popup)this.FindName("popupGenres");
            textBlockTitle = (global::Windows.UI.Xaml.Controls.TextBlock)this.FindName("textBlockTitle");
            image = (global::Windows.UI.Xaml.Controls.Image)this.FindName("image");
            textBlockOtherNames = (global::Windows.UI.Xaml.Controls.TextBlock)this.FindName("textBlockOtherNames");
            textBlockdescription = (global::Windows.UI.Xaml.Controls.TextBlock)this.FindName("textBlockdescription");
            buttonHD = (global::Windows.UI.Xaml.Controls.Button)this.FindName("buttonHD");
            buttonSD = (global::Windows.UI.Xaml.Controls.Button)this.FindName("buttonSD");
            buttonToFavorites = (global::Windows.UI.Xaml.Controls.Button)this.FindName("buttonToFavorites");
            buttonOutFromFavorites = (global::Windows.UI.Xaml.Controls.Button)this.FindName("buttonOutFromFavorites");
        }
    }
}



