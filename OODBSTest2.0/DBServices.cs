using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client.Documents;
using Raven.Client.Documents.Queries;
using Raven.Client.Documents.Commands;
using System.Windows.Forms;

namespace OODBSTest2._0
{
    class DBServices
    {
        public DocumentStore documentStore;

        private static readonly Lazy<IDocumentStore> LazyStore =
        new Lazy<IDocumentStore>(() =>
        {
            var store = new DocumentStore
            {
                Urls = new[] { "http://localhost:8081" },
                Database = "Personas"
            };

            return store.Initialize();
        });

        public static IDocumentStore Store => LazyStore.Value;
        public void AbrirDB()
        {

        }

        public void CerrarDB()
        {

        }

        public void BorrarDB()
        {
            var session = Store.OpenSession();
            var Personas = session.Query<Persona>().ToList();
            
                foreach (var persona in Personas)
                {
                    session.Delete(persona);
                }

                session.SaveChanges();
            
            
            MessageBox.Show("Datos borrados", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        //prueba
        public void ver()
        {
            //hola manuel
        }
    }
}
