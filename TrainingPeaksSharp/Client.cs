using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TrainingPeaksSharp.Extensions;
using System.Xml.Linq;

namespace TrainingPeaksSharp
{
    public class Client
    {
        private string _userName;
        private string _password;
        private readonly Uri ServiceUri = new Uri("http://www.trainingpeaks.com/TPWebServices/service.asmx");
        private readonly string ServiceNamespace = "http://www.trainingpeaks.com/TPWebServices/";

        public Client(string userName, string password)
        {
            if (userName == null)
                throw new ArgumentException("Username cannot be null or empty.");
            _userName = userName;
            if (password == null)
                throw new ArgumentException("Password cannot be null or empty.");
            _password = password;
        }

        private string GetSoapRequestBody(string body)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.Append("<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">");
            sb.Append("<soap:Body>");
            sb.Append(body);
            sb.Append("</soap:Body>");
            sb.Append("</soap:Envelope>");

            return sb.ToString();
        }

        private async Task<HttpResponseMessage> SendRequestAsync(string methodName, string body)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));
            client.DefaultRequestHeaders.Add("SOAPAction", string.Format("http://www.trainingpeaks.com/TPWebServices/{0}", methodName));
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<{0} xmlns=\"http://www.trainingpeaks.com/TPWebServices/\">", methodName);
            sb.AppendFormat("<username>{0}</username>", _userName);
            sb.AppendFormat("<password>{0}</password>", _password);
            sb.Append(body);
            sb.AppendFormat("</{0}>", methodName);
            // send request
            var soapBody = GetSoapRequestBody(sb.ToString());
            return await client.PostAsync(ServiceUri.AbsoluteUri, new StringContent(soapBody, Encoding.UTF8, "text/xml"));
        }

        public async Task<IEnumerable<Workout>> GetWorkoutsAsync(DateTime startDate, DateTime endDate)
        {
            string requestBody = string.Format("<startDate>{0}</startDate><endDate>{1}</endDate>", startDate.ToSoapFormat(), endDate.ToSoapFormat());
            var response = await SendRequestAsync("GetWorkoutsForAthlete", requestBody);
            var result = await response.Content.ReadAsStringAsync();
            // parsing the results
            var element = XElement.Parse(result);
            var workouts = from workout in element.GetDescendants("Workout") select new Workout(workout);
            return workouts.ToArray();
        }

        /// <summary>
        /// Importing workout
        /// </summary>
        /// <param name="fileData"></param>
        /// <returns></returns>
        public async Task ImportFileForUserAsync(byte[] fileData)
        {
            string requestBody = string.Format("<byteData>{0}</byteData>", System.Convert.ToBase64String(fileData));
            var response = await SendRequestAsync("ImportFileForUser", requestBody);
        }

        /// <summary>
        /// Deletes the specified workouts
        /// </summary>
        /// <param name="workoutIds"></param>
        /// <returns></returns>
        public async Task DeleteWorkoutsAsync(IEnumerable<int> workoutIds)
        {
            StringBuilder requestBuilder = new StringBuilder();
            requestBuilder.Append("<workoutIds>");
            foreach (int workoutId in workoutIds)
            requestBuilder.AppendFormat("<int>{0}</int>", workoutId);
            requestBuilder.Append("</workoutIds>");
            var response = await SendRequestAsync("DeleteWorkoutsForAthlete", requestBuilder.ToString());
            Debug.WriteLine(response);
        }
    }
}
