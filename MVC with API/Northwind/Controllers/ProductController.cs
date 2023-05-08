using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Northwind.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Northwind.Controllers
{
    public class ProductController : Controller
    {
        private readonly string baseUrl ;
        private readonly string appJson ;

        // GET: ProductController
        public ProductController(IConfiguration config)
        {
            baseUrl = config.GetValue<string>("BaseUrl");
            appJson = config.GetValue<string>("AppJson");
        }
        public async Task<ActionResult> Index(string productId)
        {
            
            try
            {
                var docs = new List<Product>();
                using (var client = new HttpClient())
                {
                    ConfigClient(client);
                    var response = await client.GetAsync("products");

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        docs = JsonSerializer.Deserialize<List<Product>>(json);
                    }
                    else
                    {
                        ViewBag.ErrorMessage = response.StatusCode.ToString();
                        return View("Error", new ErrorViewModel());
                    }
                }

                return View(docs);



                return View(docs);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error", new ErrorViewModel());
            }
        }



        // GET: ProductController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var doc = await GetProductAsync(id);
            if (doc.productId == 0)
            {
                ViewBag.ErrorMessage = $"Unable to find product with id ={id}";
                return View("Error", new ErrorViewModel());

            }
            return View(doc);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var content = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, appJson);
                    using (var client = new HttpClient())
                    {
                        ConfigClient(client);
                        var response = await client.PostAsync("products", content);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error", new ErrorViewModel());
            }
        }

        // GET: ProductController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var doc = await GetProductAsync(id);
            if (doc.productId == 0)
            {
                ViewBag.ErrorMessage = $"Unable to find product with id ={id}";
                return View("Error", new ErrorViewModel());

            }
            return View(doc);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var content = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, appJson);
                    using (var client = new HttpClient())
                    {
                        ConfigClient(client);
                        var response = await client.PutAsync($"products/{id}", content);
                        if (!response.IsSuccessStatusCode)
                        {
                            ViewBag.ErrorMessage = response.StatusCode.ToString();
                            return View("Error", new ErrorViewModel());
                        }
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error", new ErrorViewModel());
            }
        }

        // GET: ProductController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var doc = await GetProductAsync(id);
            if (doc.productId == 0)
            {
                ViewBag.ErrorMessage = $"Unable to find product with id ={id}";
                return View("Error", new ErrorViewModel());

            }
            return View(doc);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    ConfigClient(client);
                    var response = await client.DeleteAsync($"product/{id}");
                    if (!response.IsSuccessStatusCode)
                    {
                        ViewBag.ErrorMessage = response.StatusCode.ToString();
                        return View("Error", new ErrorViewModel());
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error", new ErrorViewModel());
            }
        }
        private void ConfigClient(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(appJson));
            client.BaseAddress = new Uri(baseUrl);
        }
        private async Task<Product> GetProductAsync(int id)
        {
            var doc = new Product();
            try
            {
                using (var client = new HttpClient())
                {
                    ConfigClient(client);
                    var response = await client.GetAsync($"products/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        doc = JsonSerializer.Deserialize<Product>(json);
                    }
                }
            }
            catch
            {
            }

            return doc;
        }
    }

}

