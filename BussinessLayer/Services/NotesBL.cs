using System;
using System.Collections.Generic;
using System.Text;
using BusinessManager.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;

namespace BusinessManager.Services
{
    public class NotesBL : INotesBL
    {
        readonly INotesRL noteRL;
        public NotesBL(INotesRL _noteRL)
        {
            this.noteRL = _noteRL;
        }
        public IEnumerable<Note> GetAll(long getId)
        {
            try
            {
                return this.noteRL.GetAll();
            }
            catch (Exception a)
            {
                throw a;
            }
        }
        public IEnumerable<Note> GetNote(long Id)
        {
            try
            {

                return this.noteRL.GetNote(Id);
            }
            catch (Exception a)
            {

                throw a;

            }

        }
        public Note Get(long id)
        {


            try
            {
                return this.noteRL.Get(id);
            }
            catch (Exception a)
            {

                throw a;
            }
        }

        public bool PostNote(RegisterNotes note)
        {

           
            try
            {
                return this.noteRL.PostNote(note);
            }
            catch (Exception a) 
            {
                throw a;
            } 
        }
        public bool Put(Note note, Note entity)
        {
            try
            {
                return this.noteRL.Put(note, entity);
            }
            catch(Exception a)
            {
                throw a;
            }
        }
        public bool Archive(Note note)
        {
           
            try
            {
                return this.noteRL.Archive(note);
            }
            catch (Exception a)
            { 
                throw a;
            }
        }

        public bool UnArchive(Note note)
        {
            try
            {
                return this.noteRL.UnArchive(note);
            }
            catch (Exception a)
            {
                throw a;
            }
            
        }
        public IEnumerable<Note> Trash(Note note)
        {
            try
            {
                return this.noteRL.Trash(note);
            }
            catch (Exception a)
            {
                throw a;
            }
        }

        public IEnumerable<Note> Restore(Note note)
        {
           
            try
            {
                return this.noteRL.Trash(note);
            }
            catch (Exception a)
            {
                throw a;
            }
        }
        public void Delete(Note note)
        {
            try
            {
                this.noteRL.Delete(note);
            }
            catch (Exception a)
            {
                throw a;
            }
        }
    }
}
