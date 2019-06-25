using System.Collections.Generic;
using SiteCrete.Server.API.Client.Models;

namespace SiteCrete.Server.API.Client.Interfaces
{
    public interface ITextRepository
    {
        string GetTextById(string textId, string lang);
        string SaveText(TextModel text);
    }
}