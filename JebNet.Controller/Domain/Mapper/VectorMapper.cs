using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JebNet.Controller.Domain.Mapper
{
    public class VectorMapper
    {
        public Vector MapToDomain(double x, double y, double z)
        {
            Vector domainVector = new Vector();
                domainVector.x = x;
                domainVector.y = y;
                domainVector.z = z;
            return domainVector;
        }
        
    }
}
