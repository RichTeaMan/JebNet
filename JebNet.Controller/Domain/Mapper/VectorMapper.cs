using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JebNet.Controller.Domain.Mapper
{
    public class VectorMapper
    {
        public Vector MapToDomain(JebNet.Controller.Integration.Domain.Vector vector)
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

        public JebNet.Controller.Integration.Domain.Vector MapToIntegration(Vector vector)
        {
            JebNet.Controller.Integration.Domain.Vector integrationVector = null;
            if (null != vector)
            {
                integrationVector = new JebNet.Controller.Integration.Domain.Vector();
                integrationVector.x = vector.x;
                integrationVector.y = vector.y;
                integrationVector.z = vector.z;
            }
            return integrationVector;
        }
    }
}
