using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JebNet.Domain.Mapper
{
    public class VectorMapper
    {
        public Vector Map(global::Vector3d vector)
        {
            Vector domainVector = null;
            if (null != vector)
            {
                domainVector = new Vector();
                domainVector.x = vector.x;
                domainVector.y = vector.y;
                domainVector.z = vector.z;
            }
            return domainVector;
        }
    }
}
