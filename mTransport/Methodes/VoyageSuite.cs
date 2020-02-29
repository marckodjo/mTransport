using mTransport.Methodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mTransport.Models
{
    public partial class Voyage : ICRUD
    {

        public void Insert()
        {
            using (DB db = new DB())
            {
                db.Voyages.Add(this);
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
        public static List<Voyage> getAll()
        {
            using (DB db = new DB())
            {
                var l = db.Voyages.Where(i => i.Supprime == false).ToList();
                foreach (var item in l)
                {
                    item.Vehicule = Vehicule.getVehicule(item.IdVehicule.Value);
                }
                return l;
            }               
        }

        public static Voyage getVoyage(int Id)
        {
            using (DB db = new DB())
            {
                Voyage c = new Voyage();
                c = db.Voyages.SingleOrDefault(i => i.Id == Id);
                return c;
            }
        }

        public void Delete()
        {
            Update();
        }
    }
}
