using Notes.Web.Models;
using System.Collections.Generic;

namespace Notes.Web.Services
{
    public interface INoteService
    {
        IEnumerable<Note> SelectAll();
    }
}
