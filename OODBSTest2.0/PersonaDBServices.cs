using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client.Documents;
using Raven.Client.Documents.Commands;
using Raven.Client.Documents.Session;

namespace OODBSTest2._0
{
    class PersonaDBServices
    {
        List<Persona> personas = new List<Persona>();
        public void GuardarPersona(Persona persona)
        {
            // Este codigo implementa La conexion con La base de datos RabenDB.
            using (var session = DBServices.Store.OpenSession())
            {

                var newPersona = new Persona
                {
                    Nombre = persona.Nombre,
                    Apellidos = persona.Apellidos,
                    Edad = persona.Edad,
                    _Amigos = persona._Amigos
                };


                session.Store(newPersona);
                persona.id = newPersona.id;
                session.SaveChanges();
            }

        }

        public void GuardarListaPersonas(List<Persona> lista_personas)
        {
            //Este ciclo hakerino guardar una lista de personas DUUUU (billy tone).
            foreach (var persona in lista_personas)
            {
                GuardarPersona(persona);
            }
        }

        public List<Persona> ListaPersonas()
        {
            List<Persona> ListaPersonas = new List<Persona>();


            using (var session = DBServices.Store.OpenSession())
            {

                ListaPersonas = session
                .Query<Persona>()
                .ToList();
            }


            return ListaPersonas;
        }



        public List<Persona> BuscarPersonaPorApellido(string apellido)
        {
            List<Persona> ListaPersonas = new List<Persona>();

            using (var session = DBServices.Store.OpenSession())
            {

                ListaPersonas = session
                .Query<Persona>()
                .Where(x => x.Apellidos == apellido)
                .ToList();

            }
            return ListaPersonas;

        }

        public List<Persona> BuscarPersonaPorEdad(int edad1, int edad2)
        {
            List<Persona> ListaPersonas = new List<Persona>();
            using (var session = DBServices.Store.OpenSession())
            {

                ListaPersonas = session
                .Advanced
                .DocumentQuery<Persona>()
                .Search(x => x.Edad, edad1.ToString())
                .Search(x => x.Edad, edad2.ToString())
                .ToList();

            }

            return ListaPersonas;
        }
        public void OptenerAmigos(Persona persona)
        {
            
           persona.Amigo1 = OptenerID(persona.Amigos()[0]);
           persona.Amigo2 = OptenerID(persona.Amigos()[1]);
        }

        public void UpdatePersona(Persona persona)
        {
            string Id = OptenerID(persona);
            OptenerAmigos(persona);
            using (var session = DBServices.Store.OpenSession())
            {

                var doc = new Persona
                {
                    Nombre = persona.Nombre,
                    Apellidos = persona.Apellidos,
                    Edad = persona.Edad,
                    Amigo1 = persona.Amigo1,
                    Amigo2 = persona.Amigo2


                };

                var docInfo = new DocumentInfo
                {
                    Collection = "Personas"
                };
                var blittableDoc = session.Advanced.EntityToBlittable.ConvertEntityToBlittable(doc, docInfo);
                var command = new PutDocumentCommand(Id, null, blittableDoc);
                session.Advanced.RequestExecutor.Execute(command, session.Advanced.Context);


                session.SaveChanges();
            }
        }
        public string OptenerID(Persona persona)
        {
            string IDPersona ="";
            using (var session = DBServices.Store.OpenSession())
            {
                var allPersonas = session.Query<Persona>().ToArray();
                foreach (var Persona in allPersonas)
                {
                    
                    if (persona.Nombre == Persona.Nombre && persona.Apellidos == Persona.Apellidos && persona.Edad== Persona.Edad)
                    {
                        IDPersona = session
                        .Advanced
                        .GetDocumentId(Persona);
                    }
                }
                
            }
            return IDPersona;
        }
        public List<Persona> UpdateAmigos(List<Persona> lista_personas)
        {
            List<Persona> ListaPersonas = new List<Persona>();
            foreach (var persona in lista_personas)
            {
                

            }
            
            return ListaPersonas;
        }

        public void UpdateListaPersonas(List<Persona> lista_personas)
        {
            foreach (var persona in lista_personas)
            {
                UpdatePersona(persona);
            }
        }
    }
}
