using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StartingPCL.News.Services
{
    internal class NewsServiceStatic : INewsService
    {
        public Task<List<Article>> TopStories(TopStoriesCategory category)
        {
            //                var _assembly = Assembly.GetExecutingAssembly();
            var assembly = this.GetType().GetTypeInfo().Assembly;

            //                _imageStream = assembly.GetManifestResourceStream("MyNamespace.MyImage.bmp");
            var textStreamReader = new StreamReader(assembly.GetManifestResourceStream("StartingPCL.Resources.top-stories.json"));
            var json = textStreamReader.ReadToEnd();
            var list = Task.Factory.StartNew(() => JsonConvert.DeserializeObject<List<Article>>(json));
            //                var list = JsonConvert.DeserializeObject<List<Article>>(json);

            return list;
        }
    }
}
