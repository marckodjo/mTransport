using mTransport.Methodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mTransport.Models
{
    public partial class TypeDepense : ICRUD
    {
        public void delete()
        {
            //using (DB db = new DB())
            //{
            //    var d = db.TypeDepenses.SingleOrDefault(i => i.Id == Id);
            //    if (d != null)
            //    {
            //        db.TypeDepenses.Remove(d);
            //        db.SaveChanges();
            //    }
            //}
        }

        public void Insert()
        {
            using (DB db = new DB())
            {
                db.TypeDepenses.Add(this);
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
        public static List<TypeDepense> getAll()
        {
            using (DB db = new DB())
            {
                var l = db.TypeDepenses.Where(i => i.Supprime == false).ToList();
                return l;
            }               
        }

        public static TypeDepense getTypeDepense(int Id)
        {
            using (DB db = new DB())
            {
                TypeDepense c = new TypeDepense();
                c = db.TypeDepenses.SingleOrDefault(i => i.Id == Id);
                return c;
            }
        }

        public void Delete()
        {
            using (DB db = new DB())
            {
                db.Entry(this).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
