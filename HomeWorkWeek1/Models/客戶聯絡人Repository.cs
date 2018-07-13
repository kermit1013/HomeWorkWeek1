using System;
using System.Linq;
using System.Collections.Generic;
	
namespace HomeWorkWeek1.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Where(b => b.iscancel == 0);
        }
        public override void Delete(客戶聯絡人 entity)
        {
            entity.iscancel = 1;
        }

        
        public 客戶聯絡人 Find(int id)
        {

            return this.All().FirstOrDefault(p => p.Id == id);

        }

        public IQueryable<客戶聯絡人> Title(string title)
        {
            var Contact = this.All();

            if (!String.IsNullOrEmpty(title))
            {
                Contact = Contact.Where(p => p.職稱.Contains(title));
            }

            return Contact;
        }
    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}