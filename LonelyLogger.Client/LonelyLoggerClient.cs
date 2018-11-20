using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LonelyLogger.Client
{
    public class LonelyLoggerClient : ILonelyLoggerClient
    {
        private Uri _BaseAddress;
        private readonly string _PostLogPath = "/api/log";
        private readonly string _PostLogsPath = "/api/logs";
        private DateTime _LastLogSentTime = DateTime.MinValue;
        private ConcurrentBag<object> _LogsToSend = new ConcurrentBag<object>();
        private Task _DelayedLogPost;

        public LonelyLoggerClient(Uri baseAddress)
        {
            _BaseAddress = baseAddress;
        }

        public async Task<LogResult> PostLogAsync(object objectToLog)
        {
            try
            {
                if(_LastLogSentTime < DateTime.Now.AddSeconds(-1))
                {
                    var client = new HttpClient();
                    client.BaseAddress = _BaseAddress;
                    client.Timeout = new TimeSpan(0, 0, 30);

                    var serializedObject = JsonConvert.SerializeObject(objectToLog);
                    var bodyContent = new StringContent(serializedObject);
                    bodyContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var response = await client.PostAsync(_PostLogPath, bodyContent);
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        throw new Exception("LonelyLogger did not recieve a 200 response.");
                    }

                    _LastLogSentTime = DateTime.Now;
                    return new LogResult()
                    {
                        Succeeded = true,
                        Result = LogResultEnum.Sent
                    };
                }
                else
                {
                    if(_DelayedLogPost == null)
                    {
                        _DelayedLogPost = SendLogsAfterOneSecond();
                    }
                    _LogsToSend.Add(objectToLog);
                    return new LogResult()
                    {
                        Succeeded = true,
                        Result = LogResultEnum.Queued
                    };
                }           
            }
            catch (Exception ex)
            {
                return new LogResult()
                {
                    Succeeded = false,
                    Exception = ex
                };
            }
        }

        public async Task<LogResult> PostLogsAsync(IEnumerable<object> objectsToLog)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = _BaseAddress;
                client.Timeout = new TimeSpan(0, 0, 30);

                var serializedObject = JsonConvert.SerializeObject(objectsToLog);
                var bodyContent = new StringContent(serializedObject);
                bodyContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PostAsync(_PostLogsPath, bodyContent);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception("LonelyLogger did not recieve a 200 response.");
                }
            }
            catch (Exception ex)
            {
                return new LogResult()
                {
                    Succeeded = false,
                    Result = LogResultEnum.Exception,
                    Exception = ex
                };
            }

            return new LogResult()
            {
                Succeeded = true,
                Result = LogResultEnum.Sent,
            };
        }

        private async Task SendLogsAfterOneSecond()
        {
            await Task.Delay(1000);
            var logsToSend = _LogsToSend;
            _LogsToSend = null;
            await PostLogsAsync(logsToSend);
            _DelayedLogPost = null;
        }
    }
}
