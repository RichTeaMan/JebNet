using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JebNet.Controller.Domain
{
    public class Vector
    {
        public const double kEpsilon = 1E-05D;

        /// <summary>
        /// X component of the vector.
        /// </summary>
        public double x { get; set; }
        /// <summary>
        /// Y component of the vector.
        /// </summary>
        //     ///
        public double y { get; set; }

        /// <summary>
        /// Z component of the vector.
        /// </summary>
        public double z { get; set; }
    }
}
