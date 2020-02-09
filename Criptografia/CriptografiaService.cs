using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Criptografia
{
    public class CriptografiaService
    {
        private const string token = "35e59363ac83631a21b26c0ecbe898f68096b651";
        private const string generateData = "https://api.codenation.dev/v1/challenge/dev-ps/generate-data?token=";
        private const string submitSolution = "https://api.codenation.dev/v1/challenge/dev-ps/submit-solution?token=";
        private const string DefaultUserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.154 Safari/537.36 OPR/20.0.1387.82";

        public DecriptEncript GetJson()
        {
            WebRequest request = WebRequest.Create(generateData + token);
            WebResponse response = request.GetResponse();

            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);

            string texto = reader.ReadToEnd();
            dataStream.Close();

            return JsonConvert.DeserializeObject<DecriptEncript>(texto);
        }

        public void SendFile()
        {
            FileStream fs = new FileStream(@"C:\temp\answer.json", FileMode.Open, FileAccess.Read);
            byte[] data = new byte[fs.Length];
            fs.Read(data, 0, data.Length);
            fs.Close();

            Dictionary<string, object> postParameters = new Dictionary<string, object>();
            postParameters.Add("answer", new FormUpload.FileParameter(data, "answer.json", "application/json"));

            string postURL = (submitSolution + token);
            string userAgent = DefaultUserAgent;
            HttpWebResponse webResponse = FormUpload.MultipartFormDataPost(postURL, userAgent, postParameters);

            StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
            string fullResponse = responseReader.ReadToEnd();
            webResponse.Close();
            Console.Write(fullResponse);
            Console.Read();
        }

        public void CreateFile(DecriptEncript decriptEncript)
        {
            string fileName = @"C:\temp\answer.json";
            FileInfo fi = new FileInfo(fileName);

            try
            {
                if (fi.Exists)
                {
                    fi.Delete();
                }

                using (FileStream fs = fi.Create())
                {
                    byte[] jsonByte = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(decriptEncript));
                    fs.Write(jsonByte, 0, jsonByte.Length);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }
    }
}
