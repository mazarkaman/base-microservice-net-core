using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PhungDKH.ApiGateway
{
    public class Destination
    {
        public string Uri { get; set; }

        public bool RequiresAuthentication { get; set; }

        static HttpClient Client = new HttpClient();

        public async Task<HttpResponseMessage> SendRequest(HttpRequest request)
        {
            string requestContent;
            using (Stream receiveStream = request.Body)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    requestContent = await readStream.ReadToEndAsync();
                }
            }

            using (var newRequest = new HttpRequestMessage(new HttpMethod(request.Method), Uri))
            {
                if (RequiresAuthentication)
                {
                    newRequest.Headers.Add("Authorization", request.Headers["Authorization"].ToString());
                }

                newRequest.Content = new StringContent(requestContent, Encoding.UTF8, request.ContentType);
                using (var response = await Client.SendAsync(newRequest))
                {
                    return response;
                }
            }
        }
    }
}
