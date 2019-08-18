using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Extensions.Configuration;
using BlueYonder.Flights.Service.Repository;

namespace BlueYonder.Flights.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private CloudBlobContainer _container;
        private CloudStorageAccount _storageAccount;
        private const string _manifests = "manifests";
        private IPassangerRepository _passangerRepository;

        public FlightsController(IConfiguration configuration, IPassangerRepository passangerRepository)
        {
            _passangerRepository = passangerRepository;
            _storageAccount = CloudStorageAccount.Parse(configuration.GetConnectionString("BloggingDatabase"));
            CloudBlobClient blogClient = _storageAccount.CreateCloudBlobClient();
            _container = blogClient.GetContainerReference(_manifests);


        }

        [HttpGet("FinalizeFlight")]
        public async Task<ActionResult> FinalizeFlight()
        {
            await _container.CreateIfNotExistsAsync();

            CloudBlockBlob blockBlob = _container.GetBlockBlobReference($"{_manifests }.txt");
            MemoryStream manifests = GeneratedManifests();
            await blockBlob.UploadFromStreamAsync(manifests);

            manifests.Close();
            return Ok();
        }

        [HttpGet("PassengerManifest")]
        public ActionResult<string> GetPassengerManifest()
        {
            CloudBlockBlob blockBlob = _container.GetBlockBlobReference($"{_manifests }.txt");
            return blockBlob.Uri.ToString() + GetSASToken();
        }

        private string GetSASToken()
        {
            SharedAccessAccountPolicy policy = new SharedAccessAccountPolicy()
            {
                Permissions = SharedAccessAccountPermissions.Read,
                Services = SharedAccessAccountServices.Blob,
                ResourceTypes = SharedAccessAccountResourceTypes.Object,
                SharedAccessExpiryTime = DateTime.Now.AddMinutes(1)
            };

            return _storageAccount.GetSharedAccessSignature(policy);
        }

        private System.IO.MemoryStream GeneratedManifests()
        {
            System.IO.MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            Random random = new Random();
            writer.Write("All Passenger" + Environment.NewLine);
            foreach (var passanger in _passangerRepository.GetPassangerList())
            {
                writer.Write(passanger + Environment.NewLine);
            }
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        
    }
}
