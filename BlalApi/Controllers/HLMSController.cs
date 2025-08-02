using BlalApi.Authentication;
using BlalApi.Models;
using BlalApi.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace BlalApi.Controllers
{
    [JwtAuthentication]
    public class HLMSController : ApiController
    {

        private readonly HLMSRepository _hlmsRepository;
        public HLMSController(HLMSRepository hlmsRepository)
        {
            _hlmsRepository = hlmsRepository;
        }
        [HttpPost]
        [Route("HLMReceiverAPI")]
        public async Task<Response> HLMReceiverAPI(List<BookingDataModel> reqModel)
        {
            try
            {
                var data = await _hlmsRepository.ReceiveRequest(reqModel);
              //  _logger.LogInformation($"HLMReceiverAPI API responded - {data}");
                return Models.Response.GetResponse((int)Enums.StatusCode.OK, data, "Success", "");
            }
            catch (Exception ex)
            {
                return Models.Response.GetResponse((int)Enums.StatusCode.ERROR, "", ex.StackTrace, "Data not found");
            }
        }

        [HttpPost]
        [Route("CancelBooking")]
        public async Task<Response> CancelBooking(string bookingId)
        {
            try
            {
                var data = await _hlmsRepository.RejectBooking(bookingId);
              //  _logger.LogInformation($"CancelBooking API responded - {data}");
                return Models.Response.GetResponse((int)Enums.StatusCode.OK, data, "Success", "");
            }
            catch (Exception ex)
            {
                return Models.Response.GetResponse((int)Enums.StatusCode.ERROR, "", ex.StackTrace, "Data not found");
            }
        }

        [HttpPost]
        [Route("GetLabNoFromBookingId")]
        public async Task<Response> GetLabNoFromBookingId(string bookingId)
        {
            try
            {
                var data = _hlmsRepository.GetLabNoFromBookingId(bookingId);
              //  _logger.LogInformation($"GetReportStatus API responded - {data}");
                return Models.Response.GetResponse((int)Enums.StatusCode.OK, data, "Success", "");
            }
            catch (Exception ex)
            {
                return Models.Response.GetResponse((int)Enums.StatusCode.ERROR, "", ex.StackTrace, "Data not found");
            }
        }

        [HttpPost]
        [Route("GetReportStatus")]
        public async Task<Response> GetReportStatus(string LedgerTransactionNo)
        {
            try
            {
                var data = await _hlmsRepository.GetReportStatus(LedgerTransactionNo);
              //  _logger.LogInformation($"GetReportStatus API responded - {data}");
                return Models.Response.GetResponse((int)Enums.StatusCode.OK, data, "Success", "");
            }
            catch (Exception ex)
            {
                return Models.Response.GetResponse((int)Enums.StatusCode.ERROR, "", ex.StackTrace, "Data not found");
            }
        }

        [HttpPost]
        [Route("GetReport")]
        public async Task<Response> GetReport(string LedgerTransactionNo)
        {
            try
            {
                var data = _hlmsRepository.GetReport(LedgerTransactionNo);
               // _logger.LogInformation($"GetReport API responded - {data}");
                return Models.Response.GetResponse((int)Enums.StatusCode.OK, data, "Success", "");
            }
            catch (Exception ex)
            {
                return Models.Response.GetResponse((int)Enums.StatusCode.ERROR, "", ex.StackTrace, "Data not found");
            }
        }
    }
}