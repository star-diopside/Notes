using Notes.Web.Models;
using System.Collections.Generic;

namespace Notes.Web.Services
{
    public class NoteService : INoteService
    {
        public IEnumerable<Note> SelectAll() => new[] {
            new Note() { Id = 1, Content = "First Contents" },
            new Note() { Id = 2, Content = "Second Contents" }
        };
    }
}
