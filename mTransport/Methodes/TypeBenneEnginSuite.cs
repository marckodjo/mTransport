using mTransport.Methodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mTransport.Models
{
    public partial class TypeBenneEngin : ICRUD
    {

        public void Insert()
        {
            using (DB db = new DB())
            {
                db.TypeBenneEngins.Add(this);
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
        public static List<TypeBenneEngin> getAll()
        {
            using (DB db = new DB())
            {
                var l = db.TypeBenneEngins.Where(i => i.Supprime == false).ToList();
                return l;
            }               
        }

        public static TypeBenneEngin getUnTypeBenneEngin(int Id)
        {
            using (DB db = new DB())
            {
                TypeBenneEngin c = new TypeBenneEngin();
                c = db.TypeBenneEngins.SingleOrDefault(i => i.Id == Id);
                return c;
            }
        }

        public void Delete()
        {

        }
    }
}
