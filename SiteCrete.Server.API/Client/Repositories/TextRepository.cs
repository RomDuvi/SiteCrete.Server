using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using SiteCrete.Server.API.Client.Interfaces;
using SiteCrete.Server.API.Client.Models;

namespace SiteCrete.Server.API.Client.Repositories
{
    public class TextRepository : ITextRepository
    {
        private readonly string resourcesPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Texts");
        private readonly IConfiguration _configuration;

        public TextRepository(IConfiguration config)
        {
            _configuration = config;
        }

        string ITextRepository.GetTextById(string textId, string lang)
        {
            return File.ReadAllText(Path.Combine(resourcesPath, lang, textId));
        }

        string ITextRepository.SaveText(TextModel text)
        {
            File.WriteAllText(Path.Combine(resourcesPath, text.Lang, text.TextId), text.Text);
            return text.Text;
        }
    }
}