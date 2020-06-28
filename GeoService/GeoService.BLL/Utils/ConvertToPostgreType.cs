using GeoService.BLL.DTO;
using NpgsqlTypes;
using System.Collections.Generic;

namespace GeoService.API.BusinessLogic.Utils
{
    public static class ConvertToPostgreType
    {
        public static NpgsqlPolygon ToPolygon(this PolygonTeam polygon)
        {
            List<NpgsqlPoint> postgrePOint = new List<NpgsqlPoint>();

            for(int i = 0; i < polygon.Count; i++)
                postgrePOint.Add(new NpgsqlPoint(polygon.X[i], polygon.Y[i]));
            
            return new NpgsqlPolygon(postgrePOint);
        }

        public static PolygonTeam ToPolygon(this NpgsqlPolygon polygon)
        {
            
            var xCoords = new List<double>();
            var yCoords = new List<double>();

            foreach (var point in polygon)
            {
                xCoords.Add(point.X);
                yCoords.Add(point.Y);
            }

            return new PolygonTeam
            {
                X = xCoords.ToArray(),
                Y = yCoords.ToArray()
            };
        }
    }
}
