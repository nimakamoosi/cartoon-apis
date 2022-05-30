using CartoonApis.DataAccess;
using HotChocolate.Subscriptions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;

namespace CartoonApis.Controllers
{
    [ApiController]
    [Route("api/family")]
    public class FamilyController : ControllerBase
    {
        private readonly IFamilyRepository _familyRepository;
        private readonly ITopicEventReceiver _eventReceiver;
        private readonly ITopicEventSender _eventSender;
        private readonly ILogger<FamilyController> _logger;

        public FamilyController([Service] IFamilyRepository authorService, ILogger<FamilyController> logger
            , [Service] ITopicEventReceiver eventReceiver, [Service] ITopicEventSender eventSender)
        {
            _logger = logger;            
            _familyRepository = authorService;
            _eventReceiver = eventReceiver;
            _eventSender = eventSender;
        }
                
        [HttpGet]
        public IEnumerable<Family> Get()
        {
            return _familyRepository.GetFamilies();
        }

        [HttpGet("{id}")]
        public Family GetFamily(int id)
        {
            return _familyRepository.GetFamilies().SingleOrDefault(f => f.Id == id);
        }

        [HttpPut]
        public async Task<Family> CreateOrUpdateFamily(Family family)
        {
            var existingFamily = _familyRepository.GetFamilyById(family.Id);
            Family familyResult = default;
            if (existingFamily == null)
            {
                familyResult = await _familyRepository.CreateFamily(family);
                await _eventSender.SendAsync("FamilyCreated", familyResult);
            }
            else
            {
                familyResult = await _familyRepository.UpdateFamily(family);
            }

            return familyResult;
        }

        [HttpPost]
        public async Task<Family> CreateFamily(Family family)
        {
            return await _familyRepository.CreateFamily(family);
        }

        [HttpDelete]
        public async Task DeleteFamily(int id)
        {
            await _familyRepository.DeleteFamily(id);
        }

        [HttpGet]
        [Route("on-create")]
        public async Task OnCreateFamily()
        {
            if (this.HttpContext.WebSockets.IsWebSocketRequest)
            {
                CancellationToken cancellationToken = CancellationToken.None;
                var webSocket = await this.HttpContext.WebSockets.AcceptWebSocketAsync();

                var subscription = await _eventReceiver.SubscribeAsync<string, Family>("FamilyCreated", cancellationToken);

                while (true)
                {
                    await foreach (var family in subscription.ReadEventsAsync())
                    {
                        var familyJson = JObject.FromObject(family);
                        var familyJsonString = familyJson.ToString();
                        var byteArray = Encoding.ASCII.GetBytes(familyJsonString);
                        var arraySegment = new ArraySegment<byte>(byteArray);
                        await webSocket.SendAsync(arraySegment, System.Net.WebSockets.WebSocketMessageType.Text, true, cancellationToken);
                    }

                    if (webSocket.State == WebSocketState.Aborted || webSocket.State == WebSocketState.Closed)
                    {
                        break;
                    }
                }
            }
            else 
            {
                this.HttpContext.Response.StatusCode = 400;
            }
        }
    }
}