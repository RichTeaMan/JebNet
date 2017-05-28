using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetNet.Controller.Utilities
{
    public class PidController
    {


        /// <summary>
        /// Previous error.
        /// </summary>
        public double PreviousError { get; private set; } = 0;

        /// <summary>
        /// Proportional gain.
        /// </summary>
        public double Kp { get; set; }

        /// <summary>
        /// Integral gain.
        /// </summary>
        public double Ki { get; set; }

        /// <summary>
        /// Derivative gain.
        /// </summary>
        public double Kd { get; set; }

        /// <summary>
        /// Minimum output;
        /// </summary>
        public double MinOutput { get; set; } = double.MinValue;

        /// <summary>
        /// Maximum output.
        /// </summary>
        public double MaxOutput { get; set; } = double.MaxValue;

        /// <summary>
        /// Integral.
        /// </summary>
        public double Integral { get; private set; } = 0;


        public double control(double desiredValue, double measuredValue, int durationMilliseconds)
        {
            double error = desiredValue - measuredValue;
            Integral = Clamp(Integral + (Ki * error * durationMilliseconds));
            double derivative = (error - PreviousError) / (double)durationMilliseconds;
            double output = (Kp * error) + Integral + (Kd * derivative);
            PreviousError = error;

            output = Math.Max(MinOutput, output);
            output = Math.Min(MaxOutput, output);

            return output;
        }

        private double Clamp(double value)
        {
           double output = Math.Max(MinOutput, value);
            output = Math.Min(MaxOutput, output);
            return output;
        }

    }
}
