using JebNet.Domain.Mapper;
using JebNet.Server.Service;
using KSP.UI.Screens;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using UnityEngine;

namespace JebNet.Server
{
    public class NetworkSlaveCommand : PartModule
    {

        /// <summary>
        /// Logger.
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.Ge‌​tCurrentMethod().Dec‌​laringType);

        /// <summary>
        /// Port the vessel is listening on.
        /// </summary>
        private const int NETWORK_PORT = 2001;

        /// <summary>
        /// Log 4 net config file name.
        /// </summary>
        private const string Log4NetConfigFileName = "Log4Net.xml";

        /// <summary>
        /// Server to get control messages.
        /// </summary>
        private Server server;

        /// <summary>
        /// Vessel mapper.
        /// </summary>
        private VesselMapper vesselMapper = new VesselMapper();

        /// <summary>
        /// Vessel service.
        /// </summary>
        private VesselControlService vesselControlService = new VesselControlService();

        static NetworkSlaveCommand() {
            var log4netXmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Log4NetConfigFileName);


            var patternLayout = new PatternLayout
            {
                ConversionPattern = "%date %-5level %logger - %message%newline"
            };
            patternLayout.ActivateOptions();

            // setup the appender that writes to Log\EventLog.txt
            var fileAppender = new RollingFileAppender
            {
                AppendToFile = true,
                File = @"jebnet.log",
                Layout = patternLayout,
                MaxSizeRollBackups = 5,
                MaximumFileSize = "10MB",
                RollingStyle = RollingFileAppender.RollingMode.Size,
                StaticLogFileName = true
            };
            fileAppender.ActivateOptions();
            

            BasicConfigurator.Configure(fileAppender);


           // XmlConfigurator.Configure(new FileInfo(log4netXmlPath));
        }

        /// <summary>
        /// Called once when the part is active in the game (ie, started on the launchpad).
        /// </summary>
        public override void OnStart(StartState state)
        {

            log.DebugFormat("Network slave command module on start. State is '{0}'.", state);

            try
            {
                log.Debug("Attempting to start server.");
                server = new Server(NETWORK_PORT);
                server.Start();

                log.Debug("Server started.");

            }
            catch (Exception ex)
            {
                log.Debug("Failed to start server.", ex);
            }

            base.OnStart(state);
        }

        public void OnDestroy()
        {
            log.Debug("Network slave command on destroy.");
            if (null != server)
            {
                server.Stop();
            }
        }

        public override void OnActive()
        {
            log.Debug("Network slave command module on active");
            base.OnActive();
        }

        public override void OnFixedUpdate()
        {
            log.Debug("Network slave command module on fixed update");

            base.OnFixedUpdate();

        }

        /// <summary>
        /// Called frequently during play (every tick?).
        /// </summary>
        public void Update()
        {
            
            Context context = server.FetchContext();
            if (null != context)
            {
                using (HttpListenerResponse response = context.HttpListenerResponse)
                using (Stream output = response.OutputStream)
                {

                    try
                    {
                        log.Debug("Update: processing context.");
                        if (context.RequestContext.Method == "POST")
                        {
                            log.Debug("POST recieved");
                            var requestVessel = JsonUtility.FromJson<Domain.Vessel>(context.RequestContext.Body);
                            log.Debug("Map complete.");
                            vesselControlService.TransformVessel(requestVessel, vessel);
                            log.Debug("Transform complete.");
                        }


                        var domainVessel = vesselMapper.Map(vessel);

                        var serialisedVessel = JsonUtility.ToJson(domainVessel);

                        // Construct a response.
                        byte[] buffer = Encoding.UTF8.GetBytes(serialisedVessel);
                        response.ContentLength64 = buffer.Length;
                        response.StatusCode = 200;

                        output.Write(buffer, 0, buffer.Length);

                        log.Debug("Update: processing context complete.");
                    }
                    catch (Exception ex)
                    {
                        log.Debug("Exception caught.", ex);
                        byte[] buffer = Encoding.UTF8.GetBytes(ex.Message);
                        response.ContentLength64 = buffer.Length;
                        response.StatusCode = 500;

                        output.Write(buffer, 0, buffer.Length);
                    }
                }
            }
        }

        public override void OnInactive()
        {
            base.OnInactive();
        }

    }

}
