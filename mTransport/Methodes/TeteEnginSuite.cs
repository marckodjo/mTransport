using mTransport.Methodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mTransport.Models
{
    public partial class TeteEngin : ICRUD
    {
        public string matriculeModele
        {
            get
            {
                return Matricule + "/" + Modele;
            }
        }

        public void Delete()
        {
            Update();
        }

        public void Insert()
        {
            using (DB db = new DB())
            {
                db.TeteEngins.Add(this);
                db.SaveChanges();
            }          
        }

        public void Update()
        {
            using (DB db = new DB())
            {
                db.Entry(this).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

        }

        //Méthode ne necessitant pas la création d'un objet est a déclarée static
        public static List<TeteEngin> getAll()
        {
            using (DB db = new DB())
            {
                var l = db.TeteEngins.Where(i => i.Supprime == false).ToList();
                return l;
            }               
        }

        public static TeteEngin getTeteEngin(int Id)
        {
            using (DB db = new DB())
            {
                TeteEngin c = new TeteEngin();
                c = db.TeteEngins.SingleOrDefault(i => i.Id == Id);
                return c;
            }
        }
    }
}
