using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Script.Serialization;

namespace CS.Mandrill
{
    public class Message
    {
        readonly ApiSetting _setting;

        public Message()
            : this(new ApiSetting { ApiKey = "", RequestUrl = "https://mandrillapp.com/api/1.0/users/ping.json" }){}

        public Message(ApiSetting setting)
        {
            _setting = setting;
        }

        [DataContract]
        public class MessageSend
        {
            [DataMember(Name = "message")]
            public MessageRequest Message { get; set; }

            [DataMember(Name = "key")]
            public string Key { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        public void Send(MessageRequest request) {
            var url = _setting.RequestUrl;
            var messageSend = new MessageSend {Message = request, Key = _setting.ApiKey};

            var stream1 = new MemoryStream();
            var ser = new DataContractJsonSerializer(typeof(MessageSend));

            ser.WriteObject(stream1, messageSend);
            stream1.Position = 0;
            var json = "";
            using (var sr = new StreamReader(stream1))
            {
                 json = sr.ReadToEnd();
            }

            MakePost(url, json);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="jsonData"></param>
        private string MakePost(string url, string jsonData){
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            //httpWebRequest.ContentType = "text/json";
            httpWebRequest.ContentType="application/x-www-form-urlencoded";
            httpWebRequest.Method = "POST";

            // Create POST data and convert it to a byte array.
            var postData = jsonData;
            var byteArray = Encoding.UTF8.GetBytes(postData);

            httpWebRequest.ContentLength = byteArray.Length;
            // Get the request stream.
            var dataStream = httpWebRequest.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            var response = httpWebRequest.GetResponse();

            //Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            var reader = new StreamReader(dataStream);
            // Read the content.
            var responseFromServer = reader.ReadToEnd();
            // Display the content.
            
            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;

        }

    }

    
}
