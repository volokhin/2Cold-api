using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Dfreeze.Services
{
    public class FreezeService : IFreezeService
    {
        private const string SessionIdKey = "ASP.NET_SessionId";
        private const string SessionDomain = "91.192.175.234";

        private const string ServiceUrl = "http://91.192.175.234/screenmate/ScreenMatePage.aspx";
        private const string LoginUrl = "http://91.192.175.234/screenmate/LoginPage.aspx";

        private readonly IHtmlParserService _parser;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private ISession Session => _httpContextAccessor.HttpContext.Session;

        public FreezeService(IHtmlParserService parser,
                IHttpContextAccessor httpContextAccessor)
        {
            _parser = parser;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<FreezerModel>> GetFreezersAsync()
        {
            var args = new Dictionary<string, string>
            {
                ["roomId"] = "s8236vg",
                ["__EVENTTARGET"] = "lookUpRoomId",
                ["__EVENTVALIDATION"] = Constants.LookupEventValidation,
                ["__VIEWSTATE"] = Constants.LookupViewState,
            };

            try
            {
                return await ExecuteServiceRequest(args);;
            }
            catch(UnauthorizedAccessException)
            {
                await LoginAsync();
                return await ExecuteServiceRequest(args);;
            }
        }

        public async Task<IEnumerable<FreezerModel>> EnableAsync(int id)
        {
            var args = new Dictionary<string, string>
            {
                ["roomId"] = "s8236vg",
                ["__EVENTTARGET"] = $"dataList:_ctl{id}:next",
                ["__EVENTVALIDATION"] = Constants.EnableEventValidation,
                ["__VIEWSTATE"] = Constants.EnableViewState,
            };

            try
            {
                return await ExecuteServiceRequest(args);;
            }
            catch(UnauthorizedAccessException)
            {
                await LoginAsync();
                return await ExecuteServiceRequest(args);;
            }
        }

        public async Task<IEnumerable<FreezerModel>> DisableAsync(int id)
        {
            var args = new Dictionary<string, string>
            {
                ["roomId"] = "s8236vg",
                ["__EVENTTARGET"] = $"dataList:_ctl{id}:previous",
                ["__EVENTVALIDATION"] = Constants.DisableEventValidation,
                ["__VIEWSTATE"] = Constants.DisableViewState,
            };

            try
            {
                return await ExecuteServiceRequest(args);;
            }
            catch(UnauthorizedAccessException)
            {
                await LoginAsync();
                return await ExecuteServiceRequest(args);;
            }
        }

        private async Task LoginAsync()
        {
            var requestUri = new Uri(LoginUrl);
            var cookies = new CookieContainer();

            var args = new Dictionary<string, string>
            {
                ["userName"] = "8floor",
                ["password"] = "cit4278",
                ["userType"] = "VISTA_USER",
                ["loginButton"] = "Login",
                ["__EVENTVALIDATION"] = Constants.LoginEventValidation,
                ["__VIEWSTATE"] = Constants.LoginViewState,
            };

            using (var handler = new HttpClientHandler())
            using (var client = new HttpClient(handler))
            {
                handler.CookieContainer = cookies;

                using (var content = new FormUrlEncodedContent(args))
                using (var response = await client.PostAsync(requestUri, content))
                {
                    var cookie = cookies.GetCookies(requestUri)
                        .Cast<Cookie>()
                        .FirstOrDefault(x => x.Name == SessionIdKey);

                    if (cookie != null)
                    {
                        Session.SetString(cookie.Name, cookie.Value);
                    }
                }
            }
        }

        private async Task<IEnumerable<FreezerModel>> ExecuteServiceRequest(Dictionary<string, string> args)
        {
            var requestUri = new Uri(ServiceUrl);
            var cookies = new CookieContainer();

            using (var handler = new HttpClientHandler())
            using (var client = new HttpClient(handler))
            {
                handler.CookieContainer = cookies;

                var sessionId = Session.GetString(SessionIdKey);
                if (!String.IsNullOrEmpty(sessionId))
                {
                    cookies.Add(new Cookie(SessionIdKey, sessionId, "/", SessionDomain));
                }

                var content = new FormUrlEncodedContent(args);
                using (var response = await client.PostAsync(requestUri, content))
                {
                    var html = await response.Content.ReadAsStringAsync();
                    var pageType = _parser.GetPageType(html);
                    switch (pageType)
                    {
                        case PageType.Login:
                            throw new UnauthorizedAccessException();
                        case PageType.InvalidRoomId:
                            throw new Exception("Invalid room ID.");
                        case PageType.Unknown:
                            throw new Exception("Unexpected HTML page content.");
                        case PageType.FreezersInfo:
                            var freezers = _parser.Parse(html);
                            return freezers;
                    }

                    return new List<FreezerModel>();
                }
            }
        }
    }
}