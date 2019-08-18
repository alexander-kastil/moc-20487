using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BlueYonder.Hotels.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private CloudBlobContainer _container;
        private CloudStorageAccount _storageAccount;

        public ReservationController(IConfiguration configuration)
        {
            var conStr = configuration.GetConnectionString("BloggingDatabase");
            _storageAccount = CloudStorageAccount.Parse(conStr);
            CloudBlobClient blogClient = _storageAccount.CreateCloudBlobClient();
            _container = blogClient.GetContainerReference("vouchers");
        }

       
        [HttpGet("Voucher/{id}")]
        public async Task<ActionResult<string>> GetVoucher(Guid id)
        {
            try
            {
                CloudBlockBlob blockBlob = _container.GetBlockBlobReference($"{id}.txt");
                string voucherResult = await blockBlob.DownloadTextAsync();
                return voucherResult;

            }catch(Exception ex)
            {
                return NotFound();
            }
        }

        
        [HttpPost("CreateVoucher")]
        public async Task<ActionResult<Guid>> CreateVoucher([FromBody] string name)
        {
            Guid voucherId = Guid.NewGuid();
            await _container.CreateIfNotExistsAsync();

            CloudBlockBlob blockBlob = _container.GetBlockBlobReference($"{voucherId}.txt");
            MemoryStream voucherStream = GeneratedVoucher(name, voucherId);
            await blockBlob.UploadFromStreamAsync(voucherStream);

            return voucherId;
        }

        private System.IO.MemoryStream GeneratedVoucher(string name,Guid voucherId)
        {
            System.IO.MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            Random random = new Random();
            writer.Write("Hotel Azure" + Environment.NewLine);
            writer.Write($"Guest Name {name}" + Environment.NewLine);
            writer.Write($"Voucher Id {voucherId}" + Environment.NewLine);
            writer.Write($"Voucher Date {DateTime.Now}" + Environment.NewLine);

            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
