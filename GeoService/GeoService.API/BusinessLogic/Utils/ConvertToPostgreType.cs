using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoService.API.BusinessLogic.Utils
{
    public static class ConvertToPostgreType
    {
        public static NpgsqlPolygon ToPolygon(double[] x, double[] y)
        {
            List<NpgsqlPoint> postgrePOint = new List<NpgsqlPoint>();
            for(int i = 0; i < x.Length; i++)
                postgrePOint.Add(new NpgsqlPoint(x[i], y[i]));
            
            return new NpgsqlPolygon(postgrePOint);
        }
    }
}
