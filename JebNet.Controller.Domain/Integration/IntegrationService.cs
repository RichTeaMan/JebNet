using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JebNet.Controller.Domain.Integration
{
    /// <summary>
    /// Class for interfacing with remote JSON endpoints.
    /// </summary>
    public class IntegrationService
    {
        /// <summary>
        /// GETs a resource from the URL and returns a string.
        /// </summary>
        /// <param name="url">URL to send to.</param>
        /// <returns>Response body as a string.</returns>
        public string Retrieve(string url)
        {
            using (WebClient webClient = new WebClient())
            {
                string responseString = webClient.DownloadString(url);
                return responseString;
            }
        }

        /// <summary>
        /// GETs a resource from the URL and serialises to the typed object. The response should be in JSON format.
        /// </summary>
        /// <typeparam name="T">Object to serialise the response to.</typeparam>
        /// <param name="url">URL to send to.</param>
        /// <returns>Response as an object.</returns>
        public T Retrieve<T>(string url)
        {
            string responseString = Retrieve(url);
            T response = JsonConvert.DeserializeObject<T>(responseString);
            return response;
        }

        /// <summary>
        /// POSTs to the given URL with the data. The data object will be encoded as JSON.
        /// </summary>
        /// <param name="url">URL to send to.</param>
        /// <param name="data">Data to send. It will be JSON encoded.</param>
        /// <returns>Response as a string.</returns>
        public string Send(string url, object data)
        {
            string jsonData = JsonConvert.SerializeObject(data);
            byte[] bytesData = ASCIIEncoding.ASCII.GetBytes(jsonData);
            using (WebClient webClient = new WebClient())
            {
                byte[] responseBytes = webClient.UploadData(url, bytesData);
                string responseString = ASCIIEncoding.ASCII.GetString(responseBytes);
                return responseString;
            }
        }

        /// <summary>
        /// POSTs to the given URL with the data. The data object will be encoded as JSON.
        /// </summary>
        /// <param name="url">URL to send to.</param>
        /// <param name="data">Data to send. It will be JSON encoded.</param>
        /// <returns>Response as an object.</returns>
        public T Send<T>(string url, object data)
        {
            string responseString = Send(url, data);
            T response = JsonConvert.DeserializeObject<T>(responseString);
            return response;
        }
    }
}
