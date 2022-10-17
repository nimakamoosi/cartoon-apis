using CartoonApis.DataAccess;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;

namespace CartoonApis.Test
{
    public class RestApiTests
    {
        public const string BaseAddress = "https://localhost:7207/api/family";
        public Random Random = new Random();

        [Fact]
        public async Task BasicGetTest()
        {
            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri(BaseAddress);
            var client = CreateHttpClient();
            var response = await client.GetAsync(string.Empty);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task BasicPutTest()
        {
            var id = this.Random.Next(Int16.MaxValue) + 100;
            var familyJsonString = $"{{ \"id\": \"{id}\", \"name\": \"Family #{id}\" }}"; ;
            await CreateNewFamilyAsync(familyJsonString);
        }

        [Fact]
        public async Task WebSocketTest()
        {
            using (var client = new ClientWebSocket())
            {
                var webSocketUri = new Uri($"{BaseAddress.Replace("http", "ws")}/on-create");
                await client.ConnectAsync(webSocketUri, CancellationToken.None);

                var id = this.Random.Next(Int16.MaxValue) + 100;
                var familyJsonString = $"{{ \"id\": \"{id}\", \"name\": \"Family #{id}\" }}"; ;
                await CreateNewFamilyAsync(familyJsonString);

                var familyCreatedEventString = await this.ReadAsync(client);
                var familyObject = JObject.Parse(familyCreatedEventString);
                var family = familyObject.ToObject<Family>();

                Assert.NotNull(family);
                Assert.Equal(id, family.Id);

                string stringValue = "asdfas";
                string.IsNullOrWhiteSpace(stringValue.TrimEnd().TrimEnd('}').TrimStart().TrimStart('}'));

                id++;
                var familyJsonString2 = $"{{ \"id\": \"{id}\", \"name\": \"Family #{id}\" }}"; ;
                await CreateNewFamilyAsync(familyJsonString2);

                var familyCreatedEventString2 = await this.ReadAsync(client);
                var familyObject2 = JObject.Parse(familyCreatedEventString2);
                var family2 = familyObject2.ToObject<Family>();

                Assert.NotNull(family2);
                Assert.Equal(id, family2.Id);
            }
        }    
        
        internal async Task CreateNewFamilyAsync(string familyJsonString)
        {
            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri(BaseAddress);
            var client = CreateHttpClient();
            var id = this.Random.Next(Int16.MaxValue) + 100;
            var httpContent = new StringContent(familyJsonString, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(string.Empty, httpContent);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        internal HttpClient CreateHttpClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseAddress);
            return client;
        }

        public async Task SendAsync(ClientWebSocket client, string data)
        {
            var encoded = Encoding.UTF8.GetBytes(data);
            var buffer = new ArraySegment<Byte>(encoded, 0, encoded.Length);
            await client.SendAsync(buffer, WebSocketMessageType.Text, endOfMessage: true, CancellationToken.None);
        }

        public async Task<String> ReadAsync(ClientWebSocket client, CancellationToken cancellationToken = default)
        {
            using (var memoryStream = new MemoryStream())
            {
                WebSocketReceiveResult result;
                var buffer = new ArraySegment<byte>(new Byte[1024]);

                do
                {
                    result = await client.ReceiveAsync(buffer, cancellationToken);

                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        if (client.State == WebSocketState.Open)
                        {
                            await client.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client sent close", CancellationToken.None);
                        }
                    }

                    memoryStream.Write(buffer.Array, buffer.Offset, result.Count);
                }
                while (!result.EndOfMessage);

                memoryStream.Seek(0, SeekOrigin.Begin);
                var messageType = result.MessageType;

                using (var reader = new StreamReader(memoryStream, Encoding.UTF8))
                    return reader.ReadToEnd();
            }
        }
    }
}