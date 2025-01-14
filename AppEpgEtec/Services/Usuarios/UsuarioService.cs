﻿using AppEpgEtec.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppEpgEtec.Services.Usuarios
{
    public class UsuarioService : Request
    {
        private readonly Request _request;
        private const string ApiUrlBase = "http://AmandaLima.somee.com/RpgApi/Usuarios";
        
        public UsuarioService()
        {
            _request = new Request();
        }
        public async Task<Usuario> PostLoginUsuarioAsync(Usuario u)
        {
            string urlComplementar = "/Autenticar";
            u.Token = await _request.PostReturnStringAsync(ApiUrlBase + urlComplementar, u);
            return u;

        }

    }
}
