using System;
using System.Linq;
using System.Collections.Generic;
	
namespace HomeWorkWeek1.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public override IQueryable<客戶銀行資訊> All()
        {
            return base.All().Where(b => b.iscancel == 0);
        }
        public override void Delete(客戶銀行資訊 entity)
        {
            entity.iscancel = 1;
        }

        public IQueryable<客戶銀行資訊> Searchname(string keyword)
        {
            var bank = this.All();
            if (!String.IsNullOrEmpty(keyword))
            {
                bank.Where(b => b.銀行名稱.Contains(keyword));
            }
            return bank;
        }
        public 客戶銀行資訊 Find(int id)
        {

            return this.All().FirstOrDefault(p => p.Id == id);

        }
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}