using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RpgApi.Models
{
    public class Usuario
    {
        //Atalho para propridade (PROP + TAB)
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] FotoPersonagem { get; set; }

        public byte[] Foto { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime? DataAcesso { get; set; } //using System;
        //[Required] //indica que ocampo será not null quando ele existir no banco de dados.
        public string Perfil {get; set;}
        
        public string Email {get; set;}

        [NotMapped] // using System.ComponentModel.DataAnnotations.Schema
        public string PasswordString { get; set; }
        public List<Personagem> Personagens { get; set; }//using System.Collections.Generic;
    }
}