using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayCore_HW4.Context.Abstract;
using PayCore_HW4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayCore_HW4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClusterController : ControllerBase
    {
        private readonly IMapperSession<Container> session;

        public ClusterController(IMapperSession<Container> session)
        {
            this.session = session;

        }
        [HttpGet("{vehicleid},{n}")] // vehicleid ile getirilen listeyi n girdisi kadar K-Means algoritmasına göre parçalayan Action Metot
        public IActionResult GetByCluster(int vehicleid, int n)
        {

            List<Container> container = session.Entities.Where(x => x.VehicleId == vehicleid).ToList(); // Vehicleid'ye baglı container listesini getirir
            var random = new Random(5555); // clusterların başlangıc noktalarını rastgele bulmak için random fonksiyonu kullanıldı
            List<List<double>> Kmeans = new List<List<double>>(); // Clusterların başlangıc konumlarını tutmak ıcın kullanılan lıste
            List<int> oldIndexes = new List<int>(); // clusterların başlangıc noktaların unique olması icin kullanılan liste
            for (int i = 0; i < n; i++) // n tane baslangıc noktası bulmak ıcın olusturulan dongu
            {
                while (true) // unique baslangıc noktası bulana kadar donen dongu
                {
                    var index = random.Next(container.Count); // baslangıc noktası ıcın olusturulan rastgele ındex
                    bool statue = oldIndexes.Contains(index); // baslangıc noktasının unıque olup olmadıgın kontrol eden degısken
                    if (statue == false)
                    {
                        // eger index unique ıse Kmeans listesine eklenir.
                        List<double> ag = new List<double>();
                        ag.Add(container[index].Latitude);
                        ag.Add(container[index].Longitude);
                        Kmeans.Add(ag);
                        oldIndexes.Add(index);
                        break;
                    }

                }
            }
            bool ısThat = true; // algoritmanın bıtıp bıtmedigini kontrol eden degısken
            while (true) // algoritma converge edene kadar dönen döngü
            {
                int meansIndex = 0; // anlık clusteringin indexini tutmak icin kullanılan degısken
                List<List<Container>> db = new List<List<Container>>(); // Hangi elemanın hangi clusterda oldugunu tutan liste
                foreach (var mean in Kmeans) // clusterlar ıcınde donen dongu
                {
                    List<Container> ct = new List<Container>(); // clusterın icindeki containerları tutan liste

                    foreach (var item in container) // Container'ın ıcındeki elemanları donen liste
                    {
                        List<double> distanceList = new List<double>(); // container icindeki her bir noktanın her bir clustera olan uzaklıgına tutan liste
                        foreach (var meani in Kmeans) // clusterın ıcınde donen dongu
                        {
                            var distance = Math.Pow(item.Latitude - meani[0], 2) + Math.Pow(item.Longitude - meani[1], 2); // noktanın clustera olan uzaklıgının hesaplandıgı yer
                            distanceList.Add(distance);
                        }
                        int index = distanceList.IndexOf(distanceList.Min()); // noktanın en yakın oldugu clusterın ındexının hesaplanması
                        if (index == meansIndex) // anlık clusterın en yakın cluster oldugunu kontrol eden sorgu
                        {
                            ct.Add(item);

                        }
                    }
                    meansIndex++;
                    db.Add(ct);



                }
                if (ısThat == false) // eger ısthat false olursa algorıtma bıtırılır.
                {
                    return Ok(db);
                }
                List<List<double>> newKeyMeans = new List<List<double>>(); // yeni cluster merkezlerini tutacak liste
                for (int i = 0; i < db.Count; i++) // cluster merkezlerini güncellemek icin kullanılan döngü
                {
                    // cluster merkezlerinin güncellenmesi
                    var ab = db[i];
                    double sum = 0;
                    double sum1 = 0;
                    foreach (var item in ab)
                    {
                        sum += item.Latitude;
                        sum1 += item.Longitude;

                    }
                    List<double> newMean = new List<double>();

                    newMean.Add(sum / ab.Count);
                    newMean.Add(sum1 / ab.Count);


                    newKeyMeans.Add(newMean);
                }
                // clusterların konumundakı degısım hassaslık sınırı
                double errorOld = 0.01;
                ısThat = false;
                // yeni merkez ile eskı merkezlerın arasındakı farkı kontrol eden dongu
                for (int i = 0; i < newKeyMeans.Count; i++)
                {
                    var distance = Math.Pow(newKeyMeans[i][0] - Kmeans[i][0], 2) + Math.Pow(newKeyMeans[i][1] - Kmeans[i][1], 2);
                    if (distance > errorOld)
                    {
                        ısThat = true;
                    }
                }
                // cluster merkezlerının guncellenmesı
                Kmeans = newKeyMeans;


            }







        }
    }
}
