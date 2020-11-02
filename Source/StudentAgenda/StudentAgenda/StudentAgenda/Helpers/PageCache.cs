using MonkeyCache.FileStore;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace StudentAgenda.Helpers
{
    public class APIResponse
    {
        public bool IsSuccessful;
        public string Content;
        public IRestRequest Request;
        public Exception ErrorException;
        public string ErrorMessage;
    }

    public static class PageCache
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="memoryCacheTimeout">Timeout for cached data in minutes</param>
        /// <param name="invalidateCache">If true then request will always be made even if the item is cached</param>
        /// <param name="cacheOnDisk">If true the request data will be cached in both memory and disk</param>
        /// <returns></returns>
        public static async Task<APIResponse> ExecuteCachedAPITaskAsync(this IRestClient client, IRestRequest request, int memoryCacheTimeout, bool invalidateCache = false, bool cacheOnDisk = true)
        {
            bool success;
            string ErrorMessage = "";
            Exception ErrorException = null;

            string requestURI = client.BuildUri(request).PathAndQuery;
            string responseString = DataCache.Instance.Retrieve(requestURI);

            success = responseString != null && !invalidateCache;

            if (!success)
            {
                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    responseString = response.Content ?? "";
                    DataCache.Instance.Store(requestURI, responseString, memoryCacheTimeout);

                    if (responseString.Length > 0 && cacheOnDisk)
                        Barrel.Current.Add(requestURI, responseString, TimeSpan.FromDays(30));

                    success = true;
                }
                else if (response.ResponseStatus == ResponseStatus.Error)
                {
                    // Network Unavailable localization key
                    ErrorMessage = "Network Unavailable";
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadGateway)
                {
                    // 502 bad gateway. NGINX is running, but the Docker container is unreachable
                    ErrorMessage = "Bad Gateway";
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
                {
                    // 503 service unavailable
                    ErrorMessage = "Network Service Unavailable";
                }
                else if (response.ResponseStatus == ResponseStatus.TimedOut)
                {
                    ErrorMessage = "Connection Timed Out";
                }
                else
                {
                    ErrorMessage = response.StatusDescription;
                }

                if (response.ErrorException != null)
                {
                    ErrorException = response.ErrorException;
                }

                ErrorLogger.LogErrors(ErrorMessage, request, response);

                if (!success)
                {
                    if (Barrel.Current.Exists(requestURI))
                    {
                        responseString = Barrel.Current.Get<string>(requestURI);
                        success = true;
                    }
                }
            }

            return new APIResponse
            {
                IsSuccessful = success,
                Content = responseString ?? "",
                Request = request,
                ErrorException = ErrorException,
                ErrorMessage = ErrorMessage
            };
        }
    }
}
