using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;

namespace BusinessManager.Interfaces
{
    public interface INotesBL
    {
        public IEnumerable<Note> GetAll(long getId);
       
        public Note Get(long id);
        bool PostNote(RegisterNotes note);
        public bool Put(Note note, Note entity);
        public bool Archive(Note note);
        public bool UnArchive(Note note);
        public IEnumerable<Note> Trash(Note note);
        public IEnumerable<Note> Restore(Note note);
        public void Delete(Note note);
    }
}
