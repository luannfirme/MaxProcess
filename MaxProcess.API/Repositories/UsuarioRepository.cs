using JwtAuthApi.Data;
using JwtAuthApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JwtAuthApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Usuario> GetAll() => _context.Usuarios.ToList();
        public Usuario GetById(Guid id) => _context.Usuarios.Find(id);
        public Usuario GetByLogin(string login) => _context.Usuarios.FirstOrDefault(u => u.Login == login);
        public void Create(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public void Update(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var usuario = GetById(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                _context.SaveChanges();
            }
        }

        public bool ExistsByLoginOrEmail(string login, string email)
        {
            return _context.Usuarios.Any(u => u.Login == login || u.Email == email);
        }
    }
}
