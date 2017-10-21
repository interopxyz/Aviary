using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wind.Geometry.Vectors
{
    public class wPlane
    {
        public string Type = "Plane";

        public wPoint Origin = new wPoint();
        public wVector XAxis = new wVector().Xaxis();
        public wVector YAxis = new wVector().Yaxis();
        public wVector ZAxis = new wVector().Zaxis();

        public wPlane()
        {

        }

        public wPlane(wPoint OriginPoint, wPoint XLocation, wPoint YLocation)
        {
            Origin = OriginPoint;
            XAxis = new wVector(OriginPoint, XLocation);
            YAxis = new wVector(OriginPoint, YLocation);
        }

        public wPlane(wPoint OriginPoint, wVector XVector, wVector YVector)
        {
            Origin = OriginPoint;
            XAxis = XVector;
            YAxis = YVector;
        }

        public wPlane(wPoint OriginPoint, wVector XVector, wVector YVector, wVector ZVector)
        {
            Origin = OriginPoint;
            XAxis = XVector;
            YAxis = YVector;
            ZAxis = ZVector;
        }

        public wPlane XYPlane()
        {
            return new wPlane(new wPoint(), new wVector().Xaxis(), new wVector().Yaxis(), new wVector().Zaxis());
        }

        public wPlane YZPlane()
        {
            return new wPlane(new wPoint(), new wVector().Yaxis(), new wVector().Zaxis(), new wVector().Xaxis());
        }

        public wPlane XZPlane()
        {
            return new wPlane(new wPoint(), new wVector().Xaxis(), new wVector().Zaxis(), new wVector().Yaxis());
        }

        public void SetOrigin(wPoint OriginPoint)
        {
            Origin = OriginPoint;
        }
    }
}
