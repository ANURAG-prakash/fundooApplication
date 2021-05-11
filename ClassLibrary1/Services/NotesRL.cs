using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer.Services
{
    public class NotesRL : INotesRL
    {
        readonly UserContext _noteContext;
        public NotesRL(UserContext context)
        {
            _noteContext = context;
        }
        public IEnumerable<Note> GetAll()
        {
            try
            {
                return _noteContext.Notes.ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Note> GetNote(long Id)

        {
            try
            {

                return _noteContext.Notes.Where(e => e.UserId == Id && e.IsArchive == false && e.IsTrash == false);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Note Get(long id)
        {
            try
            {
                return this._noteContext.Notes.FirstOrDefault(e => e.NoteId == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool PostNote(RegisterNotes note)
        {
            try
            {

                Note _user = new Note()
                {
                    Title = note.Title,
                    Message = note.Message,
                    Reminder = note.Reminder,
                    Color = note.Color,
                    Image = note.Image,
                    Collaborator = note.Collaborator,
                    IsPin = note.IsPin,
                    IsArchive = note.IsArchive,
                    IsTrash = note.IsTrash,
                    UserId = note.UserId
                };
                _noteContext.Notes.Add(_user);
                int result = _noteContext.SaveChanges();
                if (result <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Put(Note note, Note entity)
        {
            try
            {
                note.Title = entity.Title;
                note.Message = entity.Message;
                note.Reminder = entity.Reminder;
                note.Color = entity.Color;
                note.Image = entity.Image;
                note.Collaborator = entity.Collaborator;
                note.IsPin = entity.IsPin;
                note.IsArchive = entity.IsArchive;
                note.IsTrash = entity.IsTrash;
                note.UserId = entity.UserId;
                int result = _noteContext.SaveChanges();
                if (result <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public bool Archive(Note note)
        {
            try
            {
                note.IsArchive = true;
                int result = _noteContext.SaveChanges();
                if (result <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool UnArchive(Note note)
        {
            try
            {
                note.IsArchive = false;
                int result = _noteContext.SaveChanges();
                if (result <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public IEnumerable<Note> Trash(Note note)
        {
            try
            {
                note.IsTrash = true;
                int result = _noteContext.SaveChanges();

                return _noteContext.Notes.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Note> Restore(Note note)
        {
            try
            { 
                note.IsTrash = false;
                int result = _noteContext.SaveChanges();
                return _noteContext.Notes.ToList();
            }
            catch (Exception ex)
            { 
                throw ex;
            }
        }





        public void Delete(Note note)
        {
            try
            {
                _noteContext.Notes.Remove(note);
                _noteContext.SaveChanges();
            }
            catch (Exception a)
            {
                throw a;
            }
        }
    }
}
