using NHibernate;
using PayCore_HW4.Context.Abstract;
using PayCore_HW4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayCore_HW4.Context.Concrete
{
    public class ContainerMapperSession :IMapperSession<Container> // interface implement edilmiştir
    {
        private readonly ISession session; // Nhibernate kütüphanesinden gelir
        private ITransaction transaction; // Nhibernate kütüphanesinden gelir
        public ContainerMapperSession(ISession session)
        {
            this.session = session; //kurucu fonksiyonda kapsülleme yapılmıştır
        }
        public IQueryable<Container> Entities => session.Query<Container>();

        public void BeginTranstaction() 
        {
            transaction = session.BeginTransaction();
        }

        public void CloseTransaction() 
        {
            if (transaction != null)
            {
                transaction.Dispose();
                transaction = null;
            }
        }

        public void Commit() 
        {
            transaction.Commit();
        }

        public void Delete(Container entity) 
        {
            session.Delete(entity);
        }

        public void Rollback() 
        {
            transaction.Rollback();
        }

        public void Save(Container entity) 
        {
            session.Save(entity);
        }
        public void Update(Container entity) 
        {
            session.Update(entity);
        }

    }
}
