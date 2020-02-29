using mTransport.Methodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mTransport.Models
{
    public partial class AffectationVehicule : ICRUD
    {
        public void Delete()
        {
            //using (DB db = new DB())
            //{
            //    var d = db.AffectationVehicules.SingleOrDefault(i => i.Id == Id);
            //    if (d != null)
            //    {
            //        db.AffectationVehicules.Remove(d);
            //        db.SaveChanges();
            //    }
            //}
        }

        public void Insert()
        {
            using (DB db = new DB())
            {
                db.AffectationVehicules.Add(this);
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
        public static List<AffectationVehicule> getAll()
        {
            using (DB db = new DB())
            {
                var l = db.AffectationVehicules.Where(i => i.Supprime == false).ToList();
                foreach (var item in l)
                {
                    item.Chauffeur = Chauffeur.getUnChauffeur(item.IdChauffeur);
                    item.Vehicule = Vehicule.getVehicule(item.IdVehicule);
                }
                return l;
            }
        }

        public static AffectationVehicule getAffectationVehicule(int Id)
        {
            using (DB db = new DB())
            {
                AffectationVehicule c = new AffectationVehicule();
                c = db.AffectationVehicules.SingleOrDefault(i => i.Id == Id);
                return c;
            }
        }
    }
}
