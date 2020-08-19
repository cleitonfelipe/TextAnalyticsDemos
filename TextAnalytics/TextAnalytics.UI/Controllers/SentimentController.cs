using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TextAnalytics.Services;
using TextAnalytics.UI.Models;

namespace TextAnalytics.UI.Controllers
{
    public class SentimentController : Controller
    {
        private SentimentService _service = new SentimentService();

        public IActionResult Index(TextModel model = null)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Post()
        {
            return View();
        }

        public async Task<IActionResult> Post([FromForm] TextModel textModel)
        {
            var _result = new TextModel();
            _result.Text = textModel.Text;
            _result.Result = await _service.GetSentiment(textModel.Text);
            return View("Index", _result);
        }
    }
}