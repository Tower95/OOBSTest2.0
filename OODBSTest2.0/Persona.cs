using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODBSTest2._0
{
    class Persona
    {

        public string id;
        private string _Nombre;

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        private string _Apellidos;

        public string Apellidos
        {
            get { return _Apellidos; }
            set { _Apellidos = value; }
        }

        private int _edad;

        public int Edad
        {
            get { return _edad; }
            set { _edad = value; }
        }
        public string Amigo1 { get; set; }
        public string Amigo2 { get; set; }

        public List<Persona> _Amigos = new List<Persona>();

        public List<Persona> Amigos()
        {
            return _Amigos;
        }

        public void AgregarAmigo(Persona amigo)
        {
            _Amigos.Add(amigo);
        }
    }
}
