using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TextAnalytics.Services;

namespace TextAnalytics.UI.Controllers
{
    public class SentimentController : Controller
    {
        private SentimentService _service;
        public SentimentController()
        {
            _service = new SentimentService();

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Post()
        {
            return View();
        }
    }
}