using mTransport.Methodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mTransport.Models
{
    public partial class Vehicule : ICRUD
    {

        public void Insert()
        {
            using (DB db = new DB())
            {
                db.Vehicules.Add(this);
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
        public static List<Vehicule> getAll()
        {
            using (DB db = new DB())
            {
                List<Vehicule> l = db.Vehicules.Where(i => i.Supprime == false).ToList();
                foreach (var item in l)
                {
                    item.BenneEngin = BenneEngin.getUnBenneEngin(item.IdBenneEngins.Value);
                    item.TeteEngin = TeteEngin.getTeteEngin(item.IdTeteEngins.Value);
                }
                return l;
            }               
        }

        public static Vehicule getVehicule(int Id)
        {
            using (DB db = new DB())
            {
                Vehicule c = new Vehicule();
                c = db.Vehicules.SingleOrDefault(i => i.Id == Id);
                return c;
            }
        }

        public void Delete()
        {
            Update();
        }
    }
}
