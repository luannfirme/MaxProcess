using JwtAuthApi.Models;
using System;
using System.Collections.Generic;

namespace JwtAuthApi.Repositories
{
    public interface IUsuarioRepository
    {
        IEnumerable<Usuario> GetAll();
        Usuario GetById(Guid id);
        Usuario GetByLogin(string login);
        void Create(Usuario usuario);
        void Update(Usuario usuario);
        void Delete(Guid id);
        bool ExistsByLoginOrEmail(string login, string email);
    }
}
