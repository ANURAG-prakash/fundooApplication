using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;

namespace RepositoryLayer.Interfaces
{
    public interface INotesRL 
    {
        public IEnumerable<Note> GetAll();
        bool PostNote(RegisterNotes note);
        public IEnumerable<Note> GetNote(long Id);
        public Note Get(long id);
        public bool Put(Note note, Note entity);
        public bool Archive(Note note);
        public bool UnArchive(Note note);
        public IEnumerable<Note> Trash(Note note);
        public IEnumerable<Note> Restore(Note note);

        public void Delete(Note note);
    }
}
