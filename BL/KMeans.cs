using BE;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class KMeans
    {
        public List<Report> ReportsList { get; set; }
        public int K { get; set; }

        public KMeans(ICollection<Report> reportsList, int k)
        {
            ReportsList = reportsList.ToList();
            K = k;
        }

        public List<GeoCoordinate> K_Means()
        {
            if (ReportsList.Count == 0)
                return null;

            List<GeoCoordinate> clustersIdList = ClustersGenerator();

            bool isClustersChanged;
            var counter = 0;
            do
            {
                isClustersChanged = false;
                //for each report looking for the closest cluster 
                for (int i = 0; i < ReportsList.Count; i++)
                {
                    double min = ReportsList[i].GetCoordinate().GetDistanceTo(clustersIdList[0]);
                    ReportsList[i].ClusterId = 0;

                    for (int j = 1; j < clustersIdList.Count; j++)
                    {
                        double temp = ReportsList[i].GetCoordinate().GetDistanceTo(clustersIdList[j]);
                        if (temp < min)
                        {
                            min = temp;
                            isClustersChanged = true;
                            ReportsList[i].ClusterId = j;
                        }
                    }

                }
                counter++;
                clustersIdList = RecenterClusters(clustersIdList);
                if (counter == 100)
                {
                    break;
                }
            } while (isClustersChanged);

            return clustersIdList;
        }

        private List<GeoCoordinate> RecenterClusters(List<GeoCoordinate> clustersIdList)
        {
            //Recenter the clusters
            ReportsList = ReportsList.OrderBy(c => c.ClusterId).ToList();
            int id = 0;
            double clustersLongitudeSum = 0;
            double clustersLatitudeSum = 0;
            int counter = 0;
            for (int i = 0; i < ReportsList.Count; i++)
            {
                if (ReportsList[i].ClusterId == id)
                {
                    clustersLatitudeSum += ReportsList[i].GetCoordinate().Latitude;
                    clustersLongitudeSum += ReportsList[i].GetCoordinate().Longitude;
                    counter++;
                }
                else if (ReportsList[i].ClusterId != id)
                {
                    clustersIdList[id].Latitude = clustersLatitudeSum / counter;
                    clustersIdList[id].Longitude = clustersLongitudeSum / counter;
                    counter = 0;
                    clustersLongitudeSum = 0;
                    clustersLatitudeSum = 0;
                    i--;
                    id++;
                }
            }
            clustersIdList[id].Latitude = clustersLatitudeSum / counter;
            clustersIdList[id].Longitude = clustersLongitudeSum / counter;
            return clustersIdList;

        }

        private List<GeoCoordinate> ClustersGenerator()
        {

            List<GeoCoordinate> clustersIdList = new List<GeoCoordinate>();

            double minLatitude = ReportsList.Min(r => r.Latitude);
            double maxLatitude = ReportsList.Max(r => r.Latitude);
            double minLongitude = ReportsList.Min(r => r.Longitude);
            double maxLongitude = ReportsList.Max(r => r.Longitude);

            for (int i = 0; i < K; i++)
            {
                Random rand = new Random(i);
                double latitude = minLatitude + rand.NextDouble() * (maxLatitude - minLatitude);
                double longitude = minLongitude + rand.NextDouble() * (maxLongitude - minLongitude);
                GeoCoordinate coordinate = new GeoCoordinate(latitude, longitude);
                clustersIdList.Add(coordinate);
            }

            return clustersIdList;
        }


    }
}