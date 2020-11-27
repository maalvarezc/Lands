namespace Lands.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Models;
    using Newtonsoft.Json;
    using Plugin.Connectivity;
    //using IdentityModel.Client;

    class ApiService
    {
        public async Task<Responses> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Responses
                {
                    IsSucess = false,
                    message = "Please turn on your Internet Settings",
                };
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");

            if (!isReachable)
            {
                return new Responses
                {
                    IsSucess = false,
                    message = "Check your Internet Connection",
                };
            }

            return new Responses
            {
                IsSucess = true,
                message = "Ok",
            };
        }

        public async Task<TokenResponse> GetToken(
                string UrlBase,
                string username,
                string password)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(UrlBase);
                var response = await client.PostAsync("Token", new StringContent(string.Format(
                    "grant_type=password&username={0}&password={1}", username, password),
                    Encoding.UTF8, "application/x-www-form-urlencoded"));
                var resultJSON = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TokenResponse>(resultJSON);
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Responses> Get<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            int id)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format(
                    "{0}{1}/{2}",
                    servicePrefix,
                    controller,
                    id);
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Responses
                    {
                        IsSucess = false,
                        message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<T>(result);

                return new Responses
                {
                    IsSucess = true,
                    message = "ok",
                    Result = model,
                };
            }
            catch (Exception ex)
            {
                return new Responses
                {
                    IsSucess = false,
                    message = ex.Message,
                };
            }
        }

        public async Task<Responses> GetList<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format(
                    "{0}{1}",
                    servicePrefix,
                    controller);
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Responses
                    {
                        IsSucess = false,
                        message = result,
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(result);

                return new Responses
                {
                    IsSucess = true,
                    message = "ok",
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new Responses
                {
                    IsSucess = false,
                    message = ex.Message,
                };
            }
        }


        public async Task<Responses> GetList<T>(
            string urlBase,
            string servicePrefix,
            string controller)
        {   
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Responses
                    {
                        IsSucess = false,
                        message = result,
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(result);

                return new Responses
                {
                    IsSucess = true,
                    message = "ok",
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new Responses
                {
                    IsSucess = false,
                    message = ex.Message,
                };
            }
        }
        public async Task<Responses> GetList<T>(
           string urlBase,
           string servicePrefix,
           string controller,
           string tokenType,
           string accessToken,
           int id)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format(
                    "{0}{1}/{2}",
                    servicePrefix,
                    controller,
                    id);
                var response = await client.GetAsync(url);
                

                if (!response.IsSuccessStatusCode)
                {
                    return new Responses
                    {
                        IsSucess = false,
                        message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<T>>(result);

                return new Responses
                {
                    IsSucess = true,
                    message = "ok",
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new Responses
                {
                    IsSucess = false,
                    message = ex.Message,
                };
            }
        }

        public async Task<Responses> Post<T>(
           string urlBase,
           string servicePrefix,
           string controller,
           string tokenType,
           string accessToken,
           T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(
                    request, Encoding.UTF8,
                    "application/json");
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format(
                    "{0}{1}",
                    servicePrefix,
                    controller);
                var response = await client.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();


                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<Responses>(result);
                    error.IsSucess = false;
                    return error;   
                }

                var newRecord = JsonConvert.DeserializeObject<T>(result);

                return new Responses
                {
                    IsSucess = true,
                    message = "Record added OK",
                    Result = newRecord,
                };
            }
            catch (Exception ex)
            {
                return new Responses
                {
                    IsSucess = false,
                    message = ex.Message,
                };
            }
        }

        public async Task<Responses> Post<T>(
               string urlBase,
               string servicePrefix,
               string controller,
               T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(
                    request, Encoding.UTF8,
                    "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format(
                    "{0}{1}",
                    servicePrefix,
                    controller);
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Responses
                    {
                        IsSucess = false,
                        message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var newRecord = JsonConvert.DeserializeObject<T>(result);

                return new Responses
                {
                    IsSucess = true,
                    message = "Record added OK",
                    Result = newRecord,
                };
            }
            catch (Exception ex)
            {
                return new Responses
                {
                    IsSucess = false,
                    message = ex.Message,
                };
            }
        }

        public async Task<Responses> Put<T>(
           string urlBase,
           string servicePrefix,
           string controller,
           string tokenType,
           string accessToken,
           T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(
                    request, Encoding.UTF8,
                    "application/json");
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format(
                    "{0}{1}/{2}",
                    servicePrefix,
                    controller,
                    model.GetHashCode());
                var response = await client.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                        var error = JsonConvert.DeserializeObject<Responses>(result);
                        error.IsSucess = false;
                        return error;
                }


                var newRecord = JsonConvert.DeserializeObject<T>(result);

                return new Responses
                {
                    IsSucess = true,
                    Result = newRecord,
                };
            }
            catch (Exception ex)
            {
                return new Responses
                {
                    IsSucess = false,
                    message = ex.Message,
                };
            }
        }

        public async Task<Responses> Delete<T>(
           string urlBase,
           string servicePrefix,
           string controller,
           string tokenType,
           string accessToken,
           T model)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
                var url = string.Format(
                    "{0}{1}/{2}",
                    servicePrefix,
                    controller,
                    model.GetHashCode());
                var response = await client.DeleteAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<Responses>(result);
                    error.IsSucess = false;
                    return error;
                }

                return new Responses
                {
                    IsSucess = true,                   
                };
            }
            catch (Exception ex)
            {
                return new Responses
                {
                    IsSucess = false,
                    message = ex.Message,
                };
            }
        }
    }
}
