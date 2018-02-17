using System;
using System.IO;

namespace JebNet.Server
{
    public class JebLogger
    {
        public string LogFilePath { get; set; } = @"JebNetLog.txt";

        /// <summary>
        /// The type being logged.
        /// </summary>
        private Type _type;

        public JebLogger(Type type)
        {
            _type = type;
        }

        /// <summary>
        /// Logs.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void Log(string message, params object[] args)
        {
            string resolvedMessage = string.Format(message, args);

            string datedResolvedMessage = string.Format("[{0}, {1}] - {2}", DateTime.Now, _type.Name, resolvedMessage);

            using (StreamWriter file = new StreamWriter(LogFilePath, true))
            {
                file.WriteLine(datedResolvedMessage);
            }
        }
    }
}
