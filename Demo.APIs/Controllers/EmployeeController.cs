using AutoMapper;
using Demo.BL.Helper;
using Demo.BL.Interfaces;
using Demo.BL.Models;
using Demo.DAL.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml;

namespace Demo.APIs.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        // 2 - Create Fields
        private readonly IEmployeeRep employee;
        private readonly IMapper mapper;


        // 1 - Inject Dependencies (Employee , Auto Mapper)
        public EmployeeController(IEmployeeRep employee, IMapper mapper)
        {
            this.employee = employee;
            this.mapper = mapper;
        }


        // 3 - Actions (CRUD)

        //[HttpGet, Route("~/api/Data")]
        //public string[] Data()
        //{
        //    var names = new string[] { "Ahmed", "Sara", "Ali" };
        //    return names;
        //}

        //[HttpGet, Route("~/api/Names")]
        //public string[] Names()
        //{
        //    var names = new string[] { "Ahmed", "Mohamed", "Mona" };
        //    return names;
        //}

        [HttpGet, Route("~/api/GetData")]
        public string GetData()
        {

            Chilkat.Rest rest = new Chilkat.Rest();
            bool bTls = false;
            int port = 80;
            bool bAutoReconnect = true;
            // In this particular case, it is important to connect to "wsf.cdyne.com", not "ws.cdyne.com"...
            bool success = rest.Connect("wsf.cdyne.com", port, bTls, bAutoReconnect);
            if (success != true)
            {
                return rest.LastErrorText;
            }

            // Add request headers:
            success = rest.AddHeader("Content-Type", "text/xml; charset=utf-8");
            success = rest.AddHeader("SOAPAction", "http://ws.cdyne.com/WeatherWS/GetCityWeatherByZIP");

            // Build the SOAP XML request body.
            Chilkat.Xml soapXml = new Chilkat.Xml();

            soapXml.Tag = "soap:Envelope";
            success = soapXml.AddAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            success = soapXml.AddAttribute("xmlns:xsd", "http://www.w3.org/2001/XMLSchema");
            success = soapXml.AddAttribute("xmlns:soap", "http://schemas.xmlsoap.org/soap/envelope/");

            soapXml.NewChild2("soap:Body", "");
            success = soapXml.GetChild2(0);
            soapXml.NewChild2("GetCityWeatherByZIP", "");
            success = soapXml.GetChild2(0);
            success = soapXml.AddAttribute("xmlns", "http://ws.cdyne.com/WeatherWS/");
            soapXml.NewChild2("ZIP", "60187");
            soapXml.GetRoot2();

            Debug.WriteLine(soapXml.GetXml());

            // Send the SOAP request
            string responseXml = rest.FullRequestString("POST", "/WeatherWS/Weather.asmx", soapXml.GetXml());
            if (rest.LastMethodSuccess != true)
            {
                return rest.LastErrorText;
            }

            // When successful, the response status code will equal 200.
            if (rest.ResponseStatusCode != 200)
            {
                // Examine the request/response to see what happened.
                Debug.WriteLine("response status code = " + Convert.ToString(rest.ResponseStatusCode));
                Debug.WriteLine("response status text = " + rest.ResponseStatusText);
                Debug.WriteLine("response header: " + rest.ResponseHeader);
                Debug.WriteLine("response body (if any): " + responseXml);
                Debug.WriteLine("---");
                Debug.WriteLine("LastRequestStartLine: " + rest.LastRequestStartLine);
                Debug.WriteLine("LastRequestHeader: " + rest.LastRequestHeader);
                return "";
            }

            Chilkat.Xml xml = new Chilkat.Xml();
            success = xml.LoadXml(responseXml);

            // GetXml will emit XML that is nicely indented for human viewing..
            Debug.WriteLine(xml.GetXml());

            // A sample response XML is shown below...

            // To get some information, use ChilkatPath.  For example...
            Debug.WriteLine("Temperature: " + xml.ChilkatPath("soap:Body|GetCityWeatherByZIPResponse|GetCityWeatherByZIPResult|Temperature|*"));

            Debug.WriteLine("Success.");

            return xml.GetXml();

            //HttpClient httpClient = new HttpClient();
            //HttpRequestHeaders requestHeaders = httpClient.DefaultRequestHeaders;
            //requestHeaders.Add("Accept", "application/xml");

            //Task<HttpResponseMessage> httpResponse = httpClient.GetAsync("https://jsonplaceholder.typicode.com/todos");
            //HttpResponseMessage httpResponseMessage = httpResponse.Result;
            //Console.WriteLine(httpResponseMessage.ToString());

            //// Status Code
            //HttpStatusCode statusCode = httpResponseMessage.StatusCode;
            //Console.WriteLine($"Status Code => {statusCode}");
            //Console.WriteLine($"Status Code => {(int)statusCode}");


            //// Response Data
            //HttpContent responseContent = httpResponseMessage.Content;
            //Task<string> resonsData = responseContent.ReadAsStringAsync();
            //string data = resonsData.Result;
            //return data;




            //var data = await employee.GetAsync(emp => (emp.IsActive == true) && (emp.IsDeleted == false));
            //var result = mapper.Map<IEnumerable<EmployeeVM>>(data);

            //var success = new ApiResponse<IEnumerable<EmployeeVM>>
            //{ Code = 200, Status = "Get", Message = "Data Retrieved" };


            //return Ok(new { Data = result });
        }

        [HttpGet, FormatFilter]
        [Route("~/api/GetEmployees")]
        public async Task<IActionResult> GetEmployees()
        {

            try
            {
                var data = await employee.GetAsync(emp => (emp.IsActive == true) && (emp.IsDeleted == false));
                var result = mapper.Map<IEnumerable<EmployeeVM>>(data);

                var success = new ApiResponse<IEnumerable<EmployeeVM>>
                { Code = 200, Status = "Get", Message = "Data Retrieved" };


                //var doc = JsonExtension.ConverToXml(result);
                //ViewBag.data = doc.InnerXml;

                //return Ok(new { Data = doc });


                return Ok(new { Data = result });

            }
            catch (Exception ex)
            {
                var exception = new ApiResponse<object>
                { Code = 400, Status = "exception", Message = ex.Message };

                return NotFound(new { Status = exception });
            }

        }


        // Get By Id
        [HttpGet, Route("~/api/GetEmployeeById")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {

            try
            {
                var data = await employee.GetByIdAsync(emp =>
                           emp.IsActive == true
                           && emp.IsDeleted == false
                           && emp.Id == id);

                var result = mapper.Map<EmployeeVM>(data);

                var success = new ApiResponse<EmployeeVM>
                { Code = 200, Status = "Get", Message = "Data Retrieved" };

                return Ok(new { Status = success, Data = result });

            }
            catch (Exception ex)
            {
                var exception = new ApiResponse<object>
                { Code = 400, Status = "exception", Message = ex.Message };

                return NotFound(new { Status = exception });
            }

        }


        // Post
        [HttpPost, Route("~/api/CreateNewEmployee")]
        public async Task<IActionResult> CreateNewEmployee(EmployeeVM obj)
        {

            try
            {

                if (ModelState.IsValid)
                {
                    var result = mapper.Map<Employee>(obj);
                    await employee.CreateAsync(result);

                    var success = new ApiResponse<EmployeeVM>
                    { Code = 200, Status = "Get", Message = "Data Retrieved" };

                    return Ok(new { Status = success, Data = result });
                }


                var exception = new ApiResponse<object>
                { Code = 400, Status = "exception", Message = "Validation Exception" };

                return NotFound(new { Status = exception });

            }
            catch (Exception ex)
            {
                var exception = new ApiResponse<object>
                { Code = 400, Status = "exception", Message = ex.Message };

                return NotFound(new { Status = exception });

            }


        }


        // Put
        [HttpPut, Route("~/api/UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(EmployeeVM obj)
        {

            try
            {

                if (ModelState.IsValid)
                {
                    var result = mapper.Map<Employee>(obj);
                    await employee.UpdateAsync(result);

                    var success = new ApiResponse<EmployeeVM>
                    { Code = 200, Status = "Get", Message = "Data Retrieved" };

                    return Ok(new { Status = success, Data = result });
                }


                var exception = new ApiResponse<object>
                { Code = 400, Status = "exception", Message = "Validation Exception" };

                return NotFound(new { Status = exception });

            }
            catch (Exception ex)
            {
                var exception = new ApiResponse<object>
                { Code = 400, Status = "exception", Message = ex.Message };

                return NotFound(new { Status = exception });

            }


        }


        // Delete
        [HttpDelete, Route("~/api/DeleteEmployee")]
        //[DisableCors]
        //[EnableCors("CanDelete")]
        public async Task<IActionResult> DeleteEmployee(EmployeeVM obj)
        {

            try
            {
                var result = mapper.Map<Employee>(obj);
                await employee.DeleteAsync(result);

                var success = new ApiResponse<EmployeeVM>
                { Code = 200, Status = "Delete", Message = "Data Deleted" };

                return Ok(new { Status = success, Data = result });

            }
            catch (Exception ex)
            {
                var exception = new ApiResponse<object>
                { Code = 400, Status = "exception", Message = ex.Message };

                return NotFound(new { Status = exception });

            }


        }



        //[HttpPost]
        //public string ReturnXmlDocument(HttpRequestMessage request)
        //{
        //    var doc = new XmlDocument();
        //    doc.Load(request.Content.ReadAsStreamAsync().Result);
        //    return doc.DocumentElement.OuterXml;
        //}

        [HttpPost]
        [AllowAnonymous]
        [Route("~/api/TestTwo")]
        public IActionResult ReturnXmlDocument(HttpRequestMessage request)
        {
            List<Album> receivedata = new List<Album>();
            string data = request.Content.ReadAsStringAsync().Result;
            if (data != "")
            {
                string rawHtml = System.Net.WebUtility.HtmlDecode(data);
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.LoadXml(rawHtml);
                XmlNodeList parentNode = doc.GetElementsByTagName("Album");
                foreach (XmlNode childrenNode in parentNode)
                {
                    Album temp = new Album();
                    for (int i = 0; i <= childrenNode.ChildNodes.Count - 1; i++)
                    {
                        //coordNode.Attributes.GetNamedItem("lat").Value
                        XmlNode coordNode = childrenNode.ChildNodes.Item(i);
                        if (coordNode.Name == "ID")
                        {
                            temp.ID = coordNode.InnerText;
                        }
                        else if (coordNode.Name == "Name")
                        {
                            temp.Name = coordNode.InnerText;
                        }
                    }
                    receivedata.Add(temp);
                }
            }
            return Ok(receivedata);
        }

    }

    public class Album
    {
        public string ID { get; set; }
        public string Name { get; set; }

    }



    [DataContract]
    public class Product
    {
        [DataMember]
        private int pcode;  // serialized

        // Not serialized (read-only)
        public int ProductCode { get { return pcode; } }
    }
}
