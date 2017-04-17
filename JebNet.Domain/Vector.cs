using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JebNet
{
    [Serializable]
    public class Vector
    {
        public const double kEpsilon = 1E-05D;

        /// <summary>
        /// X component of the vector.
        /// </summary>
        public double x;
        /// <summary>
        /// Y component of the vector.
        /// </summary>
        //     ///
        public double y;

        /// <summary>
        /// Z component of the vector.
        /// </summary>
        public double z;
    }
}
