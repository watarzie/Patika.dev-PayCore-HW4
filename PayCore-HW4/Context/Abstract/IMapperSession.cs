using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayCore_HW4.Context.Abstract
{
    public interface IMapperSession<T> where T : class, new() // T class ve newlenebilir olmalıdır.Yani veritabanı nesnesi olmalıdır.
    {
        void BeginTranstaction(); // baglantı acar
        void Commit(); // commit eder
        void Rollback(); // baglantı veya işlem gerceklesmez ise geri sarmak icin kullanılan metot
        void CloseTransaction(); //baglantıyı kapatır
        void Save(T entity); // nesneyi veritabanına kaydeder
        void Update(T entity); // nesneyi veritabanında günceller
        void Delete(T entity); // nesneyi veritabanından siler

        IQueryable<T> Entities { get; } // nesneyi veritabanından okumak yani get etmek için kullanılan field


    }
}
