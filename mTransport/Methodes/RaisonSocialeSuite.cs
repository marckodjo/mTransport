using mTransport.Methodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mTransport.Models
{
    public partial class RaisonSociale : ICRUD
    {

        public void Insert()
        {
            using (DB db = new DB())
            {
                db.RaisonSociales.Add(this);
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
        public static List<RaisonSociale> getAll()
        {
            using (DB db = new DB())
            {
                var l = db.RaisonSociales.Where(i => i.Id != 0).ToList();
                return l;
            }
        }

        public static RaisonSociale getRaisonSociale()
        {
            using (DB db = new DB())
            {
                RaisonSociale c = new RaisonSociale();
                c = db.RaisonSociales.SingleOrDefault();
                return c;
            }
        }

        public void Delete()
        {
            Update();
        }
    }
}
