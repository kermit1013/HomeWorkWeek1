using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Dynamic;

namespace HomeWorkWeek1.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(c => c.iscancel == 0);
        }


        public override void Delete(客戶資料 entity)
        {
            entity.iscancel = 1;
        }

        public IQueryable<客戶資料> Searchname(string keyword)
        {
            var client = this.All();

            if (!String.IsNullOrEmpty(keyword))
            {
                client = client.Where(p => p.客戶名稱.Contains(keyword));
            }

            return client;
        }
        public IQueryable<客戶資料> Category(string Cate)
        {
            var client = this.All();

            if (!String.IsNullOrEmpty(Cate))
            {
                client = client.Where(p => p.category.Contains(Cate));
            }

            return client;
        }


        public IQueryable<客戶資料> Sortby(string col,string sort)
        {
            var client = this.All();

            if (!String.IsNullOrEmpty(col)&& !String.IsNullOrEmpty(sort))
            {
                client = client.Where(p => p.iscancel==0).OrderBy(string.Format("{0} {1}", col, sort));
                
            }

            return client;
        }

        public 客戶資料 Find(int id)
        {
            return this.All().FirstOrDefault(f => f.Id == id);
        }

    }

    public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}