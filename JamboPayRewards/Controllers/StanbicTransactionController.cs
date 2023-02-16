using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JamboPayRewards.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StanbicTransactionController : ControllerBase
    {

        public static TransactionResponse NewTransaction(TransactionRequestObject transactionRequestObject)
        {
            TransactionResponse transactionResponse = new TransactionResponse();

            var dataToSend = JsonConvert.SerializeObject(transactionRequestObject);

            String requestURI = "https://api.connect.stanbicbank.co.ke/api/sandbox";
            var client = new RestClient(requestURI);
            client.AddDefaultHeader("X-IBM-Client-Id", transactionRequestObject.ApiKey);
            client.AddDefaultHeader("Content-Type", "application/xml");
            client.AddDefaultHeader("Accept", "application/xml");

            RestRequest request = null;

            request = new RestRequest("/transaction-notification-api", Method.Post);

            request.AddJsonBody(dataToSend);
            request.RequestFormat = DataFormat.Xml;

            try
            {
                var response = client.Execute(request);

                transactionResponse = JsonConvert.DeserializeObject<TransactionResponse>(response.Content);
            }
            catch
            {

            }

            return transactionResponse;
        }

    }
    public class TransactionRequestObject {
        public string AlertType { get; set; }

        public string AlertName { get; set; }

        public string SendTo { get; set; }

        public string AccountNo { get; set; }

        public long CustomerNo { get; set; }

        public string Subject { get; set; }

        public string CustomerName { get; set; }

        public string Date { get; set; }

        public string ValueDate { get; set; }

        public string ActionType { get; set; }

        public string TxnDescr { get; set; }

        public string TxnAmount { get; set; }

        public string OurTxnReference { get; set; }

        public string ThirdPartyRef { get; set; }

        public string TransactionOriginationBranch { get; set; }

        public string Narrative { get; set; }

        public string CurrentBalance { get; set; }

        public string AvailableBalance { get; set; }

        public string ClearedBalance { get; set; }

        public long AccountOfficer { get; set; }

        public string ApiKey { get; set; }

        public string ClientFormat { get; set; }

        public string Msisdn { get; set; }

        public long PayerRef { get; set; }

        public Uri CallbackUrl { get; set; }
    }

    public class TransactionResponse
    {
        public string ResultCode { get; set; }
        public string ResultDesc { get; set; }
    }
}
