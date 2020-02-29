using mTransport.Methodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mTransport.Models
{
    public partial class Materiel : ICRUD
    {
        public void Delete()
        {
            //using (DB db = new DB())
            //{
            //    var d = db.Materiels.SingleOrDefault(i => i.Id == Id);
            //    if (d != null)
            //    {
            //        db.Materiels.Remove(d);
            //        db.SaveChanges();
            //    }
            //}
        }

        public void Insert()
        {
            using (DB db = new DB())
            {
                db.Materiels.Add(this);
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
        public static List<Materiel> getAll()
        {
            using (DB db = new DB())
            {
                var l = db.Materiels.Where(i => i.Supprime == false).ToList();
                return l;
            }               
        }

        public static Materiel getMateriel(int Id)
        {
            using (DB db = new DB())
            {
                Materiel c = new Materiel();
                c = db.Materiels.SingleOrDefault(i => i.Id == Id);
                return c;
            }
        }
    }
}
