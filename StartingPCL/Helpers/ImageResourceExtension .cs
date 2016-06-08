using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StartingPCL
{
    [ContentProperty("Source")]
    public class ImageResourceExtension : BindableObject, IMarkupExtension
    {
        private Dictionary<string, ImageSource> Cache = new Dictionary<string, ImageSource>(); 

//        public static readonly BindableProperty SourceProperty = BindableProperty.Create("Source", typeof(string), typeof(ImageResourceExtension));
        public static readonly BindableProperty SourceProperty = BindableProperty.Create<ImageResourceExtension, string>(p => p.Source, default(string));


        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

//        public string Source1 { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
                return null;

            ImageSource value;
            if( Cache.TryGetValue(this.Source, out value) )
                return value;

            // Do your translation lookup here, using whatever method you require
            value = ImageSource.FromResource(Source);
            Cache[this.Source] = value;

            return value;
        }
    }
}